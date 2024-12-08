using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager S;
    private int currentScene = 0;

    private List<GameObject> lifeIcons;

    private float _balloonsRemaining;
    private float balloonsRemaining
    {
        get { return _balloonsRemaining; }
        set {
            _balloonsRemaining = value;
            if (balloonsRemaining <= 0)
            {
                NextLevel();
            }
        }
    }

    private float _livesRemaining = 3;

    private float livesRemaining
    {
        get { return _livesRemaining; }
        set
        {
            //implement logic for removing life icons
            _livesRemaining = value;
            if(livesRemaining <= 0)
            {
                GameOver();
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

    private void ReloadLevel()
    {
        SceneManager.LoadScene(currentScene);
    }

    private void OnActiveSceneChange(Scene current, Scene next)
    {
        //find out how many ballons there are, in order to know when to end level
        var balloonContatiner = GameObject.Find("Balloons");
        var balloons = balloonContatiner.GetComponentsInChildren<Balloon>();
        if(balloons != null)
        {
            balloonsRemaining = balloons.Length;
        }

        var uiContainer = GameObject.Find("Level UI");
        //find bee lives icon, so you can remove them
        var  beeIcons = uiContainer.GetComponentsInChildren<Bee>();
        foreach(var icon in beeIcons)
        {
            lifeIcons.Add(icon.gameObject);
        }
    }

    public void BallonDestroyed()
    {
        balloonsRemaining--;
    }

    private void GameOver()
    {
        //go to end screen
        print("game over");
    }

    public void BeeDies()
    {
        livesRemaining--;
        if(livesRemaining >0)
        {
            ReloadLevel();
        }
    }
}
