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
        Time.timeScale = 0f;
        SetGameState(GameState.GS_PAUSEMENU);
    }

    public void LevelComleted()
    {
        Time.timeScale = 0f;
        SetGameState(GameState.GS_LEVELCOMPLETED);
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        SetGameState(GameState.GS_GAME_OVER);
    }

    public void InGame()
    {
        Time.timeScale = 1f;
        SetGameState(GameState.GS_GAME);
    }

    private void SetGameState(GameState newGameState)
    {
        currentGameState = newGameState;
    }
}
