using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
