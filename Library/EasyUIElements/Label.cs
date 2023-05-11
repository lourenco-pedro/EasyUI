using System.Collections.Generic;

using TMPro;

using UnityEngine;

using EasyUI.Interfaces;

namespace EasyUI.Library
{
  
    public class Label : UIElement<string>
    {
        [SerializeField]
        protected TextMeshProUGUI label;

        public const string FonPropertyt = "font";
        public const string FontSizeProperty = "fontSize";
        public const string FontStyleProperty = "fontStyle";
        public const string ColorProperty = "color";
        public const string AlignmentProperty = "aligment";
        public const string LineSpacingProperty = "lineSpacing";

        public override void SetupElement(string data, Dictionary<string, object> args = null)
        {
            base.SetupElement(data, args);

            label.text = data;
        }

        protected override void ApplyArgs(Dictionary<string, object> args = null)
        {
            if (args.TryGetValue("fontSize", out object fontSize)) 
            {
                label.fontSize = (float)fontSize;
            }

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

            base.ApplyArgs(args);
        }
    }
}