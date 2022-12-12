using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MIIProjekt
{
    public class KeyCollectorListener : MonoBehaviour
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
            if (AreAllKeysCollected())
            {
                keyCollector.KeyCollected -= OnKeyCollected;
                gameObject.SendMessage("OnAllKeysCollected");
            }
        }

        private bool AreAllKeysCollected()
        {
            foreach (string requiredKey in requiredKeys)
            {
                if (!keyCollector.ContainsKey(requiredKey))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
