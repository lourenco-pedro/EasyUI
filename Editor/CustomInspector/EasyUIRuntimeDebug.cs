#if UNITY_EDITOR

using System.Linq;
using System.Collections.Generic;

using UnityEngine;
using UnityEditor;

using EasyUI.Runtime;
using EasyUI.Library;
using EasyUI.Page;

namespace EasyUI.Editor
{
    [CustomEditor(typeof(SO_EasyUIRuntimeDataContainer))]
    public class EasyUIRuntimeDebug : UnityEditor.Editor
    {

        Vector2 scrollview_activeUIPages;
        Dictionary<object, bool> foldableElements;
        int indentLevel = 0;

        private void OnEnable()
        {
            foldableElements = new Dictionary<object, bool>();
        }

        public override void OnInspectorGUI()
        {

            Repaint();

            RemoveInvalidFoldableElements();

            if (!Application.isPlaying)
            {
                UnityEditor.EditorGUILayout.HelpBox("There is nothing to show while editor is not in Play mode", MessageType.Warning);
            }
            else 
            {
                SO_EasyUIRuntimeDataContainer current = (SO_EasyUIRuntimeDataContainer)target;
                if (null == current)
                    return;

                DrawDefaultTitle("Active EasyUI Pages", iconContent: "d_PreMatCube");
                GUILayout.BeginVertical(GUI.skin.box, GUILayout.MaxHeight(200f), GUILayout.MinHeight(200f), GUILayout.Width(Screen.width));
                { 
                    DrawCollapsableItems();
                }
                GUILayout.EndVertical();
            }
        }

        void DrawDefaultTitle(string title, string iconContent = "") 
        {
            GUILayout.BeginHorizontal(GUI.skin.box);
            GUILayout.Label(new GUIContent { text = "\t" + title, image = EditorGUIUtility.IconContent(iconContent).image });
            GUILayout.EndHorizontal();
        }

        void TryAddFoldableElement(object item) 
        {
            if (!foldableElements.TryGetValue(item, out bool folded))
                foldableElements.Add(item, false);
        }

        void RemoveInvalidFoldableElements() 
        {
            foldableElements = foldableElements.Where(f => f.Key != null).ToDictionary(x => x.Key, x => x.Value);
        }

        void DrawCollapsableItems() 
        {
            EasyUIPage[] pages = EasyUIPage.GetActivePages();
            GUILayout.Label("\t");
            scrollview_activeUIPages = GUILayout.BeginScrollView(scrollview_activeUIPages);
            {
                foreach (EasyUIPage page in pages)
                {
                    TryAddFoldableElement(page);

                    DrawCollapsableItem(page, page.Name);
                }
            }
            GUILayout.EndScrollView();
        }

        void DrawCollapsableItemRecursively(UIElement[] elements) 
        {
            indentLevel++;
            foreach (UIElement element in elements) 
            {
                TryAddFoldableElement(element);
                DrawCollapsableItem(element, element.name);
            }
            indentLevel--;
        }

        void DrawCollapsableItem(object foldableElementReference, string name) 
        {
            if ((foldableElementReference as EasyUIPage) != null || foldableElementReference.GetType().IsSubclassOf(typeof(UIContainer)))
            {
                bool folded = false;
                GUILayout.BeginHorizontal();
                {
                    GUILayout.Space(indentLevel * 20);
                    folded = foldableElements[foldableElementReference];
                    if (GUILayout.Button(new GUIContent { image = folded == true ? EditorGUIUtility.IconContent("d_Toolbar Plus More").image : EditorGUIUtility.IconContent("d_Toolbar Minus").image }, GUI.skin.label, GUILayout.Width(15)))
                    {
                        foldableElements[foldableElementReference] = !foldableElements[foldableElementReference];
                    }

                    GUILayout.Label(name);
                }

                GUILayout.EndHorizontal();
                if (!folded)
                {
                    EasyUIPage page = foldableElementReference as EasyUIPage;
                    if(page != null) 
                    {
                        UIElement[] roots = page.GetPageRoot();
                        DrawCollapsableItemRecursively(roots);
                    }

                    UIContainer container = foldableElementReference as UIContainer;
                    if (page == null && container != null) 
                    {
                        UIElement[] children = container.GetChildren().ToUIElements();
                        DrawCollapsableItemRecursively(children);
                    }
                }
            }
            else
            {
                GUILayout.BeginHorizontal();
                GUILayout.Space(indentLevel * 20);
                GUILayout.Label(new GUIContent { image = EditorGUIUtility.IconContent("d_PreMatCube").image, text = name });
                GUILayout.EndHorizontal();
            }
        }
    }
}
#endif
