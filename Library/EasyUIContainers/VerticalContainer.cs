using UnityEngine;
using UnityEngine.UI;

namespace EasyUI.Library
{
    [RequireComponent(typeof(VerticalLayoutGroup))]
    public class VerticalContainer : HorizontalOrVerticalContainer
    {
        private void OnValidate()
        {
            if (horizontalOrVerticalLayoutGroup == null)
                horizontalOrVerticalLayoutGroup = GetComponent<HorizontalOrVerticalLayoutGroup>();
        }
    }
}
