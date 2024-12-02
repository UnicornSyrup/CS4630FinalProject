using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager S;
    private int currentScene = 0;

    private float _balloonsRemaining;
    private float balloonsRemaining
    {
        get { return _balloonsRemaining; }
        set {
            _balloonsRemaining = value; 
            if(balloonsRemaining <= 0)
            {
                NextLevel();
            }
        }
    }
    private void Awake()
    {

        if (GameManager.S != null)
        {
            Destroy(this);
        }
        else
        {
            DontDestroyOnLoad(this);
        }
    }

    void Start()
    {
        S = this;
        SceneManager.activeSceneChanged += OnActiveSceneChange;
    }

    public void NextLevel()
    {
        currentScene++;
        SceneManager.LoadScene(currentScene);

    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene(currentScene);
    }

    private void OnActiveSceneChange(Scene current, Scene next)
    {
        var balloonContatiner = GameObject.Find("Balloons");
        var balloons = balloonContatiner.GetComponentsInChildren<Balloon>();
        if(balloons != null)
        {
            balloonsRemaining = balloons.Length;
        }
    }

    public void BallonDestroyed()
    {
        balloonsRemaining--;
    }

}
