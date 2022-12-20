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
    }
}
