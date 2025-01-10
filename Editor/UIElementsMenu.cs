using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace JDoddsNAIT.ThemedUI.Editor 
{
    public static class UIElementsMenu
    {
        const string
            MENU_NAME = "GameObject/UI/",
            WINDOW = "Window",
            NAVIGATOR = "Navigator";
        const int PRIORITY_OFFSET = 0;

        [MenuItem(MENU_NAME + WINDOW, validate = true)]
        static bool CreateWindowValidate()
        {
            return ThemedUIElementsMenu.InstantiatePrefabValidate(WINDOW, string.Empty);
        }
        [MenuItem(MENU_NAME + WINDOW, priority = PRIORITY_OFFSET + 0)]
        static void CreateWindow()
        {
            ThemedUIElementsMenu.InstantiatePrefab(WINDOW, string.Empty);
        }

        [MenuItem(MENU_NAME + NAVIGATOR, validate = true)]
        static bool CreateNavigatorValidate()
        {
            return ThemedUIElementsMenu.InstantiatePrefabValidate(NAVIGATOR, string.Empty);
        }
        [MenuItem(MENU_NAME + NAVIGATOR, priority = PRIORITY_OFFSET + 1)]
        static void CreateNavigator()
        {
            ThemedUIElementsMenu.InstantiatePrefab(NAVIGATOR, string.Empty);
        }
    }
}
