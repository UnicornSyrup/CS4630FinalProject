using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;


public class GameManager : MonoBehaviour
{
    const float SCORE_INCREMENT = 10;

    public static GameManager S;
    private int currentScene = 0;

    private List<GameObject> lifeIcons = new List<GameObject>();
    private Slider scoreSlider;

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

    private float initialBalloons;

    private float _livesRemaining = 3;

    int i = 0;

    private float livesRemaining
    {
        get { return _livesRemaining; }
        set
        {
            // Life icon will disappear when scene reloads

            _livesRemaining = value;
            if(livesRemaining <= 0)
            {
                GameOver();
            }
        }
    }

    private float _levelScore = 0;

    private float levelScore
    {
        get { return _levelScore; }
        set
        {
            if(scoreSlider != null)
            {
                scoreSlider.value = value / SCORE_INCREMENT / initialBalloons;
            }
            _levelScore = value;
        }
    }

    private float totalScore=0;

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
        totalScore += levelScore;
        levelScore = 0;

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
            initialBalloons = balloons.Length;
            balloonsRemaining = initialBalloons;
        }

        var livesContainer = GameObject.Find("Lives Panel");
        lifeIcons.Clear();
        for (int i = 0; i < livesContainer.transform.childCount; i++)
        {
            Image icon = livesContainer.transform.GetChild(i).GetComponent<Image>();
            lifeIcons.Add(icon.gameObject);
            int lifeIndex = (int)Char.GetNumericValue(icon.gameObject.name[4]);
            if (lifeIndex > livesRemaining - 1)
            {
                icon.color = new Color(0, 0, 0, 0);
            }
        }

        scoreSlider = GameObject.Find("Score Slider").GetComponent<Slider>();
        

    }

    public void BallonDestroyed()
    {
        balloonsRemaining--;
        levelScore += SCORE_INCREMENT;
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