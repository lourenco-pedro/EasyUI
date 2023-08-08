using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

namespace EasyUI.Library
{
    [RequireComponent(typeof(ScrollRect))]
    public class ScrollableContainer : VerticalContainer
    {

        public const string ScrollableContainerMovementType = "movementType";
        public const string ScrollableContainerInertia = "inertia";
        public const string ScrollableContainerScrollHorizontal = "scrollHorizontal";
        public const string ScrollableContainerScrollVertical = "scrollVertical";

        [SerializeField] protected ScrollRect scrollRect;

        public override void SetupElement(Dictionary<string, object> args = null)
        {
            scrollRect.viewport = transform.parent as RectTransform;
            scrollRect.content = transform as RectTransform;

            base.SetupElement(args);
        }

        protected override void ApplyArgs(Dictionary<string, object> args = null)
        {
            if (args.TryGetValue("movementType", out object movementType))
                scrollRect.movementType = (ScrollRect.MovementType)movementType;

            if (args.TryGetValue("inertia", out object inertia))
                scrollRect.inertia = (bool)inertia;

            if (args.TryGetValue("scrollHorizontal", out object scrollHorizontal))
                scrollRect.horizontal = (bool)scrollHorizontal;

            if(args.TryGetValue("scrollVertical", out object scrollVertical))
                scrollRect.vertical = (bool)scrollVertical;

            base.ApplyArgs(args);
        }

#if UNITY_EDITOR
        void OnValidate()
        {
            if(scrollRect == null)
                scrollRect = GetComponent<ScrollRect>();   
        }
#endif
    }
}
