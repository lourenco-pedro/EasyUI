using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

namespace EasyUI.Library
{
    public class HorizontalOrVerticalContainer : UIContainer
    {
        [SerializeField] protected HorizontalOrVerticalLayoutGroup horizontalOrVerticalLayoutGroup;

        protected override void ApplyArgs(Dictionary<string, object> args = null)
        {

            if (args.TryGetValue("spacing", out object spacing))
                horizontalOrVerticalLayoutGroup.spacing = (float)spacing;

            if (args.TryGetValue("childAlignment", out object childAlignment))
                horizontalOrVerticalLayoutGroup.childAlignment = (TextAnchor)childAlignment;

            if (args.TryGetValue("childControlWidth", out object childControlWidth))
                horizontalOrVerticalLayoutGroup.childControlWidth = (bool)childControlWidth;

            if(args.TryGetValue("childControlHeigh", out object childControlHeight))
                horizontalOrVerticalLayoutGroup.childControlHeight = (bool)childControlHeight;

            if (args.TryGetValue("childScaleWidth", out object childScaleWidth))
                horizontalOrVerticalLayoutGroup.childScaleWidth = (bool)childScaleWidth;

            if (args.TryGetValue("childScaleHeight", out object childScaleHeight))
                horizontalOrVerticalLayoutGroup.childScaleHeight = (bool)childScaleHeight;

            if (args.TryGetValue("childForceExpandWidth", out object childForceExpandWidth))
                horizontalOrVerticalLayoutGroup.childForceExpandWidth = (bool)childForceExpandWidth;

            if (args.TryGetValue("childForceExpandHeight", out object childForceExpandHeight))
                horizontalOrVerticalLayoutGroup.childForceExpandHeight = (bool)childForceExpandHeight;

            if (args.TryGetValue("padding", out object padding))
                horizontalOrVerticalLayoutGroup.padding = (RectOffset)padding;
            
            base.ApplyArgs(args);
        }
    }
}