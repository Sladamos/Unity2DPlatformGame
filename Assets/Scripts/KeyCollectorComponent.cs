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


        public bool AcceptKey(string keyIdentifier)
        {
            if (!isActive)
            {
                return false;
            }

            if (collectedKeys.Contains(keyIdentifier))
            {
                return false;
            }

            Debug.Log($"Collected key: {keyIdentifier}");
            collectedKeys.Add(keyIdentifier);

            KeyCollected?.Invoke();

            return true;
        }

        public bool ContainsKey(string keyIdentifier)
        {
            return collectedKeys.Contains(keyIdentifier);
        }
    }
}