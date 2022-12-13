using System;
using System.Collections.Generic;
using MIIProjekt.GameManagers;
using UnityEngine;

namespace MIIProjekt.Keys
{
    public class KeyCollectorComponent : MonoBehaviour, KeyCollector
    {
        [SerializeField]
        private List<string> collectedKeys = new List<string>();

        [SerializeField]
        private bool isActive;

        public event Action KeyCollected;

        public bool AcceptedKey(KeyAttributes keyAttributes)
        {
            string keyIdentifier = keyAttributes.GetIdentifier();

            if (isActive && !ContainsKey(keyIdentifier))
            {
                collectedKeys.Add(keyIdentifier);
                DisplayManager.instance.SendMessage("DisplayKey", keyAttributes);
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
