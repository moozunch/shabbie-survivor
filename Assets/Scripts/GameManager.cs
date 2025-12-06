using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // mendefinisikan enum untuk status permainan
    public enum GameState
  {
    Gameplay,
    Paused,
    GameOver
  }
// menyimpan status permainan saat ini
  public GameState currentState;
// menyimpan status permainan sebelumnya
  public GameState previousState;

  void update()
  {
    // mendefinisikan logika berdasarkan status permainan saat ini
    switch (currentState)
    {
      case GameState.Gameplay:
        // logika untuk mode gameplay
        CheckForPauseAndResume();
        break;
      case GameState.Paused:
        // logika untuk mode pause
        CheckForPauseAndResume();
        break;
      case GameState.GameOver:
        // logika untuk mode game over
        break;

        default:
        Debug.LogError("Unknown game state: " + currentState);
        break;
        
    }   
  }

  public void ChangeState(GameState newState)
  {
    currentState = newState;
    Debug.Log("Game State changed to: " + currentState);
  }

  public void PauseGame()
  {
    if (currentState == GameState.Paused)
    {
        previousState = currentState;
        ChangeState(GameState.Paused);
        Time.timeScale = 0f; // menghentikan waktu dalam permainan
        Debug.Log("Game Paused");
    }
  }

    public void ResumeGame()
    {
        if (currentState == GameState.Paused)
        {
            ChangeState(previousState);
            Time.timeScale = 1f; // melanjutkan waktu dalam permainan
            Debug.Log("Game Resumed");
        }
    }

    // memeriksa input untuk pause dan resume
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
