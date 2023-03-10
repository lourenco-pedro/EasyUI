#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using EasyUI.Runtime;

namespace EasyUI.Editor
{
    [CustomEditor(typeof(SO_EasyUIRuntimeDataContainer))]
    public class EasyUIRuntimeDebug : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            if (!Application.isPlaying)
            {
                UnityEditor.EditorGUILayout.HelpBox("There is nothing to show while editor is not in Play mode", MessageType.Warning);
            }
            else 
            {
                SO_EasyUIRuntimeDataContainer current = (SO_EasyUIRuntimeDataContainer)target;
                if (null == current)
                    return;
            }
        }
    }
}
#endif
