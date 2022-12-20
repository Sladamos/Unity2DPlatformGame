using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MIIProjekt.UI.Level
{
    public class LevelButtonsHandler : MonoBehaviour
    {
        public void OnClickedButtonResume()
        {
            throw new NotImplementedException();
        }

        public void OnClickedButtonRestart()
        {
            SceneManager.LoadScene((SceneManager.GetActiveScene().name));
        }

        public void OnClickedButtonMainMenu()
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
