using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MIIProjekt.UI.MainMenu
{
    public class MainMenuAbout : MonoBehaviour
    {
        [SerializeField]
        private MainMenuComponent mainMenu;
        public void OnBackButtonClicked()
        {
            gameObject.SetActive(false);
            mainMenu.gameObject.SetActive(true);
        }
    }
}
