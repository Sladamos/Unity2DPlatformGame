using System;
using System.Collections.Generic;
using MIIProjekt.Collectables;
using UnityEngine;

namespace MIIProjekt.Collectables.Keys
{
    public class KeyCollectorComponent : MonoBehaviour
    {
        public event Action<KeyAttributes> KeyCollected;

        [SerializeField]
        private List<string> collectedKeys = new List<string>();

        [SerializeField]
        private bool isActive;

        public List<ICollectable> Collectables => throw new NotImplementedException();

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

        public void AddCollectable(ICollectable collectable)
        {
            throw new NotImplementedException();
        }

        public void RemoveCollectable(ICollectable collectable)
        {
            throw new NotImplementedException();
        }
    }
}
