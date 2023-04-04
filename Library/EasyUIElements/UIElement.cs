using System;
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
        public string Id { get; private set; }

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

        protected string Tags;

        protected virtual void ApplyArgs(Dictionary<string, object> args = null)
        {

            if (args.TryGetValue("tag", out object tag))
                Tags = (string)tag;

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
                }
                if(size.Equals("fitWidth") && RectTransform != null)
                {
                    RectTransform.anchorMin = new Vector2(0, RectTransform.anchorMin.y);
                    RectTransform.anchorMax = new Vector2(1, RectTransform.anchorMax.y);
                }
            }

            
            if (args.TryGetValue("anchorsMin", out object anchorsMin))
                RectTransform.anchorMin = (Vector2)anchorsMin;
            if (args.TryGetValue("anchorsMax", out object anchorsMax))
                RectTransform.anchorMax = (Vector2)anchorsMax;

            if (args.TryGetValue("offsetMin", out object offsetMin))
                RectTransform.offsetMin = (Vector2)offsetMin;
            if (args.TryGetValue("offsetMax", out object offsetMax))
                RectTransform.offsetMax = (Vector2)offsetMax;

            if (args.TryGetValue("left", out object left))
                RectTransform.offsetMin = new Vector2((float)left, RectTransform.offsetMin.y);

            if (args.TryGetValue("right", out object right))
                RectTransform.offsetMax = new Vector2((float)right, RectTransform.offsetMax.y);

            if (args.TryGetValue("bottom", out object bottom))
                RectTransform.offsetMin = new Vector2(RectTransform.offsetMin.x, (float)bottom);

            if (args.TryGetValue("top", out object top))
                RectTransform.offsetMax = new Vector2(RectTransform.offsetMin.x, (float)top * -1);

            if (args.TryGetValue("width", out object width))
                RectTransform.sizeDelta = new Vector2((float)width, RectTransform.sizeDelta.y);

            if (args.TryGetValue("height", out object height))
                RectTransform.sizeDelta = new Vector2(RectTransform.sizeDelta.x, (float)height);

            if (args.TryGetValue("sizeDelta", out object sizeDelta))
                RectTransform.sizeDelta = (Vector2)sizeDelta;
        }

        protected virtual void OnEnable() 
        {
            if (string.IsNullOrEmpty(Id))
                Id = System.Guid.NewGuid().ToString();

            SO_EasyUIRuntimeDataContainer.RegisterUI(this);
        }

        protected virtual void OnDisable() 
        {
            SO_EasyUIRuntimeDataContainer.RemoveUI(this);
        }

        public bool HasTag(string tag)
        {
            return string.IsNullOrEmpty(Tags) ? false : tag.Length == 0 ? false : System.Array.Exists(Tags.Split(','), x => x.Equals(tag));
        }

        public void AddTag(string tag)
        {
            Tags = string.Concat(Tags, "," + tag.Trim());
        }

        //IFade implementations
        public virtual void FadeIn(bool waitBeforeFade = true, Action onFadeIn = null)
        {
            StopCoroutine(IEFadeTo(1, waitBeforeFade));
            StartCoroutine(IEFadeTo(1, waitBeforeFade, onFadeIn));
        }

        public virtual void FadeOut(bool waitBeforeFade = true, Action onFadeOut = null)
        {
            StopCoroutine(IEFadeTo(0, waitBeforeFade));
            StartCoroutine(IEFadeTo(0, waitBeforeFade, onFadeOut));
        }

        public void WaitEndOfFrameAndExecute(System.Action action) 
        {
            StopCoroutine(IEWaitEndOfFrameAndExecute(action));
            StartCoroutine(IEWaitEndOfFrameAndExecute(action));
        }

        IEnumerator IEFadeTo(float value, bool waitBeforeFade = true, Action onFade = null) 
        {
            CanvasGroup.alpha = value > 0 ? 0 : 1;

            if(waitBeforeFade)
                yield return new WaitForSeconds(.5f);
            
            var fade = CanvasGroup.DOFade(value, .15f);
            yield return fade.WaitForCompletion();
            onFade?.Invoke();
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