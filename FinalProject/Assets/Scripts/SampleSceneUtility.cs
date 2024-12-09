using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SampleSceneUtility : MonoBehaviour
{
    public static SampleSceneUtility S;
    private string sceneName = "";


    private void Awake()
    {

        if (SampleSceneUtility.S != null)
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
        if (sceneName == "")
        {
            sceneName = SceneManager.GetActiveScene().name;
        }
        if (SceneManager.GetActiveScene().name != sceneName)
        {
            GameManager.S.NextLevel();
        }
    }

}
