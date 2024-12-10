using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public void Music()
    {
        AudioManager.instance.Play("theme");
    }
}
