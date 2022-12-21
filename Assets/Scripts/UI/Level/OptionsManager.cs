using System;
using MIIProjekt.Logging;
using NLog;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MIIProjekt.UI.Level
{
    public class OptionsManager : MonoBehaviour
    {
        private static readonly NLog.Logger Logger = LogManager.GetCurrentClassLogger();

        [SerializeField]
        private UIAnimatorController uiAnimatorController;

        [SerializeField]
        private Slider slider;

        [SerializeField]
        private TMP_Dropdown dropdown;

        public void OnValueChangedMasterVolume(float value)
        {
            Logger.Debug("Master volume set new value: {}", value);
            AudioListener.volume = Mathf.Clamp(value, 0.0f, 1.0f);
        }

        public void OnValueChangedGraphics(int value)
        {
            Logger.Debug("Graphics set new value: {} {}", value, (value < QualitySettings.names.Length ? QualitySettings.names[value] : "INVALID"));
            QualitySettings.SetQualityLevel(value);
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

            if (slider != null)
            {
                slider.value = AudioListener.volume;
            }
            else
            {
                Logger.Warn("Slider component is not set on the OptionsManager instance. The default value will not be applied to the slider.");
            }

            if (dropdown != null)
            {
                slider.value = QualitySettings.GetQualityLevel();
            }
            else
            {
                Logger.Warn("Dropdown component is not set on the OptionsManager instance. The default value will not be applied to the slider.");
            }
        }
    }
}
