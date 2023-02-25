using UnityEngine;

using EasyUI.Library;

namespace EasyUI.Debug
{
    public static class EasyUIConsole
    {
        public static void Log<TargetType>(this TargetType target, string message)
            where TargetType : UIElement
        {
            UnityEngine.Debug.Log($"<color=#4B8BF5>[{target.GetType().Name}][{target.name}]:</color> {message}");
        }

        public static void Log(string id, string message) 
        {
            UnityEngine.Debug.Log($"<color=#4B8BF5>[BaseUI][{id}]:</color> {message}");
        }

        public static void LogError(string id, string message) 
        {
            UnityEngine.Debug.LogError($"<color=#4B8BF5>[BaseUI][{id}]:</color> {message}");
        }
    }
}