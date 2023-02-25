using System.Collections.Generic;

using UnityEngine;

using EasyUI.Library;

namespace EasyUI.Runtime
{
    [CreateAssetMenu(menuName = "EasyUI/RuntimeDataContainer", fileName = "RuntimeDataContainer")]
    public class SO_EasyUIRuntimeDataContainer : ScriptableObject
    {
        static SO_EasyUIRuntimeDataContainer instance;

        HashSet<UIElement> registeredUIs;

        public static void RegisterUI(UIElement uiToRegister) 
        {
            instance.registeredUIs.Add(uiToRegister);
        }

        public static void RemoveUI(UIElement uiToRemove) 
        {
            if(instance.registeredUIs.Contains(uiToRemove))
                instance.registeredUIs.Remove(uiToRemove);
        }

        [RuntimeInitializeOnLoadMethod]
        public static void SetupInstance() 
        {
            instance = Resources.Load<SO_EasyUIRuntimeDataContainer>("RuntimeDataContainer");

            instance.registeredUIs = new HashSet<UIElement>();
        }
    }
}