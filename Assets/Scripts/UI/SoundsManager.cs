using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MIIProjekt.UI
{
    public class SoundsManager : MonoBehaviour
    {
        [SerializeField]
        private AudioSource levelMusic;

        [SerializeField]
        private AudioClip backgroundMusic;

        [SerializeField]
        private AudioClip levelCompletedMusic;

        private float musicChangeTime = 2.0f;

        public void playLevelCompletedSound()
        {
            playSound(levelCompletedMusic);
        }

        public void playGameSound()
        {
            playSound(backgroundMusic);
        }

        private void playSound(AudioClip clip)
        {
            if(levelMusic.clip != clip)
            {
                levelMusic.clip = clip;
                levelMusic.Play();
            }
        }
    }
}
