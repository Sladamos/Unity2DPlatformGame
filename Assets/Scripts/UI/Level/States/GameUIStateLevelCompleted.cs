using MIIProjekt.Logging;
using NLog;
using UnityEngine;

namespace MIIProjekt.UI.Level.States
{
    public class GameUIStateLevelCompleted : StateMachineBehaviour
    {
        private static readonly NLog.Logger Logger = LogManager.GetCurrentClassLogger();

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            Logger.Trace("UI state LevelCompleted entered");
            var gameObject = animator.GetComponent<UIManager>()?.UIObjectLevelCompleted;
            SoundsManager soundsManager = animator.GetComponent<SoundsManager>();
            if (gameObject != null)
            {
                gameObject.SetActive(true);
            }
            else
            {
                Logger.Warn("Cannot set GameOver UI GameObject as active");
            }
            soundsManager.playLevelCompletedSound();
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            Logger.Trace("UI state LevelCompleted exited");
            var gameObject = animator.GetComponent<UIManager>()?.UIObjectLevelCompleted;
            if (gameObject != null)
            {
                gameObject.SetActive(false);
            }
            else
            {
                Logger.Warn("Cannot set GameOver UI GameObject as inactive");
            }
        }

        private void Awake()
        {
            LoggingManager.InitializeLogging();
        }
    }
}
