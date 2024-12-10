using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    private GameObject pauseMenu;

    private void Start()
    {
        pauseMenu = gameObject;
    }

    public void Unpause()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }

    public void Restart()
    {
        Time.timeScale = 1;
        GameManager.S.RestartGame();
    }

    public void Exit()
    {
        GameManager.S.ExitGame();
    }
}
