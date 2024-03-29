using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.ContentSizeFitter;

namespace EasyUI.Library
{
    public class UIContainer : UIElement
    {

        public const string UIContainerPropertyUseContentSizeFitter = "useContentSizeFitter";
        public const string UIContainerPropertyHorizontalFit = "horizontalFit";
        public const string UIContainerPropertyVerticalFit = "verticalFit";
        public const string UIContainerPropertyBackgroundColor = "backgroundColor";
        public const string UIContainerPropertyBackgroundSprite = "backgroundSprite";

        public bool UseContentSizeFitter => useContentSizeFitter;

        public float widht => RectTransform.sizeDelta.x;
        public float height => RectTransform.sizeDelta.y;
        [SerializeField] ContentSizeFitter contentSizeFitter;
        [SerializeField] Image background;
        [SerializeField] bool useContentSizeFitter;

        List<string> childs;

        public virtual void SetupElement(Dictionary<string, object> args = null) 
        {
            childs = new List<string>();
            ApplyArgs(args);
        }

        public string[] GetChildren() 
        {
            return childs.ToArray();
        }

        public void AddChild(string id) 
        {
            childs.Add(id);
        }

        protected override void ApplyArgs(Dictionary<string, object> args = null)
        {
            if (args.TryGetValue("useContentSizeFitter", out object useContentSizeFitter))
            {
                if ((bool)useContentSizeFitter && contentSizeFitter == null)
                {
                    contentSizeFitter = gameObject.GetComponent<ContentSizeFitter>();
                    if (contentSizeFitter == null)
                        contentSizeFitter = gameObject.AddComponent<ContentSizeFitter>();
                }

                this.useContentSizeFitter = (bool)useContentSizeFitter;
                contentSizeFitter.enabled = this.useContentSizeFitter;
            }

            if (this.useContentSizeFitter)
            {
                if (args.TryGetValue("horizontalFit", out object horizontalFitMode))
                    contentSizeFitter.horizontalFit = (FitMode)horizontalFitMode;

                if(args.TryGetValue("verticalFit", out object verticalFitMode))
                    contentSizeFitter.verticalFit = (FitMode)verticalFitMode;
            }

            if (args.TryGetValue("backgroundColor", out object backgroundColor) && background != null) 
                background.color = (Color)backgroundColor;

            if (args.TryGetValue("backgroundSprite", out object backgroundSprite) && background != null)
                background.sprite = (Sprite)backgroundSprite;

            base.ApplyArgs(args);
        }
    }
}