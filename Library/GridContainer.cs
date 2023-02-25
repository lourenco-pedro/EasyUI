using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EasyUI.Library
{
    [RequireComponent(typeof(CustomGridLayoutGroup))]
    public class GridContainer : UIContainer
    {
        [SerializeField] CustomGridLayoutGroup gridLayoutGroup;

        protected override void ApplyArgs(Dictionary<string, object> args = null)
        {
            if (args.TryGetValue("cellSize", out object cellSize))
                gridLayoutGroup.cellSize = (Vector2)cellSize;

            if (args.TryGetValue("padding", out object padding)) 
                gridLayoutGroup.padding = (RectOffset)padding;

            if (args.TryGetValue("spacing", out object spacing))
                gridLayoutGroup.spacing = (Vector2)spacing;

            if (args.TryGetValue("startCorner", out object startCorner))
                gridLayoutGroup.startCorner = (GridLayoutGroup.Corner)startCorner;

            if(args.TryGetValue("startAxis", out object startAxis))
                gridLayoutGroup.startAxis = (GridLayoutGroup.Axis)startAxis;

            if (args.TryGetValue("childAlignment", out object childAlignment))
                gridLayoutGroup.childAlignment = (TextAnchor)childAlignment;

            if (args.TryGetValue("constraint", out object constraint))
                gridLayoutGroup.constraint = (GridLayoutGroup.Constraint)constraint;

            if (args.TryGetValue("constraitCount", out object constraintCount))
                gridLayoutGroup.constraintCount = (int)constraintCount;

            base.ApplyArgs(args);
        }

#if UNITY_EDITOR
        void OnValidate()
        {
            if (gridLayoutGroup == null)
                gridLayoutGroup = GetComponent<CustomGridLayoutGroup>();
        }
#endif
    }
}
