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
        
        Dictionary<object, bool> elementsSelection;
        object lastSelectedItem;

        Texture2D texture2D_selection;
        
        int indentLevel = 0;

        private void OnEnable()
        {
            foldableElements = new Dictionary<object, bool>();
            elementsSelection = new Dictionary<object, bool>();

            ColorUtility.TryParseHtmlString("#4B8BF5", out Color color_normal);

            texture2D_selection = new Texture2D(1, 1);
            texture2D_selection.SetPixel(0, 0, color_normal);
            texture2D_selection.Apply();
        }

        public override void OnInspectorGUI()
        {

            Repaint();

            RemoveInvalidDictionaryElements();

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

        void TryAddElementSelection(object item) 
        {
            if (!elementsSelection.TryGetValue(item, out bool selected))
                elementsSelection.Add(item, false);
        }

        void RemoveInvalidDictionaryElements() 
        {
            foldableElements = foldableElements.Where(f => f.Key != null).ToDictionary(x => x.Key, x => x.Value);
            elementsSelection = elementsSelection.Where(f => f.Key != null).ToDictionary(x => x.Key, x => x.Value);
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
                    TryAddElementSelection(page);

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
                TryAddElementSelection(element);
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

                    GUIStyle skin_selectedLabel = new GUIStyle(GUI.skin.label);
                    skin_selectedLabel.normal.background = texture2D_selection;
                    GUIStyle skin = elementsSelection[foldableElementReference] ? skin_selectedLabel : GUI.skin.label;

                    if (GUILayout.Button(name, skin)) 
                    {
                        if(lastSelectedItem != null)
                            elementsSelection[lastSelectedItem] = false;
                        
                        lastSelectedItem = foldableElementReference;
                        elementsSelection[lastSelectedItem] = true;

                        if(foldableElementReference.GetType().IsSubclassOf(typeof(UIElement)))
                            EditorGUIUtility.PingObject(lastSelectedItem as UIElement);
                    }
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

                GUIStyle skin_selectedLabel = new GUIStyle(GUI.skin.label);
                skin_selectedLabel.normal.background = texture2D_selection;
                GUIStyle skin = elementsSelection[foldableElementReference] ? skin_selectedLabel : GUI.skin.label;

                if (GUILayout.Button(new GUIContent { image = EditorGUIUtility.IconContent("d_PreMatCube").image, text = name }, skin))
                {
                    if(lastSelectedItem != null)
                        elementsSelection[lastSelectedItem] = false;
                    
                    lastSelectedItem = foldableElementReference;
                    elementsSelection[lastSelectedItem] = true;
                    EditorGUIUtility.PingObject(lastSelectedItem as UIElement);
                }

                GUILayout.EndHorizontal();
            }
        }
    }
}
#endif
