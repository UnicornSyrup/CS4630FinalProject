using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndMenu : MonoBehaviour
{
    public static void ExitGame()
    {
        GameManager.S.ExitGame();
    }

    public static void RestartGame()
    {
        GameManager.S.RestartGame();
    }
}
