using System;
using UnityEngine;

namespace MIIProjekt.Keys
{
    public class KeyAttributes
    {
        private Tuple<string, Color> keyAttributes;

        public KeyAttributes(string keyIdentifier, Color keyColor)
        {
            keyAttributes = new Tuple<string, Color>(keyIdentifier, keyColor);
        }

        public string GetIdentifier()
        {
            return keyAttributes.Item1;
        }

        public Color GetColor()
        {
            return keyAttributes.Item2;
        }
    }
}
