using System;
using System.Collections.Generic;

using UnityEngine;

using EasyUI.Library;


namespace EasyUI.Runtime
{
    public static class BuilderUI
    {
        static Canvas defaultCanvas = null;

        public class BuildSettings 
        {
            public bool Fade;
        }

        public static void AddContainer<UIContainerType>(Dictionary<string, object> args = null, Action<UIContainerType> onElementCreated = null)
            where UIContainerType : UIContainer 
        {
            CheckCanvas();
            AddContainer(defaultCanvas, args, onElementCreated);
        }

        public static void AddContainer<UIContainerType>(UIContainer parent, Dictionary<string, object> args = null, Action<UIContainerType> onElementCreated = null)
            where UIContainerType : UIContainer
        {
            UIContainerType prefab = SO_ResourcesLibrary.GetEasyUI<UIContainerType>();

            if (null == prefab)
            {
                return;
            }

            UIContainerType instance = GameObject.Instantiate(prefab, parent.transform);
            instance.SetupElement(args);

            onElementCreated?.Invoke(instance);
        }

        public static void AddContainer<UIContainerType>(Canvas parent, Dictionary<string, object> args = null, Action<UIContainerType> onElementCreated = null)
            where UIContainerType : UIContainer
        {
            UIContainerType prefab = SO_ResourcesLibrary.GetEasyUI<UIContainerType>();

            if (null == prefab)
            {
                return;
            }

            UIContainerType instance = GameObject.Instantiate(prefab, parent.transform);
            instance.SetupElement(args);

            onElementCreated?.Invoke(instance);
        }

        public static void AddUIElement<UIElementType, ElementData>(ElementData data, UIContainer parent = null, Dictionary<string, object> args = null, Action<UIElementType> onElementCreated = null, BuildSettings settings = null)
            where UIElementType : UIElement<ElementData>
        {
            UIElementType prefab = SO_ResourcesLibrary.GetEasyUI<UIElementType, ElementData>();

            if (null == settings) 
            {
                settings = new BuildSettings
                {
                    Fade = true
                };
            }

            if (null == prefab) 
            {
                return;
            }

            CheckCanvas();

            UIElementType instance = GameObject.Instantiate(prefab, parent != null ? parent.transform : defaultCanvas.transform);

            instance.SetupElement(data, args);

            if (settings.Fade) 
            {
                instance.FadeIn();
            }

            onElementCreated?.Invoke(instance);
        }

        public static void AddUIElements<UIElementType, ElementData>(List<ElementData> datas, UIContainer parent = null, Dictionary<string, object> args = null, Action<UIElementType> onElementCreated = null)
            where UIElementType : UIElement<ElementData>
        {
            foreach (ElementData data in datas) 
            {
                AddUIElement(data, parent, args, onElementCreated);
            }
        }

        public static void AddMask<UIElementType>(UIElementType uiElement, string maskType = "rounded", bool showMaskGraphics = false)
            where UIElementType : UIElement
        {
            Transform targetsParent = uiElement.RectTransform.parent;
            Mask maskPrefab = SO_ResourcesLibrary.GetEasyUI<Mask, Sprite>();
            Mask instance = GameObject.Instantiate(maskPrefab, targetsParent);

            instance.SetupElement(SO_ResourcesLibrary.GetSprite("mask_"+maskType), args: new Dictionary<string, object> 
            {
                { "width", uiElement.RectTransform.sizeDelta.x },
                { "height", uiElement.RectTransform.sizeDelta.y },
                { "x", uiElement.RectTransform.position.x },
                { "y", uiElement.RectTransform.position.y },
                { "showMaskGraphics", showMaskGraphics }
            });

            instance.RectTransform.SetParent(targetsParent);          
            uiElement.RectTransform.SetParent(instance.RectTransform);
            
            instance.WaitEndOfFrameAndExecute(() => 
            {
                uiElement.RectTransform.sizeDelta = instance.RectTransform.sizeDelta;
            });
        }

        public static void SetCanvas(Canvas canvas) 
        {
            defaultCanvas = canvas;
        }

        static void CheckCanvas() 
        {
            if (defaultCanvas == null)
            {
                Canvas canvas = GameObject.FindObjectOfType<Canvas>();
                if (canvas != null)
                    SetCanvas(canvas);
            }
        }
    }
}