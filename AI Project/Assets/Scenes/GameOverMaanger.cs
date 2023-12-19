using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMaanger : MonoBehaviour
{
    public void Start()
    {
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
    public void Retry()
    {
        SceneManager.LoadScene("Gameplay");
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
