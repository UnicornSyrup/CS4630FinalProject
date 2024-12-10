using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TimerDisplay : MonoBehaviour
{
    public TMP_Text field;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        field.text = TimeSpan.FromSeconds(GameManager.S.timer).ToString(@"mm\:ss");
    }
}
