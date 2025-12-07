using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    // Referensi ke UI Game Over (Drag Panel Game Over ke sini di Inspector)
    [Header("UI References")]
    public GameObject gameOverUI;

    // Enum untuk status permainan
    public enum GameState
    {
        Gameplay,
        Paused,
        GameOver
    }

    public GameState currentState;
    public GameState previousState;

    void Awake()
    {
        // Singleton pattern
        if (instance == null)
        {
            instance = this;
            // Hati-hati dengan DontDestroyOnLoad jika UI ada di scene yang berbeda, 
            // referensi UI bisa hilang saat reload scene.
            // Untuk skenario simpel, ini oke.
            // DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);
        }

        // Pastikan waktu berjalan normal saat game mulai/restart
        Time.timeScale = 1f;

        // Sembunyikan UI Game Over saat mulai
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(false);
        }

        // Set initial state
        currentState = GameState.Gameplay;
    }

    void Update()
    {
        switch (currentState)
        {
            case GameState.Gameplay:
                CheckForPauseAndResume();
                break;
            case GameState.Paused:
                CheckForPauseAndResume();
                break;
            case GameState.GameOver:
                // Di sini kita tidak memanggil CheckForPauseAndResume
                // Supaya player tidak bisa mem-pause saat sudah Game Over
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
            Time.timeScale = 0f;
            Debug.Log("Game Paused");
        }
    }

    public void ResumeGame()
    {
        if (currentState == GameState.Paused)
        {
            ChangeState(previousState);
            Time.timeScale = 1f;
            Debug.Log("Game Resumed");
        }
    }

    // --- FUNGSI BARU UNTUK GAME OVER ---
    public void TriggerGameOver()
    {
        // Cek agar fungsi ini tidak dipanggil berkali-kali
        if (currentState != GameState.GameOver)
        {
            ChangeState(GameState.GameOver);
            Time.timeScale = 0f; // Hentikan permainan
            Debug.Log("GAME OVER!");

            // Tampilkan layar Game Over
           
        }
    }

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