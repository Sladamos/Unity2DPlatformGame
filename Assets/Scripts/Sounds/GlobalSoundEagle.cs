using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MIIProjekt.Sounds
{
    [RequireComponent(typeof(AudioSource))]
    public class GlobalSoundEagle : MonoBehaviour
    {
        private AudioSource audioSource;

        public void OnVolumeChanged(float newValue)
        {
            audioSource.volume = newValue;
        }
        
        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
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
