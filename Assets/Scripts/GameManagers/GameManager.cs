using UnityEngine;

namespace MIIProjekt.GameManagers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;
        private GameState currentGameState;

        void Awake()
        {
            instance = this;
            PauseMenu();
        }

        void Update()
        {
            CheckPauseActivation();
        }

        public bool IsGameCurrentlyPlayed()
        {
            return currentGameState == GameState.GS_GAME;
        }

        public bool IsGameCurrentlyPaused()
        {
            return currentGameState == GameState.GS_PAUSEMENU;
        }

        public void PauseMenu()
        {
            SetGameState(GameState.GS_PAUSEMENU);
        }

        public void LevelComleted()
        {
            SetGameState(GameState.GS_LEVELCOMPLETED);
        }

        public void GameOver()
        {
            SetGameState(GameState.GS_GAME_OVER);
        }

        public void InGame()
        {
            SetGameState(GameState.GS_GAME);
        }

        private void CheckPauseActivation()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (IsGameCurrentlyPaused())
                {
                    InGame();
                }
                else
                {
                    PauseMenu();
                }
            }
        }

        private void SetGameState(GameState newGameState)
        {
            currentGameState = newGameState;
            DisplayManager.instance.SendMessage("UpdateDisplay");
            SendMessage("UpdateTime");
        }
    }
}
