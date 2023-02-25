using System;
using System.Collections.Generic;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

namespace EasyUI.Library
{
    [RequireComponent(typeof(Button))]
    [RequireComponent(typeof(Image))]
    public class SimpleButton : UIElement<Action>
    {

        [SerializeField]
        TextMeshProUGUI label;
        [SerializeField]
        Button button;
        [SerializeField]
        Image backgroundImage;
        [SerializeField]
        Image coverImage;

        public override void SetupElement(Action data, Dictionary<string, object> args = null)
        {
            base.SetupElement(data, args);
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(()=> { data?.Invoke(); });
        }

        public void SetButtonState(bool state) 
        {
            button.enabled = state;
        }

        public void SetLabel(string label) 
        {
            this.label.text = label;
        }

        public void SetBackgroundSprite(Sprite sprite) 
        {
            this.backgroundImage.sprite = sprite;
        }

        protected override void ApplyArgs(Dictionary<string, object> args = null)
        {
            if (args.TryGetValue("fontSize", out object fontSize))
            {
                label.fontSize = (float)fontSize;
            }

            if(args.TryGetValue("label", out object valueLabel))
                label.text = (string)valueLabel;

            if (args.TryGetValue("font", out object font))
            {
                label.font = Resources.Load<TMP_FontAsset>($"Fonts/{(string)font}");
            }

            if (args.TryGetValue("fontStyle", out object fontStyle))
            {
                label.fontStyle = (FontStyles)fontStyle;
            }

            if (args.TryGetValue("color", out object color))
            {
                label.color = (Color)color;
            }

            if (args.TryGetValue("alignment", out object alignment))
            {
                label.alignment = (TextAlignmentOptions)alignment;
            }

            if (args.TryGetValue("lineSpacing", out object lineSpacing))
            {
                label.lineSpacing = (float)lineSpacing;
            }

            if (args.TryGetValue("backgroundColor", out object backgroundColor))
                backgroundImage.color = (Color)color;

            if (args.TryGetValue("sprite", out object sprite))
                backgroundImage.sprite = (Sprite)sprite;

            if (args.TryGetValue("preserveAspect", out object preserveAspect))
                backgroundImage.preserveAspect = (bool)preserveAspect;

            if (args.TryGetValue("coverImageColor", out object coverImageColor) && null != coverImage)
            {
                Color c = (Color)coverImageColor;
                coverImage.color = new Color(c.r, c.g, c.b, .8f);
            }
            base.ApplyArgs(args);
        }

#if UNITY_EDITOR

        private void OnValidate()
        {
            if (null == backgroundImage)
                backgroundImage = GetComponent<Image>();

            if (null == label)
                label = GetComponentInChildren<TextMeshProUGUI>();

            if (null == button)
                button = GetComponent<Button>();
        }
#endif
    }
}
