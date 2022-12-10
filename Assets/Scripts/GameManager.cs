using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    GS_PAUSEMENU,
    GS_GAME,
    GS_LEVELCOMPLETED,
    GS_GAME_OVER
}

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Canvas inGameCanvas;

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

    private bool IsGameCurrentlyPaused()
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

    private void SetGameState(GameState newGameState)
    {
        if (newGameState == GameState.GS_GAME)
        {
            inGameCanvas.enabled = true;
            Time.timeScale = 1f;
        }
        else
        {
            inGameCanvas.enabled = false;
            Time.timeScale = 0f;
        }
        currentGameState = newGameState;
    }
}
