using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bee : MonoBehaviour
{
    const float MOVE_SMOOTH_FACTOR = 3f;

    public bool followMode = false;
    private Rigidbody2D rb;

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
        if (followMode && (mousePos - rb.position).magnitude > 0.1)
        {
            // Look at mouse
            transform.up = mousePos - rb.position;
            rb.velocity = (mousePos - rb.position) / Time.fixedDeltaTime / MOVE_SMOOTH_FACTOR;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }
}
