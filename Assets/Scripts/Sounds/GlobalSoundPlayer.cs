using MIIProjekt.Player;
using MIIProjekt.UI.Level;
using UnityEngine;

namespace MIIProjekt.Sounds
{
    [RequireComponent(typeof(AudioSource))]
    public class GlobalSoundPlayer : MonoBehaviour
    {
        private AudioSource audioSource;

        [SerializeField]
        private OptionsManager optionsManager;

        [SerializeField]
        private PlayerLife playerLife;

        [SerializeField]
        private AudioClip playerLifeDecreasedAudio;

        [SerializeField]
        private PlayerScore playerScore;

        [SerializeField]
        private AudioClip playerScoreIncreasedAudio;

        [SerializeField]
        private PlayerController2 playerController;

        [SerializeField]
        private AudioClip playerJumpedAudio;

        [SerializeField]
        private AudioClip playerDashedAudio;

        public void OnVolumeChanged(float newValue)
        {
            audioSource.volume = newValue;
        }

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

            if (playerController != null)
            {
                playerController.PlayerJumped += OnPlayerJumped;
                playerController.PlayerDashed += OnPlayerDashed;
            }

            optionsManager.effectsVolumeUpdate += OnVolumeChanged;
        }

        private void Start()
        {
            audioSource.volume = optionsManager.EffectsVolume;
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

        private void OnPlayerJumped()
        {
            TryPlaySoundPlayerJumped();
        }

        private void OnPlayerDashed()
        {
            TryPlaySoundPlayerDashed();
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
            if (playerScoreIncreasedAudio != null)
            {
                audioSource?.PlayOneShot(playerScoreIncreasedAudio);
            }
        }

        private void TryPlaySoundPlayerJumped()
        {
            if (playerJumpedAudio != null)
            {
                audioSource?.PlayOneShot(playerJumpedAudio);
            }
        }

        private void TryPlaySoundPlayerDashed()
        {
            if (playerDashedAudio != null)
            {
                audioSource?.PlayOneShot(playerDashedAudio);
            }
        }
    }
}
