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
        private Canvas levelCompletedCanvas;

        private Canvas currentlyDisplayedCanvas;

        void Awake()
        {
            instance = this;
            currentlyDisplayedCanvas = null;
            levelCompletedCanvas.enabled = false;
            inGameCanvas.enabled = false;
        }

        private void UpdateDisplay(GameState newGameState)
        {
            if (currentlyDisplayedCanvas != null)
            {
                currentlyDisplayedCanvas.enabled = false;
            }

            switch(newGameState)
            {
                case GameState.GS_GAME:
                    currentlyDisplayedCanvas = inGameCanvas;
                    break;
                case GameState.GS_LEVELCOMPLETED:
                    currentlyDisplayedCanvas = levelCompletedCanvas;
                    break;
                default:
                    currentlyDisplayedCanvas = null;
                    break;
            }

            if (currentlyDisplayedCanvas != null)
            {
                currentlyDisplayedCanvas.enabled = true;
            }
        }
    }
}
