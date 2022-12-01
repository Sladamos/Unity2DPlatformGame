using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MIIProjekt
{
    public class Door : MonoBehaviour
    {
        [SerializeField]
        private List<string> requiredKeys;

        [SerializeField]
        private KeyCollectorComponent keyCollector;

        private void Awake()
        {
            if (keyCollector == null)
            {
                Debug.Log($"Key collector for instance {name} is not set.");
            }
            else
            {
                keyCollector.KeyCollected += OnKeyCollected;
            }
        }

        private void OnKeyCollected()
        {
            foreach (string requiredKey in requiredKeys) {
                if (!keyCollector.ContainsKey(requiredKey)) {
                    return;
                } 
            }

            GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
