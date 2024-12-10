using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButton : MonoBehaviour
{
    public GameObject pauseMenu;

    public void PauseToggle()
    {
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
    }
}
