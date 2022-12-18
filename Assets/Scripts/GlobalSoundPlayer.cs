using MIIProjekt.Player;
using UnityEngine;

namespace MIIProjekt
{
    [RequireComponent(typeof(AudioSource))]
    public class GlobalSoundPlayer : MonoBehaviour
    {
        private AudioSource audioSource;

        [SerializeField]
        private PlayerLife playerLife;

        [SerializeField]
        private AudioClip playerLifeDecreasedAudio;

        [SerializeField]
        private PlayerScore playerScore;

        [SerializeField]
        private AudioClip playerScoreIncreasedAudio;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();

            if (playerLife != null)
            {
                playerLife.PlayerLifeChanged += OnPlayerLifeChanged;
            }

            if (playerScore != null)
            {
                playerScore.PlayerScoreChanged += OnPlayerScoreChanged;
            }
        }

        private void OnPlayerLifeChanged(int oldValue, int newValue)
        {
            if (newValue < oldValue)
            {
                TryPlaySoundLifeDecreased();
            }
        }

        private void OnPlayerScoreChanged(int oldValue, int newValue)
        {
            if (newValue > oldValue)
            {
                TryPlaySoundScoreIncreased();
            }
        }

        private void TryPlaySoundLifeDecreased()
        {
            if (playerLifeDecreasedAudio != null)
            {
                audioSource?.PlayOneShot(playerLifeDecreasedAudio);
            }
        }

        private void TryPlaySoundScoreIncreased()
        {
            if (playerScoreIncreasedAudio)
            {
                audioSource?.PlayOneShot(playerScoreIncreasedAudio);
            }
        }
    }
}
