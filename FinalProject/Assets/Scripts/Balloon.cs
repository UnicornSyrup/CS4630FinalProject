using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Bee>() != null)
        {
            GameManager.S.BallonDestroyed();
            Destroy(gameObject);
        }
    }
}
