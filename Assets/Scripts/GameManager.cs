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
    private GameState currentGameState = GameState.GS_PAUSEMENU;

    void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        CheckPauseActivation();
    }

    private void CheckPauseActivation()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Game paused: " + !isGameCurrentlyPaused());
            if (isGameCurrentlyPaused())
            {
                InGame();
            }
            else
            {
                PauseMenu();
            }
        }
    }

    private bool isGameCurrentlyPaused()
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
        currentGameState = newGameState;
    }
}
