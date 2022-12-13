using System;
using TMPro;
using UnityEngine;

namespace MIIProjekt.GameManagers
{
    [Obsolete("Temporary class created to showcase some functionality")]
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class DeadEnemyCounterDisplayer : MonoBehaviour
    {
        public static DeadEnemyCounterDisplayer Instance { get; private set; }

        private TextMeshProUGUI text;

        private int counter = 0;

        public void IncrementCounter()
        {
            counter++;
            UpdateCounter();
        }

        private void UpdateCounter()
        {
            if (text != null)
            {
                text.SetText(string.Format("{0:0}", counter));
            }
        }

        private void Awake()
        {
            Instance = this;

            text = GetComponent<TextMeshProUGUI>();
        }
    }
}
