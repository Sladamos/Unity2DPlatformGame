using System;
using System.Collections.Generic;
using UnityEngine;

namespace MIIProjekt
{
    public class KeyCollectorComponent : MonoBehaviour, KeyCollector
    {
        public event Action KeyCollected;
        [SerializeField]
        private List<string> collectedKeys = new List<string>();

        [SerializeField]
        private bool isActive;


        public bool AcceptedKey(string keyIdentifier)
        {
            if (isActive && !ContainsKey(keyIdentifier))
            {
                Debug.Log($"Collected key: {keyIdentifier}");
                collectedKeys.Add(keyIdentifier);
                KeyCollected?.Invoke();

                return true;
            }

            return false;
        }

        public bool ContainsKey(string keyIdentifier)
        {
            return collectedKeys.Contains(keyIdentifier);
        }
    }
}
