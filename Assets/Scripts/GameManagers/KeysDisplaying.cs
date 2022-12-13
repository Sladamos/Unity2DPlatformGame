using System;
using System.Collections.Generic;
using MIIProjekt.Keys;
using UnityEngine;
using UnityEngine.UI;

namespace MIIProjekt.GameManagers
{
    public class KeysDisplaying : MonoBehaviour
    {
        [SerializeField]
        private Image[] keysImages;

        [SerializeField]
        private KeyCollectorListener keysSource;

        private Dictionary<string, int> keysIdentifiers;

        private void Awake()
        {
            keysIdentifiers = new Dictionary<string, int>();
        }

        void Start()
        {
            CreateIdentifiersDictionary(keysSource.GetRequiredKeys());
        }

        private void DisplayKey(KeyAttributes keyAttributes)
        {
            String keyIdentifier = keyAttributes.GetIdentifier();
            Color keyColor = keyAttributes.GetColor();
            if (keysIdentifiers.ContainsKey(keyIdentifier))
            {
                ActivateKeyImage(keysIdentifiers[keyIdentifier], keyColor);
            }
        }

        private void ActivateKeyImage(int keyIndex, Color keyColor)
        {
            keysImages[keyIndex].color = keyColor;
        }

        private void CreateIdentifiersDictionary(List<string> requiredKeys)
        {
            int iterator = 0;
            foreach (string identifier in requiredKeys)
            {
                keysIdentifiers.Add(identifier, iterator);
                iterator++;
            }
        }
    }
}
