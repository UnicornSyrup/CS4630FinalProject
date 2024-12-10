using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System;
using System.IO;

struct SessionData
{
    public string playerID;
    public DateTime startTime;
    public float durationPlayed;
    public float score;
    public string feedback;

    public override string ToString()
    {
        return "-------------------\n" +
                "Player ID: " + playerID + "\n" +
                "Start Time: " + startTime.ToString("u") + "\n" +
                "Duration: " + durationPlayed.ToString() + "\n" +
                "Score: " + score.ToString() + "\n" +
                "Feedback:\n\n" + feedback + "\n\n" +
                "-------------------\n";
    }
}

public class GameManager : MonoBehaviour
{
    const int MAX_LEVEL = 3;
    const float SCORE_INCREMENT = 10;

    public static GameManager S;
    private int currentScene = 0;

    private List<GameObject> lifeIcons = new List<GameObject>();
    private Slider scoreSlider;
    private TMP_Text scoreSliderText;

    private SessionData session;

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
                scoreSliderText.text = value.ToString() + " pts";
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

    public void StartGame()
    {
        session.durationPlayed = 0;
        session.startTime = System.DateTime.Now;

        TMP_InputField PlayerID = GameObject.Find("PlayerIDField").GetComponent<TMP_InputField>();
        session.playerID = PlayerID.text;

        NextLevel();
    }

    public void NextLevel()
    {
        totalScore += levelScore;
        levelScore = 0;

        currentScene++;
        if (currentScene > MAX_LEVEL)
        {
            GameOver();
        }
        else
        {
            SceneManager.LoadScene(currentScene);
        }
    }

    private void ReloadLevel()
    {
        SceneManager.LoadScene(currentScene);
    }

    public void RestartGame()
    {
        currentScene = 0;
        levelScore = 0;
        totalScore = 0;
        livesRemaining = 3;
        //setting remainging ballons to 0 calls next scene, so don't do that
        SceneManager.LoadScene(0);
    }

    private void SaveSession()
    {
        session.score = totalScore;

        if (SceneManager.GetActiveScene().buildIndex == MAX_LEVEL + 1)
        {
            TMP_InputField feedback = GameObject.Find("FeedbackField").GetComponent<TMP_InputField>();
            session.feedback = feedback.text;
        }
        else
        {
            session.feedback = "- Player ended game early -";
        }

        if (session.durationPlayed == 0)
        {
            session.durationPlayed = (System.DateTime.Now - session.startTime).Seconds;
        }

        string feedbackFolder = Path.Combine(Application.dataPath, "../Feedback");
        using (StreamWriter outputFile = new StreamWriter(Path.Combine(feedbackFolder, "feedback.txt"), true))
        {
            outputFile.Write(session.ToString());
        }
    }

    public void ExitGame()
    {
        SaveSession();
        Application.Quit(); 
    }

    private void OnActiveSceneChange(Scene current, Scene next)
    {
        //find out how many ballons there are, in order to know when to end level
        var balloonContatiner = GameObject.Find("Balloons");
        if(balloonContatiner != null)
        {
            var balloons = balloonContatiner.GetComponentsInChildren<Balloon>();
            if(balloons != null)
            {
                initialBalloons = balloons.Length;
                balloonsRemaining = initialBalloons;
            }
        }

        var levelUi = GameObject.Find("Level UI");

        if(levelUi != null)
        {
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
            scoreSliderText = scoreSlider.GetComponentInChildren<TMP_Text>();
            scoreSliderText.text = "0 pts";

            var levelDisplay = GameObject.Find("Level Display").GetComponentInChildren<TMP_Text>();
            levelDisplay.text = "Level " + currentScene.ToString();
        }
    }

    public void BallonDestroyed()
    {
        balloonsRemaining--;
        levelScore += SCORE_INCREMENT;
    }

    private void GameOver()
    {
        //go to end screen
        session.durationPlayed = (System.DateTime.Now - session.startTime).Seconds;
        SceneManager.LoadScene(MAX_LEVEL + 1);
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