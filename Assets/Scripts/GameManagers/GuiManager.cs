﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MIIProjekt.GameManagers
{
    public class GuiManager : MonoBehaviour
    {
        [SerializeField]
        private Canvas inGameCanvas;

        private void UpdateDisplay()
        {
            if (GameManager.instance.IsGameCurrentlyPlayed())
            {
                inGameCanvas.enabled = true;
            }
            else
            {
                inGameCanvas.enabled = false;
            }
        }
    }
}
