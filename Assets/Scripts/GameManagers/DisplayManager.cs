using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MIIProjekt.GameManagers
{
    public class DisplayManager : MonoBehaviour
    {
        public static DisplayManager instance;

        [SerializeField]
        private Canvas inGameCanvas;

        [SerializeField]
        private Canvas pauseMenuCanvas;
        void Awake()
        {
            instance = this;
        }

        private void UpdateDisplay()
        {
            if (GameManager.instance.IsGameCurrentlyPlayed())
            {
                inGameCanvas.enabled = true;
            }
            else
            {
                inGameCanvas.enabled = false;
            }

            if (GameManager.instance.IsGameCurrentlyPaused())
            {
                pauseMenuCanvas.enabled = true;
            }
            else
            {
                pauseMenuCanvas.enabled = false;
            }
        }

    }
}
