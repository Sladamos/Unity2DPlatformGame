using System;
using UnityEngine;

namespace MIIProjekt.GameManagers
{
    public class TimeManager : MonoBehaviour
    {
        [SerializeField]
        public event Action<float> TimeUpdated;

        public float TimePassed { get; private set; }

        private void Update()
        {
            TimePassed += Time.deltaTime;
            TimeUpdated?.Invoke(TimePassed);
        }

        private void UpdateTime()
        {
            if (GameManager.instance.IsGameCurrentlyPlayed())
            {
                Time.timeScale = 1f;
            }
            else
            {
                Time.timeScale = 0f;
            }
        }
    }
}
