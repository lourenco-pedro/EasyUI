using UnityEngine;

namespace EasyUI
{
    public static class RectTransformExtension
    {
        public static Vector3 GetChildAnchorPosition(this RectTransform target, TextAnchor anchor) 
        {

            if(target.parent == null)
                return Vector2.zero;

            RectTransform parent = (RectTransform)target.parent;
            Rect parentRect = parent.GetWorldRect();

            switch (anchor) 
            {
                case TextAnchor.UpperLeft:
                    return new Vector2(parentRect.xMin, parentRect.yMax);
                case TextAnchor.UpperRight:
                    return new Vector2(parentRect.xMax, parentRect.yMax);
                case TextAnchor.UpperCenter:
                    return new Vector2(parentRect.center.x, parentRect.yMax);
                case TextAnchor.MiddleLeft:
                    return new Vector2(parentRect.xMin, parentRect.center.y);
                case TextAnchor.MiddleRight:
                    return new Vector2(parentRect.xMax, parentRect.center.y);
                case TextAnchor.LowerLeft:
                    return new Vector2(parentRect.xMin, parentRect.yMin);
                case TextAnchor.LowerRight:
                    return new Vector2(parentRect.xMax, parentRect.yMin);
                case TextAnchor.LowerCenter:
                    return new Vector2(parentRect.center.x, parentRect.yMin);
                default:
                case TextAnchor.MiddleCenter:
                    return parentRect.center;
            }
        }

        public static Rect GetWorldRect(this RectTransform rectTransform)
        {
            // This returns the world space positions of the corners in the order
            // [0] bottom left,
            // [1] top left
            // [2] top right
            // [3] bottom right
            var corners = new Vector3[4];
            rectTransform.GetWorldCorners(corners);

            Vector2 min = corners[0];
            Vector2 max = corners[2];
            Vector2 size = max - min;

            return new Rect(min, size);
        }
    }
}
