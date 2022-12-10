using MIIProjekt;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    private void DisplayKey(Tuple<string, Color> key)
    {
        String keyIdentifier = key.Item1;
        Color keyColor = key.Item2;
        if(keysIdentifiers.ContainsKey(keyIdentifier))
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
        foreach(string identifier in requiredKeys)
        {
            keysIdentifiers.Add(identifier, iterator);
            iterator++;
        }
    }
}
