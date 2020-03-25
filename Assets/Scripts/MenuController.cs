using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public static MenuController instance;

    public GameObject mainMenu, deathMenu, skinsMenu;

    public Button menuButton, skinsButton;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        menuButton.onClick.AddListener(() => OpenMenu(MenuType.Main));
        skinsButton.onClick.AddListener(() => OpenMenu(MenuType.Skins));
        OpenMenu(MenuType.Main);
    }

    public void OpenMenu (MenuType menu)
    {
        CloseMenus();
        switch (menu)
        {
            case MenuType.Main:
                mainMenu.SetActive(true);
                break;
            case MenuType.Death:
                deathMenu.SetActive(true);
                break;
            case MenuType.Skins:
                skinsMenu.SetActive(true);
                break;
            default:
                break;
        }
    }

    public void CloseMenus ()
    {
        mainMenu.SetActive(false);
        deathMenu.SetActive(false);
        skinsMenu.SetActive(false);
    }

    private void OnDestroy()
    {
        menuButton.onClick.RemoveListener(() => OpenMenu(MenuType.Main));
        skinsButton.onClick.RemoveListener(() => OpenMenu(MenuType.Skins));
    }
}

public enum MenuType { Main, Death, Skins }