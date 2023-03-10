using EasyUI.Library;
using EasyUI.Runtime;
using System.Collections.Generic;

using UnityEngine;
using static UnityEngine.UI.ContentSizeFitter;

namespace EasyUI.Page
{
    public class EasyUIPage_Home : EasyUIPage
    {
        public override List<string> OnDrawPage()
        {
            List<string> elements = new List<string>();

            ColorUtility.TryParseHtmlString("#5e6472", out Color darkGrey_c5e6472);
            ColorUtility.TryParseHtmlString("#f54242", out Color lightRed_f54242);
            ColorUtility.TryParseHtmlString("#64b6ac", out Color lightBlue_64b6ac);

            BuilderUI.AddUIElement<BackgroundImage, Sprite>(null, BuilderUI.GetScreenMargins(), args: new Dictionary<string, object> 
            {
                { "size", "fit" },
                { "offsetMin", new Vector2(-10, -10) },
                { "offsetMax", new Vector2(10, 10) },
            }, onElementCreated: background => elements.Add(background.Id));

            BuilderUI.AddContainer<ScrollableContainer>(BuilderUI.GetScreenMargins(), args: new Dictionary<string, object>
            {
                { "childForceExpandHeight", false },
                { "childControlWidth", true },
                { "childAnchor", TextAnchor.UpperCenter },
                { "spacing", 10f },
                { "size", "fitWidth" },
                { "offsetMin", Vector2.zero },
                { "offsetMax", Vector2.zero },
                { "useContentSizeFitter", true },
                { "verticalFit", FitMode.MinSize },
                { "scrollType", UnityEngine.UI.ScrollRect.MovementType.Clamped },
                { "scrollHorizontal", false },
                { "scrollVertical", true },
            }, onElementCreated: (parent) =>
            {

                elements.Add(parent.Id);

                BuilderUI.AddUIElement<Label, string>("Olá\nPedro", parent, new Dictionary<string, object>
                {
                    { "color", darkGrey_c5e6472 },
                    { "font", "SEGOEUIL SDF" },
                    { "fontSize", 150f },
                    { "height", 309f },
                    { "lineSpacing", -35f },
                    { "fontStyle", TMPro.FontStyles.Normal },
                    { "alignment", TMPro.TextAlignmentOptions.TopLeft }
                }, onElementCreated: nameLabel => elements.Add(nameLabel.Id));

                BuilderUI.AddContainer<HorizontalContainer>(
                 args: new Dictionary<string, object>
                {
                    { "size", "fitWidth" },
                    { "useContentSizeFitter", true },
                    { "verticalFit", FitMode.MinSize }
                }, parent: parent, onElementCreated: (horizontal) =>
                {

                    elements.Add(horizontal.Id);

                    BuilderUI.AddUIElement<Label, string>("Pedro Lourenço", horizontal, new Dictionary<string, object>
                    {
                        { "color", darkGrey_c5e6472 },
                        { "font", "SEGOEUIL SDF" },
                        { "fontStyle", TMPro.FontStyles.Normal },
                        { "alignment", TMPro.TextAlignmentOptions.TopLeft },
                        { "fontSize", 90f },
                        { "height", 200f }
                    }, onElementCreated: fullName => elements.Add(fullName.Id));

                    BuilderUI.AddContainer<UIContainer>(args: new Dictionary<string, object>
                    {
                        { "pivot", new Vector2(0.5f, 1) },
                        { "heihgt", 200f }
                    },
                    parent: horizontal,
                    onElementCreated: (settingsIcnContainer) =>
                    {
                        elements.Add(settingsIcnContainer.Id);
                        BuilderUI.AddUIElement<SimpleButton, System.Action>(() => { UnityEngine.Debug.Log("Teste!"); }, parent: settingsIcnContainer, args: new Dictionary<string, object>()
                        {
                            { "label", string.Empty },
                            { "sprite", SO_ResourcesLibrary.GetSprite("icn_settings_512") },
                            { "preserveAspect", true }
                        });
                    });

                });


                BuilderUI.AddContainer<HorizontalContainer>(parent: parent, onElementCreated: (coursesHeader) =>
                {
                    elements.Add(coursesHeader.Id);
                    BuilderUI.AddUIElement<Label, string>("Seus cursos", coursesHeader, new Dictionary<string, object>
                    {
                        { "color", darkGrey_c5e6472 },
                        { "font", "SEGOEUIL SDF" },
                        { "fontStyle", TMPro.FontStyles.Normal },
                        { "alignment", TMPro.TextAlignmentOptions.TopLeft },
                        { "fontSize", 70f },
                        { "height", 115f }
                    }, onElementCreated: yourCourses => elements.Add(yourCourses.Id));

                    BuilderUI.AddUIElement<Label, string>("ver todos", coursesHeader, new Dictionary<string, object>
                    {
                        { "color", lightBlue_64b6ac },
                        { "font", "SEGOEUIL SDF" },
                        { "fontStyle", TMPro.FontStyles.Normal },
                        { "alignment", TMPro.TextAlignmentOptions.TopRight },
                        { "fontSize", 45f },
                        { "height", 115f }
                    }, onElementCreated: seeAll => elements.Add(seeAll.Id));
                }, args: new Dictionary<string, object>
                {
                    { "size", "fitWidth" },
                    { "height", 150f },
                    { "childControlWidth", true }
                });

                BuilderUI.AddContainer<GridContainer>(parent, new Dictionary<string, object>
                {
                    { "cellSize", new Vector2(415, 300) },
                    { "backgroundColor", Color.clear },
                    { "size", "fitWidth" },
                    { "useContentSizeFitter", true},
                    { "verticalFit", FitMode.MinSize },
                    { "childAlignment", TextAnchor.MiddleCenter },
                    { "spacing", new Vector2(15, 15) }
                },
                onElementCreated: (coursesContainer) =>
                {
                    elements.Add(coursesContainer.Id);
                    List<string> labels = new List<string>
                    {
                        "CCAA",
                        "COMON",
                        "SUPER GEEKS",
                    };

                    List<System.Action> actions = new List<System.Action>();
                    foreach (string label in labels)
                    {
                        actions.Add(() => { UnityEngine.Debug.Log($"{label}!"); });
                    }

                    int index = 0;

                    BuilderUI.AddUIElements<SimpleButton, System.Action>(actions, coursesContainer, args: new Dictionary<string, object>
                    {
                        { "color", Color.white },
                        { "font", "SEGOEUIL SDF" },
                        { "fontStyle", TMPro.FontStyles.Bold },
                        { "fontSize", 70f },
                        { "alignment", TMPro.TextAlignmentOptions.Center },
                        { "sprite", SO_ResourcesLibrary.GetSprite("background_02") },
                        { "coverImageColor", lightRed_f54242 }
                    }, onElementCreated: (courseBtn) =>
                    {
                        elements.Add(courseBtn.Id);
                        BuilderUI.AddMask(courseBtn);
                        courseBtn.SetLabel(labels[index]);
                        index++;
                    });
                });


                BuilderUI.AddContainer<HorizontalContainer>(parent: parent, onElementCreated: (teachersHeader) =>
                {

                    elements.Add(teachersHeader.Id);

                    BuilderUI.AddUIElement<Label, string>("Professores", teachersHeader, new Dictionary<string, object>
                    {
                        { "color", darkGrey_c5e6472 },
                        { "font", "SEGOEUIL SDF" },
                        { "fontStyle", TMPro.FontStyles.Normal },
                        { "alignment", TMPro.TextAlignmentOptions.TopLeft },
                        { "fontSize", 70f },
                        { "height", 115f }
                    }, onElementCreated: teachersTitle => elements.Add(teachersTitle.Id));

                    BuilderUI.AddUIElement<Label, string>("ver todos", teachersHeader, new Dictionary<string, object>
                    {
                        { "color", lightBlue_64b6ac },
                        { "font", "SEGOEUIL SDF" },
                        { "fontStyle", TMPro.FontStyles.Normal },
                        { "alignment", TMPro.TextAlignmentOptions.TopRight },
                        { "fontSize", 45f },
                        { "height", 115f }
                    }, onElementCreated: seeAll => elements.Add(seeAll.Id));
                }, args: new Dictionary<string, object>
                {
                    { "size", "fitWidth" },
                    { "height", 150f },
                    { "childControlWidth", true }
                });

                BuilderUI.AddContainer<GridContainer>(parent: parent, onElementCreated: (teachersContainer) =>
                {

                    elements.Add(teachersContainer.Id);

                    for (int i = 0; i < 6; i++)
                    {
                        BuilderUI.AddUIElement<BackgroundImage, Sprite>(null, teachersContainer, args: new Dictionary<string, object>
                        {
                            { "color", Color.red }
                        }, onElementCreated: (teacherButton) => { BuilderUI.AddMask(teacherButton, "circle"); });
                    }

                }, args: new Dictionary<string, object>
                {
                    { "cellSize", new Vector2(250, 250) },
                    { "backgroundColor", Color.clear },
                    { "size", "fitWidth" },
                    { "height", 500f },
                    { "spacing", Vector2.one * 25f },
                    { "childAlignment", TextAnchor.MiddleCenter },
                    { "constraint", UnityEngine.UI.GridLayoutGroup.Constraint.FixedRowCount },
                    { "constraintCount", 3 }
                });



                BuilderUI.AddContainer<HorizontalContainer>(parent: parent, onElementCreated: (teachersHeader) =>
                {

                    elements.Add(teachersHeader.Id);

                    BuilderUI.AddUIElement<Label, string>("Alunos", teachersHeader, new Dictionary<string, object>
                    {
                        { "color", darkGrey_c5e6472 },
                        { "font", "SEGOEUIL SDF" },
                        { "fontStyle", TMPro.FontStyles.Normal },
                        { "alignment", TMPro.TextAlignmentOptions.TopLeft },
                        { "fontSize", 70f },
                        { "height", 115f }
                    }, onElementCreated: students => elements.Add(students.Id));

                    BuilderUI.AddUIElement<Label, string>("ver todos", teachersHeader, new Dictionary<string, object>
                    {
                        { "color", lightBlue_64b6ac },
                        { "font", "SEGOEUIL SDF" },
                        { "fontStyle", TMPro.FontStyles.Normal },
                        { "alignment", TMPro.TextAlignmentOptions.TopRight },
                        { "fontSize", 45f },
                        { "height", 115f }
                    }, onElementCreated: seeAll => elements.Add(seeAll.Id));
                }, args: new Dictionary<string, object>
                {
                    { "size", "fitWidth" },
                    { "height", 150f },
                    { "childControlWidth", true }
                });

                BuilderUI.AddContainer<GridContainer>(parent: parent, onElementCreated: (teachersContainer) =>
                {
                    elements.Add(teachersContainer.Id);

                    for (int i = 0; i < 6; i++)
                    {
                        BuilderUI.AddUIElement<BackgroundImage, Sprite>(null, teachersContainer, args: new Dictionary<string, object>
                        {
                            { "color", Color.red }
                        }, onElementCreated: (teacherButton) =>
                        {
                            elements.Add(teacherButton.Id);
                            BuilderUI.AddMask(teacherButton, "circle");
                            teacherButton.AddTag("teacher");
                        });
                    }

                }, args: new Dictionary<string, object>
                {
                    { "cellSize", new Vector2(250, 250) },
                    { "backgroundColor", Color.clear },
                    { "size", "fitWidth" },
                    { "height", 500f },
                    { "spacing", Vector2.one * 25f },
                    { "childAlignment", TextAnchor.MiddleCenter },
                    { "constraint", UnityEngine.UI.GridLayoutGroup.Constraint.FixedRowCount },
                    { "constraintCount", 3 }
                });

                BuilderUI.AddUIElement<SimpleButton, System.Action>(() => { EasyUIPage.Pop(); }, parent, new Dictionary<string, object> 
                {
                    { "label", "close" }
                });
            });

            return elements;
        }
    }
}
