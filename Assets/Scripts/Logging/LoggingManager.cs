using System;
using System.IO;
using System.Xml;
using NLog;
using NLog.Config;
using NLog.Targets;

namespace MIIProjekt.Logging
{
    public static class LoggingManager
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        static LoggingManager()
        {
            string logDirectory = GetLogDirectory();

            try
            {
                // Load the configuration from a file in the unity project directory
                using var streamReader = new StreamReader("Assets/Libraries/NLog.config");
                using var xmlReader = XmlReader.Create(streamReader);

                var readConfiguration = new XmlLoggingConfiguration(xmlReader, null);

                // Update file log directory
                var fileLogTarget = readConfiguration.FindTargetByName<FileTarget>("logFile");
                fileLogTarget.FileName = logDirectory + fileLogTarget.FileName;
                fileLogTarget.ArchiveFileName = logDirectory + fileLogTarget.ArchiveFileName;

                // Create a Unity target to log to
                var unityDebugLogTarget = new UnityDebugLogTarget()
                {
                    Name = "LogUnity"
                };

                // Update all rules to contain file and unity target
                foreach (var rule in readConfiguration.LoggingRules)
                {
                    rule.Targets.Clear();
                    rule.Targets.Add(fileLogTarget);
                    rule.Targets.Add(unityDebugLogTarget);
                }

                LogManager.Configuration = readConfiguration;

                Logger.Info("Initialized logger config");
                UnityEngine.Debug.Log($"Log files location: {logDirectory}");
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
    }
}
