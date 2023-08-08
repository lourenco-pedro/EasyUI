using System.Collections.Generic;
using UnityEngine;

namespace EasyUI.Library
{
    [RequireComponent(typeof(UnityEngine.UI.Mask))]
    public class Mask : BackgroundImage
    {
        [SerializeField]
        protected UnityEngine.UI.Mask mask;

        public const string MaskPropertyShowMaskGraphics = "showMaskGraphics";

        protected override void ApplyArgs(Dictionary<string, object> args = null)
        {
            if (args.TryGetValue("showMaskGraphics", out object showMaskGraphics))
                mask.showMaskGraphic = (bool)showMaskGraphics;

            base.ApplyArgs(args);
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            if(null == mask)
                mask = GetComponent<UnityEngine.UI.Mask>();
        }
#endif
    }
}
