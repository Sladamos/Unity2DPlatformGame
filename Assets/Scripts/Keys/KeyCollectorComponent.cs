using System;
using System.Collections.Generic;
using UnityEngine;

namespace MIIProjekt.Keys
{
    public class KeyCollectorComponent : MonoBehaviour
    {
        public event Action<KeyAttributes> KeyCollected;

        [SerializeField]
        private List<string> collectedKeys = new List<string>();

        [SerializeField]
        private bool isActive;

        public bool AcceptedKey(KeyAttributes keyAttributes)
        {
            if (isActive && !ContainsKey(keyAttributes.Identifier))
            {
                collectedKeys.Add(keyAttributes.Identifier);

                KeyCollected?.Invoke(keyAttributes);

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
