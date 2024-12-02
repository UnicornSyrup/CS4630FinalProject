using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bee : MonoBehaviour
{
    // Helps smooth movement 
    const float MOVE_SMOOTH_FACTOR = 3f;

    public bool followMode = false;
    private Rigidbody2D rb;
    public bool inDangerZone = false;
    public bool inDangerMask = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public Vector2 mousePos
    {
        get
        {
            return Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        private set
        {
            return;
        }
    }

    void FixedUpdate()
    {
        if (inDangerMask && inDangerZone)
        {
            // Die
            gameObject.GetComponent<SpriteRenderer>().color = Color.green;
            Destroy(this.gameObject);
            GameManager.S.ReloadLevel();
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        }

        // Stay still until moused over
        if (!followMode)
        {
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.up, 0f, LayerMask.GetMask("Bee"));

            if (hit)
            {
                followMode = true;
            }
        }
        
        // Move to follow mouse
        if (followMode && (mousePos - rb.position).magnitude > 0.1)
        {
            // Look at mouse
            transform.up = mousePos - rb.position;
            // Move toward mouse with collisions and slight smoothing
            rb.velocity = (mousePos - rb.position) / Time.fixedDeltaTime / MOVE_SMOOTH_FACTOR;
        }
        else
        {
            // Stay still if mouse hasn't moved much
            rb.velocity = Vector2.zero;
        }

        

        // OnTriggerStay will call after this
        inDangerZone = false;
        inDangerMask = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<DangerZone>() != null)
        {
            inDangerZone = true;
        }
        if (collision.gameObject.GetComponent<DangerMask>() != null)
        {
            inDangerMask = true;
        }
    }
}
