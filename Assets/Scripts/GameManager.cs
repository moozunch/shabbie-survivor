using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // Singleton untuk akses global

    [Header("UI References")]
    public GameObject gameOverUI; // UI panel untuk Game Over

    // Enum untuk status permainan
    public enum GameState
    {
        Gameplay, // Mode main normal
        Paused,   // Mode pause (Time.timeScale = 0)
        GameOver  // Saat player mati/game selesai
    }

    public GameState currentState;
    public GameState previousState;

    void Awake()
    {
        // Setup singleton agar tidak duplikat saat reload/pindah scene
        if (instance == null)
        {
            instance = this;
            
        }
        else
        {
            Destroy(gameObject); // Hapus instance yang duplikat
        }

        // Pastikan waktu berjalan normal saat game mulai/restart
        Time.timeScale = 1f;

        // Sembunyikan UI Game Over saat mulai
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(false);
        }

        // Set state awal
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
                // Jangan izinkan pause/resume saat sudah Game Over
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
            previousState = currentState;        // Simpan state sebelumnya
            ChangeState(GameState.Paused);       // Masuk state Paused
            Time.timeScale = 0f;                 // Hentikan waktu
            Debug.Log("Game Paused");
        }
    }

    public void ResumeGame()
    {
        if (currentState == GameState.Paused)
        {
            ChangeState(previousState); // Kembali ke state sebelumnya
            Time.timeScale = 1f;        // Lanjutkan waktu
            Debug.Log("Game Resumed");
        }
    }

    // --- FUNGSI BARU UNTUK GAME OVER ---
    public void TriggerGameOver()
    {
        // Cegah pemanggilan berulang
        if (currentState != GameState.GameOver)
        {
            ChangeState(GameState.GameOver);
            Time.timeScale = 0f; // Hentikan permainan
            Debug.Log("GAME OVER!");

            // Tampilkan panel Game Over jika referensi ada
            if (gameOverUI != null)
            {
                gameOverUI.SetActive(true);
            }
        }
    }

    void CheckForPauseAndResume()
    {
        // Toggle pause/resume dengan tombol Escape
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