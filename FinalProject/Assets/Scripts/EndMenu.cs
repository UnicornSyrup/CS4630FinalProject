using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndMenu : MonoBehaviour
{
    private void Start()
    {
        GameObject.Find("ScoreText").GetComponent<TMPro.TMP_Text>().text = "Score: " + GameManager.S.totalScore;
    }

    public static void ExitGame()
    {
        GameManager.S.ExitGame();
    }

    public static void RestartGame()
    {
        GameManager.S.RestartGame();
    }
}
