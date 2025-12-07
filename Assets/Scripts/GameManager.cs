using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    // Enum untuk status permainan
    public enum GameState
    {
        Gameplay,
        Paused,
        GameOver
    }

    // Menyimpan status permainan saat ini
    public GameState currentState;
    // Menyimpan status permainan sebelumnya
    public GameState previousState;

    void Awake()
    {
        // Singleton pattern
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        // Set initial state
        currentState = GameState.Gameplay;
    }

    void Update()
    {
        // Mendefinisikan logika berdasarkan status permainan saat ini
        switch (currentState)
        {
            case GameState.Gameplay:
                // Logika untuk mode gameplay
                CheckForPauseAndResume();
                break;
            case GameState.Paused:
                // Logika untuk mode pause
                CheckForPauseAndResume();
                break;
            case GameState.GameOver:
                // Logika untuk mode game over
                break;
            default:
                Debug.LogError("Unknown game state: " + currentState);
                break;
        }
    }

    public void ChangeState(GameState newState)
    {
        currentState = newState;
    }

    public void PauseGame()
    {
        if (currentState != GameState.Paused)
        {
            previousState = currentState;
            ChangeState(GameState.Paused);
            Time.timeScale = 0f; // Menghentikan waktu dalam permainan
            Debug.Log("Game Paused");
        }
    }

    public void ResumeGame()
    {
        if (currentState == GameState.Paused)
        {
            ChangeState(previousState);
            Time.timeScale = 1f; // Melanjutkan waktu dalam permainan
            Debug.Log("Game Resumed");
        }
    }

    // Memeriksa input untuk pause dan resume
    void CheckForPauseAndResume()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (currentState == GameState.Paused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }
}
