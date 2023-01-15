using System.Collections;
using System.Collections.Generic;
using MIIProjekt.UI.Level;
using UnityEngine;

namespace MIIProjekt.Sounds
{
    [RequireComponent(typeof(AudioSource))]
    public class GlobalSoundEagle : MonoBehaviour
    {
        private AudioSource audioSource;

        [SerializeField]
        private OptionsManager optionsManager;

        public void OnVolumeChanged(float newValue)
        {
            audioSource.volume = newValue;
        }
        
        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
            optionsManager.effectsVolumeUpdate += OnVolumeChanged;
        }

        private void Start()
        {
            audioSource.volume = optionsManager.EffectsVolume;
        }

        public void OnEagleStopChase()
        {
            audioSource.Stop();
        }

        public void OnEagleStartChase()
        {
            audioSource.Play();
        }
    }

}
