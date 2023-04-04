using UnityEngine;
using UnityEngine.UI;

namespace EasyUI.Library
{
    [RequireComponent(typeof(HorizontalLayoutGroup))]
    public class HorizontalContainer : HorizontalOrVerticalContainer
    {
        private void OnValidate()
        {
            if(horizontalOrVerticalLayoutGroup == null)
                horizontalOrVerticalLayoutGroup = GetComponent<HorizontalOrVerticalLayoutGroup>();
        }
    }
}
