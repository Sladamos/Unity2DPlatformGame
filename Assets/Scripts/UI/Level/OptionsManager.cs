using MIIProjekt.Logging;
using NLog;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MIIProjekt.UI.Level
{
    public class OptionsManager : MonoBehaviour
    {
        private static readonly NLog.Logger Logger = LogManager.GetCurrentClassLogger();

        [SerializeField]
        private UIAnimatorController uiAnimatorController;

        [SerializeField]
        private Slider sliderMaster;

        [SerializeField]
        private Slider sliderMusic;

        [SerializeField]
        private Slider sliderEffects;

        [SerializeField]
        private TMP_Dropdown dropdown;

        [SerializeField]
        private AudioSource music;

        [SerializeField]
        private UnityEvent<float> effectsVolumeUpdate;

        private string masterVolumeKey = "masterVolumeOpt";
        private string musicKey = "backgroundMusicOpt";
        private string effectsKey = "effectsVolumeOpt";
        private string graphicsKey = "graphicsOpt";


        public float MasterVolume
        {
            get
            {
                return PlayerPrefs.GetFloat(masterVolumeKey);
            }
            private set
            {
                float newValue = Mathf.Clamp(value, 0.0f, 1.0f);
                PlayerPrefs.SetFloat(masterVolumeKey, newValue);
                AudioListener.volume = newValue;
            }
        }

        public float EffectsVolume
        {
            get
            {
                return PlayerPrefs.GetFloat(effectsKey);
            }
            private set
            {
                float newValue = Mathf.Clamp(value, 0.0f, 1.0f);
                PlayerPrefs.SetFloat(effectsKey, newValue);
                effectsVolumeUpdate.Invoke(newValue);
            }
        }

        public float MusicVolume
        {
            get
            {
                return PlayerPrefs.GetFloat(musicKey);
            }
            private set
            {
                float newValue = Mathf.Clamp(value, 0.0f, 1.0f);
                PlayerPrefs.SetFloat(musicKey, newValue);
                if (music != null)
                {
                    music.volume = newValue;
                }
            }
        }

        public int Graphic
        {
            get
            {
                return PlayerPrefs.GetInt(graphicsKey);
            }
            private set
            {
                PlayerPrefs.SetInt(graphicsKey, value);
                QualitySettings.SetQualityLevel(value);
                dropdown.value = value;
            }
        }

        public void OnValueChangedMasterVolume(float value)
        {
            Logger.Debug("Master volume set new value: {}", value);
            MasterVolume = value;
        }

        public void OnValueChangedMusicVolume(float value)
        {
            MusicVolume = value;
            if (music != null)
            {
                Logger.Debug("Background music volume set new value: {}", value);
            }
            else
            {
                Logger.Debug("Background music is not set!");
            }
        }

        public void OnValueChangedEffectsVolume(float value)
        {
            EffectsVolume = value;
        }

        public void OnValueChangedGraphics(int value)
        {
            Logger.Debug("Graphics set new value: {} {}", value, (value < QualitySettings.names.Length ? QualitySettings.names[value] : "INVALID"));
            Graphic = value;
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

            if (!PlayerPrefs.HasKey(masterVolumeKey))
            {
                MasterVolume = 0.5f;
            }

            if (sliderMaster != null)
            {
                sliderMaster.value = MasterVolume;
            }
            else
            {
                Logger.Warn("Slider component is not set on the OptionsManager instance. The default value will not be applied to the slider.");
            }

            if (!PlayerPrefs.HasKey(musicKey))
            {
                MusicVolume = 0.5f;
            }
            MusicVolume = MusicVolume;

            if (sliderMusic != null)
            {
                sliderMusic.value = MusicVolume;
            }
            else
            {
                Logger.Warn("It's problem with slider music");
            }

            if (dropdown != null)
            {
                if (!PlayerPrefs.HasKey(graphicsKey))
                {
                    Graphic = 3;
                }
                else
                {
                    Graphic = Graphic;
                }
            }
            else
            {
                Logger.Warn("Dropdown component is not set on the OptionsManager instance. The default value will not be applied to the slider.");
            }
        }

        private void Start()
        {
            if (!PlayerPrefs.HasKey(effectsKey))
            {
                EffectsVolume = 0.5f;
            }

            EffectsVolume = EffectsVolume;

            if (sliderEffects != null)
            {
                sliderEffects.value = EffectsVolume;
            }
            else
            {
                Logger.Warn("It's problem with slider effects");
            }
        }
    }
}
