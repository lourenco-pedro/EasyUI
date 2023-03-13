using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;

namespace EasyUI.Library
{
    public class BackgroundImage : UIElement<Sprite>
    {
        [SerializeField]
        protected Image image;

        public override void SetupElement(Sprite data, Dictionary<string, object> args = null)
        {
            base.SetupElement(data, args);
            image.sprite = data;
        }

        protected override void ApplyArgs(Dictionary<string, object> args = null)
        {
            base.ApplyArgs(args);

            if (args.TryGetValue("color", out object color))
                image.color = (Color)color;

            if (args.TryGetValue("sprite", out object sprite))
                image.sprite = (Sprite)sprite;
        }

#if UNITY_EDITOR

        void OnValidate() 
        {
            if (null == image)
                image = GetComponent<Image>();
        }
#endif
    }
}
