using UnityEngine;
using UnityEngine.SceneManagement;

namespace MIIProjekt.UI.MainMenu
{
    public class MainMenuComponent : MonoBehaviour
    {
        public void OnClickedButtonLevel1()
        {
            Debug.Log("On clicked level 1");
            SceneManager.LoadScene("Level1");
        }
        
        public void OnClickedButtonLevel2()
        {
            Debug.Log("On clicked level 2");
        }

        public void OnClickedButtonExitGame()
        {
            Debug.Log("Exit game");
            Application.Quit();
        }
    }
}
