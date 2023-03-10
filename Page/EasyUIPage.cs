using System;
using System.Linq;
using System.Collections.Generic;

using EasyUI.Library;
using EasyUI.Runtime;

namespace EasyUI.Page
{
    public abstract class EasyUIPage
    {
        
        public string Name { get; private set; }
        
        protected List<string> Elements;

        public EasyUIPage(string name = "") 
        {
            Name = name;
            Elements = OnDrawPage();
        }

        public abstract List<string> OnDrawPage();
        public virtual void Close(Action<UIElement[]> onClose = null)
        {

            if (onClose != null)
                onClose += (roots) => { stackedPages.Remove(this); };
            else
                onClose = (roots) => { stackedPages.Remove(this); };

            UIElement[] roots = GetPageRoot();

            foreach (UIElement root in roots) 
            {
                root.FadeOut(waitBeforeFade: false, ()=> 
                {
                    UnityEngine.GameObject.Destroy(root.gameObject);
                });
            }

            onClose.Invoke(roots);
        }

        public UIElement[] GetPageRoot() 
        {
            string[] canvasRoot = SO_EasyUIRuntimeDataContainer.GetCanvasRootElements();
            string[] rootIds = Elements.Where(elementId => canvasRoot.Contains(elementId)).ToArray();

            return rootIds.ToUIElement();
        }


        #region - System - 

        public static EasyUIPage currentPage => stackedPages.Count > 0? stackedPages[stackedPages.Count - 1] : null;
        static List<EasyUIPage> stackedPages = new List<EasyUIPage>();

        public static void Add(EasyUIPage page) 
        {
            stackedPages.Add(page);
        }

        public static void Pop(Action<UIElement[]> onClose = null) 
        {
            stackedPages[stackedPages.Count - 1].Close(onClose);
        }

        #endregion
    }
}
