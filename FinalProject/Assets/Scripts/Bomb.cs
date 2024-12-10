using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        var bee = collision.gameObject.GetComponent<Bee>();
        if(bee != null)
        {
            Destroy(collision.gameObject);//destory bee
            GameManager.S.BeeDies();
            Destroy(gameObject); //destroy bomb
        }
    }
}
