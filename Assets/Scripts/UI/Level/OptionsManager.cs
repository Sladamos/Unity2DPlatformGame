using System;
using MIIProjekt.Logging;
using NLog;
using UnityEngine;

namespace MIIProjekt.UI.Level
{
    public class OptionsManager : MonoBehaviour
    {
        private static readonly NLog.Logger Logger = LogManager.GetCurrentClassLogger();

        [SerializeField]
        private UIAnimatorController uiAnimatorController;

        public void OnValueChangedMasterVolume(float value)
        {
            Logger.Debug("Master volume set new value: {}", value);
            throw new NotImplementedException();
        }

        public void OnValueChangedGraphics(int value)
        {
            Logger.Debug("Graphics set new value: {}", value);
            throw new NotImplementedException();
        }

        public void OnBackButtonClicked()
        {
            Logger.Debug("Clicked options menu back button");
            uiAnimatorController?.InvokeOptionsBackClicked();
        }

        private void Awake()
        {
            LoggingManager.InitializeLogging();

            if (uiAnimatorController == null)
            {
                Logger.Warn("UIAnimatorController is not set on OptionsManager instance. GameObject name = {}", name);
            }
        }
    }
}
