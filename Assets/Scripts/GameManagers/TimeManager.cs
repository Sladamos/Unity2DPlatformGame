using System;
using UnityEngine;

namespace MIIProjekt.GameManagers
{
    public class TimeManager : MonoBehaviour
    {
        private const float TimeEpsilon = 0.01f;

        public event Action<float> TimeUpdated;
        public event Action GamePaused;
        public event Action GameUnpaused;

        public float TimePassed { get; private set; }

        [SerializeField]
        private bool unpauseGameOnAwake = true;

        public void PauseGame()
        {
            Time.timeScale = 0.0f;
            GamePaused?.Invoke();
        }

        public void UnpauseGame()
        {
            Time.timeScale = 1.0f;
            GameUnpaused?.Invoke();
        }

        public bool IsGamePaused()
        {
            return Time.timeScale < TimeEpsilon;
        }

        private void Awake()
        {
            if (unpauseGameOnAwake)
            {
                UnpauseGame();
            }
        }

        private void Update()
        {
            TimePassed += Time.deltaTime;
            TimeUpdated?.Invoke(TimePassed);
        }
    }
}
