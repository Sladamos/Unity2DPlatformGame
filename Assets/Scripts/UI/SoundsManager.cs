using System;
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

        [SerializeField]
        private AudioClip highTemperatureMusic;

        private const float FADE_TIME_SECONDS = 2.0f;
        private AudioClip currentGameMusic;

        private void Awake()
        {
            currentGameMusic = backgroundMusic;
        }

        public void PlayLevelCompletedSound()
        {
            PlaySound(levelCompletedMusic);
        }

        public void PlayGameSound()
        {
            PlaySound(currentGameMusic);
        }

        public void PlayGameOverSound()
        {
            PlaySound(gameOverMusic);
        }

        public void PlayHighTemperatureSong()
        {
            currentGameMusic = highTemperatureMusic;
            PlaySoundWithFade(highTemperatureMusic);
        }

        private void PlaySound(AudioClip clip)
        {
            if(clip != null && levelMusic.clip != clip)
            {
                levelMusic.clip = clip;
                levelMusic.PlayOneShot(clip);
            }
        }

        private void PlaySoundWithFade(AudioClip clip, float duration = FADE_TIME_SECONDS)
        {
            if (clip != null && levelMusic.clip != clip)
            {
                float volume = levelMusic.volume;
                StartCoroutine(FadeOut(volume, duration));
                StartCoroutine(FadeIn(volume, duration, clip));
            }
        }

        IEnumerator FadeOut(float volume, float duration)
        {
            float timeElapsed = 0;

            while (levelMusic.volume > 0)
            {
                levelMusic.volume = Mathf.Lerp(volume, 0, timeElapsed / duration);
                timeElapsed += Time.deltaTime;
                yield return null;
            }

            yield break;
        }

        IEnumerator FadeIn(float volume, float delay, AudioClip clip)
        {
            yield return new WaitForSeconds(delay);
            float timeElapsed = 0;
            levelMusic.clip = clip;
            levelMusic.PlayOneShot(clip);

            while (levelMusic.volume < volume)
            {
                levelMusic.volume = Mathf.Lerp(0, volume, timeElapsed / FADE_TIME_SECONDS);
                timeElapsed += Time.deltaTime;
                yield return null;
            }

            levelMusic.volume = volume;
            yield break;
        }

    }
}
