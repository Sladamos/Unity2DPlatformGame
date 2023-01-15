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
            Debug.Log(audioSource == null);
            audioSource.volume = newValue;
        }
        
        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }

        public void OnEagleStopChase()
        {
            audioSource.loop = false;
        }

        public void OnEagleStartChase()
        {
            audioSource.loop = true;
            audioSource.PlayOneShot(audioSource.clip);
        }
    }

}
