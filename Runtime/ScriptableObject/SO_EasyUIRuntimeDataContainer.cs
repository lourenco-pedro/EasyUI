using System.Linq;
using System.Collections.Generic;

using UnityEngine;

using EasyUI.Library;

namespace EasyUI.Runtime
{
    [CreateAssetMenu(menuName = "EasyUI/RuntimeDataContainer", fileName = "RuntimeDataContainer")]
    public class SO_EasyUIRuntimeDataContainer : ScriptableObject
    {
        static SO_EasyUIRuntimeDataContainer instance;

        HashSet<UIElement> registeredUIs;
        HashSet<string> canvasRoot;

        public static void RegisterUI(UIElement uiToRegister) 
        {
            instance.registeredUIs.Add(uiToRegister);
            if (uiToRegister.transform.parent == BuilderUI.GetScreenMargins().transform) 
                instance.canvasRoot.Add(uiToRegister.Id);
        }
        
        public static void RemoveUI(UIElement uiToRemove) 
        {
            if(instance.registeredUIs.Contains(uiToRemove))
                instance.registeredUIs.Remove(uiToRemove);
            
            if(instance.canvasRoot.Contains(uiToRemove.Id))
                instance.canvasRoot.Remove(uiToRemove.Id);
        }

        public static UIElementType GetUIElementWithId<UIElementType>(string id)
            where UIElementType: UIElement
        {
            var elements = instance.registeredUIs.Where(element => element.Id.Equals(id)).ToArray();
            if (elements.Length > 0)
                return elements[0] as UIElementType;

            return null;
        }

        public static string[] GetCanvasRootElements() 
        {
            return instance.canvasRoot.ToArray();
        }

        [RuntimeInitializeOnLoadMethod]
        public static void SetupInstance() 
        {
            instance = Resources.Load<SO_EasyUIRuntimeDataContainer>("RuntimeDataContainer");

            instance.registeredUIs = new HashSet<UIElement>();
            instance.canvasRoot = new HashSet<string>();
        }

        public static UIElementType GetElementWithTag<UIElementType>(string tag)
            where UIElementType : UIElement
        {
            if (null == instance)
                return null;

            var elements = instance.registeredUIs.Where(element => element.HasTag(tag)).ToArray();
            if (null == elements || elements.Length == 0)
                return null;

            return elements[1] as UIElementType;
        }
    }
}