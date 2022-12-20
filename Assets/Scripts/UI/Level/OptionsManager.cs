using System;
using MIIProjekt.Logging;
using NLog;
using UnityEngine;

namespace MIIProjekt.UI.Level
{
    public class OptionsManager : MonoBehaviour
    {
        private static readonly NLog.Logger Logger = LogManager.GetCurrentClassLogger();

        public void OnValueChangedMasterVolume(float value)
        {
            Logger.Debug("Master volume set new value: {}", value);
            throw new NotImplementedException();
        }

        public void OnValueChangedGraphics(int value)
        {
            Logger.Debug("Graphics set new value: {}", value);
            throw new NotImplementedException();
        }

        private void Awake()
        {
            LoggingManager.InitializeLogging();
        }
    }
}
