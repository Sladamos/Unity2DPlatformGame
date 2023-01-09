using MIIProjekt.Extensions;
using MIIProjekt.GameManagers;
using MIIProjekt.Logging;
using NLog;
using UnityEngine;

namespace MIIProjekt.UI.Level
{
    [RequireComponent(typeof(Animator))]
    public class UIAnimatorController : MonoBehaviour
    {
        private const string TriggerStringLevelCompleted = "LevelCompleted";
        private const string TriggerStringGameOver = "GameOver";
        private const string TriggerStringGamePaused = "GamePaused";
        private const string TriggerStringOptionsOpened = "OptionsOpened";
        private const string TriggerStringBackKeyClicked = "BackKeyClicked";
        private const string TriggerStringMenuBackClicked = "MenuBackClicked";
        private const string TriggerStringOptionsBackClicked = "OptionsBackClicked";

        private static readonly NLog.Logger Logger = LogManager.GetCurrentClassLogger();

        private Animator animator;

        [SerializeField]
        private LevelManager levelManager;

        [SerializeField]
        private TimeManager timeManager;

        public void InvokeGameResumeTrigger()
        {
            // TODO: Move this method somewhere? Maybe an interface?
            TriggerAnimator(TriggerStringMenuBackClicked);
        }

        public void InvokeOptionsTrigger()
        {
            // TODO: Move this method somewhere? Maybe an interface?
            TriggerAnimator(TriggerStringOptionsOpened);
        }

        public void InvokeOptionsBackClicked()
        {
            // TODO: Move this method somewhere? Maybe an interface?
            TriggerAnimator(TriggerStringOptionsBackClicked);
        }

        private void TriggerAnimator(string triggerName)
        {
            Logger.Debug("Setting trigger {}", triggerName);
            animator.SetTrigger(triggerName);
        }

        private void Awake()
        {
            LoggingManager.InitializeLogging();

            animator = GetComponent<Animator>().VerifyNotNull($"Could not find required Animator instance on GameObject with name {name}.");

            if (levelManager != null)
            {
                levelManager.LevelCompleted += OnLevelCompleted;
                levelManager.GameOver += OnGameOver;
            }
            else
            {
                Logger.Warn("LevelManager is not set on instance {}. The animator will not be notified of some events.", name);
            }

            if (timeManager != null)
            {
                timeManager.GamePaused += OnGamePaused;
                timeManager.GameUnpaused += OnGameUnpaused;
            }
            else
            {
                Logger.Warn("TimeManager is not set on instance {}. The animator will not be notified of some events.", name);
            }
        }

        private void Update()
        {
            // TODO: This is temporary. Move this code somewhere else.
            if (timeManager.IsGamePaused())
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    Logger.Debug("Detected unpause game request");
                    TriggerAnimator(TriggerStringBackKeyClicked);
                }
            }
        }

        private void OnLevelCompleted()
        {
            TriggerAnimator(TriggerStringLevelCompleted);
        }

        private void OnGameOver()
        {
            TriggerAnimator(TriggerStringGameOver);
        }

        private void OnGamePaused()
        {
            TriggerAnimator(TriggerStringGamePaused);
        }

        private void OnGameUnpaused()
        {
            // TODO: Not implemented. Consider removing...
        }
    }
}
