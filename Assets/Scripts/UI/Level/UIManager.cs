using System;
using MIIProjekt.Logging;
using NLog;
using UnityEngine;

namespace MIIProjekt.UI.Level
{
    public class UIManager : MonoBehaviour
    {
        private static readonly NLog.Logger Logger = LogManager.GetCurrentClassLogger();

        [SerializeField]
        private GameObject uiObjectGame;

        [SerializeField]
        private GameObject uiObjectGameOver;

        [SerializeField]
        private GameObject uiObjectLevelCompleted;

        [SerializeField]
        private GameObject uiObjectOptionsPauseMenu;

        [SerializeField]
        private GameObject uiObjectPauseMenu;

        [SerializeField]
        private Canvas canvas;

        public GameObject UIObjectGame { get => uiObjectGame; }
        public GameObject UIObjectGameOver { get => uiObjectGameOver; }
        public GameObject UIObjectLevelCompleted { get => uiObjectLevelCompleted; }
        public GameObject UIObjectOptionsPauseMenu { get => uiObjectOptionsPauseMenu; }
        public GameObject UIObjectPauseMenu { get => uiObjectPauseMenu; }

        private void Awake()
        {
            LoggingManager.InitializeLogging();

            if (UIObjectGame == null)
            {
                Logger.Error("UI Object Game is not set. Name of UIManager object: {}", name);
            }

            if (UIObjectGameOver == null)
            {
                Logger.Error("UI Object GameOver is not set. Name of UIManager object: {}", name);
            }

            if (UIObjectLevelCompleted == null)
            {
                Logger.Error("UI Object LevelCompleted is not set. Name of UIManager object: {}", name);
            }

            if (UIObjectOptionsPauseMenu == null)
            {
                Logger.Error("UI Object OptionsPauseMenu is not set. Name of UIManager object: {}", name);
            }

            if (UIObjectPauseMenu == null)
            {
                Logger.Error("UI Object PauseMenu is not set. Name of UIManager object: {}", name);
            }

            if (canvas == null)
            {
                Logger.Error("Cannot initialize children, canvas is not set! Name of UIManager object: {}", name);
                throw new NullReferenceException("Canvas is null.");
            }

            // Workaround to have Awake() called in GUI components
            foreach (RectTransform child in canvas.transform)
            {
                child.gameObject.SetActive(true);
                child.gameObject.SetActive(false);
            }
        }
    }
}
