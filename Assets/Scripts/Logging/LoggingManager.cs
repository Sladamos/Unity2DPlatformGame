using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using NLog;
using NLog.Config;
using NLog.Targets;

namespace MIIProjekt.Logging
{
    public static class LoggingManager
    {
        private const string ConfigurationPath = "Assets/Libraries/NLog.config";

        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        static LoggingManager()
        {
            try
            {
                var readConfiguration = LoadConfigurationFromAssets(ConfigurationPath);

                var fileLogTarget = readConfiguration.FindTargetByName<FileTarget>("logFile");
                UpdateFileLogDirectory(fileLogTarget);

                // Create a Unity target to log to
                var unityDebugLogTarget = new UnityDebugLogTarget() { Name = "LogUnity" };

                // Update all rules to contain file and unity target
                UpdateRulesToContainWriteToTarget(readConfiguration, new List<Target>() { fileLogTarget, unityDebugLogTarget });

                LogManager.Configuration = readConfiguration;

                Logger.Info("Initialized logger config");

                UnityEngine.Application.logMessageReceived += HandleLog;
            }
            catch (Exception ex)
            {
                // Catch exceptions in cases where the 
                UnityEngine.Debug.Log($"An exception has occurred while trying to read NLog configuration.\n{ex}");
            }
        }

        /// <summary>
        /// Initializes loggers.
        /// </summary>
        public static void InitializeLogging()
        {
            // This method does not have any body. 
            // The main purpose of this method is to force the application to call the static constructor.
        }

        public static string GetLogDirectory()
        {
            return UnityEngine.Application.persistentDataPath + "/";
        }

        private static LoggingConfiguration LoadConfigurationFromAssets(string filePath)
        {
            // Load the configuration from a file in the unity project directory
            using var streamReader = new StreamReader(filePath);
            using var xmlReader = XmlReader.Create(streamReader);

            return new XmlLoggingConfiguration(xmlReader, null);
        }

        private static void UpdateFileLogDirectory(FileTarget fileTarget)
        {
            string logDirectory = GetLogDirectory();
            fileTarget.FileName = logDirectory + fileTarget.FileName;
            fileTarget.ArchiveFileName = logDirectory + fileTarget.ArchiveFileName;
            UnityEngine.Debug.Log($"Updating file log directory to: {logDirectory}");
        }

        private static void UpdateRulesToContainWriteToTarget(LoggingConfiguration configuration, List<Target> targets)
        {
            foreach (var rule in configuration.LoggingRules)
            {
                rule.Targets.Clear();

                foreach (var target in targets)
                {
                    rule.Targets.Add(target);
                }
            }
        }

        private static void HandleLog(string logString, string stackTrace, UnityEngine.LogType type)
        {
            switch (type)
            {
                case UnityEngine.LogType.Exception:
                    Logger.Fatal("Unhandled exception: {} \nStack trace: {}\n", logString, ('\n' + stackTrace));
                    break;
            }
        }
    }
}
