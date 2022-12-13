using System;
using TMPro;
using UnityEngine;

namespace MIIProjekt.GameManagers
{
    [Obsolete("Temporary class created to showcase some functionality")]
    public class DeadEnemyCounterDisplayer : MonoBehaviour
    {
        public static DeadEnemyCounterDisplayer Instance { get; private set; }

        [SerializeField]
        private TMP_Text counterText;

        private int counter;

        public void IncrementCounter()
        {
            counter++;
            UpdateCounter();
        }

        private void UpdateCounter()
        {
            counterText.SetText(string.Format("{0:0}", counter));
        }

        private void Awake()
        {
            Instance = this;
        }
    }
}
