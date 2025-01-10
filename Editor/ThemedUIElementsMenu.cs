using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

namespace JDoddsNAIT.ThemedUI.Editor
{
    public class ThemedUIElementsMenu : ScriptableObject
    {
        static bool InstantiatePrefabValidate(string prefabName)
        {
            return Resources.Load(prefabName) != null;
        }

        static GameObject InstantiatePrefab(string prefabName)
        {
            // double work. fix (maybe) later
            GameObject prefab = Resources.Load(prefabName) as GameObject;

            Transform canvas = GetCanvas();

            GameObject newObj = null;
            if (prefab != null)
            {
                newObj = Instantiate(prefab, canvas);
                newObj.name = prefabName;
                Undo.RegisterCreatedObjectUndo(newObj, $"Create {prefabName}");
            }
            return newObj;
        }

        private static Transform GetCanvas()
        {
            Transform canvas = null;
            bool foundCanvas = false;

            // look through the selection for a canvas component.
            if (Selection.transforms.Length > 0)
            {
                for (int i = 0; i < Selection.transforms.Length && foundCanvas is false; i++)
                {
                    var component = Selection.transforms[i].GetComponentInChildren<Canvas>();
                    foundCanvas = component != null;
                    if (foundCanvas)
                        canvas = component.transform;
                }
            }

            if (!foundCanvas)
            {
                // find a canvas somewhere in the scene
                var c = FindObjectOfType<Canvas>();
                if (c != null)
                    canvas = c.transform;
                else if (EditorApplication.ExecuteMenuItem("GameObject/UI/Canvas"))
                    canvas = FindObjectOfType<Canvas>().transform;
                else
                    canvas = null;
            }

            Debug.Assert(canvas != null);

            return canvas;
        }

        const string // Menu & prefab names
            MENU_NAME = "GameObject/UI/Themed Elements/",
            IMAGE = "Image",
            TEXT = "Text",
            PANEL = "Panel",
            TOGGLE = "Toggle",
            SLIDER = "Slider",
            SCROLL_BAR = "Scrollbar",
            SCROLL_VIEW = "Scroll View",
            BUTTON = "Button",
            DROPDOWN = "Dropdown",
            INPUT_FIELD = "Input Field";

        const int PRIORITY_OFFSET = 7;

        [MenuItem(MENU_NAME + IMAGE, validate = true)]
        static bool CreateThemedImageValidate()
        {
            return InstantiatePrefabValidate(IMAGE);
        }
        [MenuItem(MENU_NAME + IMAGE, priority = PRIORITY_OFFSET + 0)]
        static void CreateThemedImage()
        {
            InstantiatePrefab(IMAGE);
        }

        [MenuItem(MENU_NAME + TEXT, validate = true)]
        static bool CreateThemedTextValidate()
        {
            return InstantiatePrefabValidate(TEXT);
        }
        [MenuItem(MENU_NAME + TEXT, priority = PRIORITY_OFFSET + 1)]
        static void CreateThemedText()
        {
            InstantiatePrefab(TEXT);
        }

        [MenuItem(MENU_NAME + PANEL, validate = true)]
        static bool CreateThemedPanelValidate()
        {
            return InstantiatePrefabValidate(PANEL);
        }
        [MenuItem(MENU_NAME + PANEL, priority = PRIORITY_OFFSET + 2)]
        static void CreateThemedPanel()
        {
            InstantiatePrefab(PANEL);
        }

        [MenuItem(MENU_NAME + TOGGLE, validate = true)]
        static bool CreateThemedToggleValidate()
        {
            return InstantiatePrefabValidate(TOGGLE);
        }
        [MenuItem(MENU_NAME + TOGGLE, priority = PRIORITY_OFFSET + 3)]
        static void CreatedThemedToggle()
        {
            InstantiatePrefab(TOGGLE);
        }

        [MenuItem(MENU_NAME + SLIDER, validate = true)]
        static bool CreatedThemedSliderValidate()
        {
            return InstantiatePrefabValidate(SLIDER);
        }
        [MenuItem(MENU_NAME + SLIDER, priority = PRIORITY_OFFSET + 4)]
        static void CreateThemedSlider()
        {
            InstantiatePrefab(SLIDER);
        }

        [MenuItem(MENU_NAME + SCROLL_BAR, validate = true)]
        static bool CreateThemedScrollBarValidate()
        {
            return InstantiatePrefabValidate(SCROLL_BAR);
        }
        [MenuItem(MENU_NAME+ SCROLL_BAR, priority = PRIORITY_OFFSET + 5)]
        static void CreateThemedScrollBar()
        {
            InstantiatePrefab(SCROLL_BAR);
        }

        [MenuItem(MENU_NAME + SCROLL_VIEW, validate = true)]
        static bool CreateThemedScrollViewValidate()
        {
            return InstantiatePrefabValidate(SCROLL_VIEW);
        }
        [MenuItem(MENU_NAME + SCROLL_VIEW, priority = PRIORITY_OFFSET + 6)]
        static void CreateThemedScrollView()
        {
            InstantiatePrefab(SCROLL_VIEW);
        }

        [MenuItem(MENU_NAME + BUTTON, validate = true)]
        static bool CreateThemedButtonValidate()
        {
            return InstantiatePrefabValidate(BUTTON);
        }
        [MenuItem(MENU_NAME + BUTTON, priority = PRIORITY_OFFSET + 7)]
        static void CreateThemedButton()
        {
            InstantiatePrefab(BUTTON);
        }

        [MenuItem(MENU_NAME + DROPDOWN, validate = true)]
        static bool CreateThemedDropdownValidate()
        {
            return InstantiatePrefabValidate(DROPDOWN);
        }
        [MenuItem(MENU_NAME + DROPDOWN, priority = PRIORITY_OFFSET + 8)]
        static void CreateThemedDropdown()
        {
            InstantiatePrefab(DROPDOWN);
        }

        [MenuItem(MENU_NAME + INPUT_FIELD, validate = true)]
        static bool CreateThemedInputFieldValidate()
        {
            return InstantiatePrefabValidate(INPUT_FIELD);
        }
        [MenuItem(MENU_NAME + INPUT_FIELD, priority = PRIORITY_OFFSET + 9)]
        static void CreateThemedInputField()
        {
            InstantiatePrefab(INPUT_FIELD);
        }
    }
}