using System;
using System.Collections.Generic;
using UnityEngine;

namespace MIIProjekt
{
    public class KeyCollectorComponent : MonoBehaviour, KeyCollector
    {
        [SerializeField]
        private List<string> collectedKeys = new List<string>();

        [SerializeField]
        private bool isActive;

        public event Action KeyCollected;

        public bool AcceptedKey(string keyIdentifier)
        {
            if (isActive && !ContainsKey(keyIdentifier))
            {
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