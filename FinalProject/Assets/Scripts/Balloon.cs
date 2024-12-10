using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon : MonoBehaviour
{
    public ParticleSystem ps;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Bee>() != null)
        {
            GameManager.S.BallonDestroyed();
            var go = GameObject.Instantiate(ps.gameObject);
            go.transform.position = transform.position;
            Destroy(go, 1f);
            Destroy(gameObject);
        }
    }
}
