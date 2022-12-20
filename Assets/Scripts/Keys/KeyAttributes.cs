using System;
using UnityEngine;

namespace MIIProjekt.Keys
{
    public class KeyAttributes
    {
        public string Identifier { get; }
        public Color Color { get; }

        public KeyAttributes(string identifier, Color color)
        {
            this.Identifier = identifier;
            this.Color = color;
        }
    }
}
