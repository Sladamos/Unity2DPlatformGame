using System;
using UnityEngine;

namespace MIIProjekt
{
    using KeyAttributes = Tuple<string, Color>;

    public interface KeyCollector
    {
        public event Action KeyCollected;
        /// <summary>
        /// Tries to accept a key.
        /// </summary>
        /// <param name="keyAttributes">Key attributes.</param>
        /// <returns>True if an object successfully accepted a key. Otherwise, false.</returns>
        public bool AcceptedKey(KeyAttributes key);
        
        /// <summary>
        /// Checks wheteher the collector contains a key by the given identifier.
        /// </summary>
        /// <param name="keyIdentifier">Key identifier.</param>
        /// <returns>True if the collector contains a key. Otherwise, false.</returns>
        public bool ContainsKey(string keyIdentifier);
    }
}