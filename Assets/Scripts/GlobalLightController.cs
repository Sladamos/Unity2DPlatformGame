﻿using NLog;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace MIIProjekt
{
    [RequireComponent(typeof(Light2D))]
    public class GlobalLightController : MonoBehaviour
    {
        private static readonly NLog.Logger Logger = LogManager.GetCurrentClassLogger();

        private Light2D controlledLight;
        
        private float timeLastLightSet = Mathf.Infinity;
        private float changeDuration = 1.0f;

        private Color colorLast = Color.white;
        private Color colorTarget = Color.white;

        private float TimeSinceLastLightSet => Time.time - timeLastLightSet;

        public void SetLightGradually(Color color, float duration)
        {
            if (controlledLight != null) 
            {
                colorLast = controlledLight.color;
            }

            colorTarget = color;
            changeDuration = duration;
            timeLastLightSet = Time.time;
        }

        private void Awake()
        {
            controlledLight = GetComponent<Light2D>();
        }

        private void Update()
        {
            float percent = Mathf.Clamp01((TimeSinceLastLightSet / changeDuration));
            if (percent <= 0.0f && percent >= 1.0f)
            {
                return;
            }

            Color newColor = Color.Lerp(colorLast, colorTarget, percent);
            controlledLight.color = newColor;

            Logger.Debug("Set color to {}", newColor);
        }
    }
}
