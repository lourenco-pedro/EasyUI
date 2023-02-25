using System.Collections.Generic;

using UnityEngine;

using EasyUI.Interfaces;
using EasyUI.Runtime;
using System.Collections;

using DG.Tweening;

namespace EasyUI.Library
{

    [RequireComponent(typeof(CanvasGroup))]
    public class UIElement : MonoBehaviour, IFade
    {
        public RectTransform RectTransform
        {
            get
            {
                if (rect == null)
                    rect = transform as RectTransform;

                return rect;
            }
        }
        public CanvasGroup CanvasGroup
        {
            get
            {
                if (canvasGroup == null)
                    canvasGroup = gameObject.GetComponent<CanvasGroup>();

                return canvasGroup;
            }
        }

        //Protected
        protected RectTransform rect;
        protected CanvasGroup canvasGroup;

        protected virtual void ApplyArgs(Dictionary<string, object> args = null)
        {
            if (args.TryGetValue("pivot", out object pivot))
                RectTransform.pivot = (Vector2)pivot;                

            if (args.TryGetValue("childAnchor", out object childAlignment))
            {
                TextAnchor anchor = (TextAnchor)childAlignment;
                RectTransform.position = RectTransform.GetChildAnchorPosition(anchor);
            }

            if (args.TryGetValue("x", out object x))
                RectTransform.position = new Vector2((float)x, RectTransform.position.y);

            if (args.TryGetValue("y", out object y))
                RectTransform.position = new Vector2(RectTransform.position.x, (float)y);

            if (args.TryGetValue("size", out object size)) 
            {

                if (size.Equals("fit") && RectTransform.parent != null)
                {
                    RectTransform.anchorMin = Vector2.zero;
                    RectTransform.anchorMax = Vector2.one;
                    RectTransform.offsetMin = Vector2.zero;
                    RectTransform.offsetMax = Vector2.one;
                }
                else if (size.Equals("fitWidth") && RectTransform.parent != null) 
                {
                    RectTransform.anchorMin = new Vector2((RectTransform.parent.transform as RectTransform).anchorMin.x, RectTransform.anchorMin.y);
                    RectTransform.anchorMax = new Vector2((RectTransform.parent.transform as RectTransform).anchorMax.x, RectTransform.anchorMax.y);
                    RectTransform.offsetMin = new Vector2((RectTransform.parent.transform as RectTransform).offsetMin.x, RectTransform.offsetMin.y);
                    RectTransform.offsetMax = new Vector2((RectTransform.parent.transform as RectTransform).offsetMax.x, RectTransform.offsetMax.y);
                }
            }
            
            if (args.TryGetValue("left", out object left))
                RectTransform.offsetMin = new Vector2((float)left, RectTransform.offsetMin.y);

            if (args.TryGetValue("right", out object right))
                RectTransform.offsetMin = new Vector2((float)right, RectTransform.offsetMin.y);

            if (args.TryGetValue("bottom", out object bottom))
                RectTransform.offsetMin = new Vector2(RectTransform.offsetMin.x, (float)bottom);

            if (args.TryGetValue("bottom", out object top))
                RectTransform.offsetMax = new Vector2(RectTransform.offsetMin.x, (float)top);


            if (args.TryGetValue("width", out object width))
                RectTransform.sizeDelta = new Vector2((float)width, RectTransform.sizeDelta.y);

            if (args.TryGetValue("height", out object height))
                RectTransform.sizeDelta = new Vector2(RectTransform.sizeDelta.x, (float)height);
        }

        protected virtual void OnEnable() 
        {
            SO_EasyUIRuntimeDataContainer.RegisterUI(this);
        }

        protected virtual void OnDisable() 
        {
            SO_EasyUIRuntimeDataContainer.RemoveUI(this);
        }


        //IFade implementations
        public virtual void FadeIn()
        {
            StopCoroutine(IEFadeTo(1));
            StartCoroutine(IEFadeTo(1));
        }

        public virtual void FadeOut()
        {
            StopCoroutine(IEFadeTo(0));
            StartCoroutine(IEFadeTo(0));
        }

        public void WaitEndOfFrameAndExecute(System.Action action) 
        {
            StopCoroutine(IEWaitEndOfFrameAndExecute(action));
            StartCoroutine(IEWaitEndOfFrameAndExecute(action));
        }

        IEnumerator IEFadeTo(float value) 
        {
            CanvasGroup.alpha = value > 0 ? 0 : 1;
            yield return new WaitForSeconds(.5f);
            var fade = CanvasGroup.DOFade(value, .15f);
            yield return fade.WaitForCompletion();
        }

        IEnumerator IEWaitEndOfFrameAndExecute(System.Action action) 
        {
            yield return new WaitForEndOfFrame();
            action?.Invoke();
        }
    }

    public abstract class UIElement<ElementData> : UIElement
    {
        public ElementData Data => data;

        //Protected
        protected ElementData data;

        public virtual void SetupElement(ElementData data, Dictionary<string, object> args = null) 
        {
            this.data = data;
            ApplyArgs(args);
        }
    }
}