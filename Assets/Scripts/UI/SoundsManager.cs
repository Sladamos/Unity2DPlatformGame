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

        [SerializeField]
        private AudioClip gameOverMusic;


        public void PlayLevelCompletedSound()
        {
            PlaySound(levelCompletedMusic);
        }

        public void PlayGameSound()
        {
            PlaySound(backgroundMusic);
        }

        public void PlayGameOverSound()
        {
            PlaySound(gameOverMusic);
        }

        private void PlaySound(AudioClip clip)
        {
            if(clip != null && levelMusic.clip != clip)
            {
                levelMusic.clip = clip;
                levelMusic.Play();
            }
        }
    }
}
