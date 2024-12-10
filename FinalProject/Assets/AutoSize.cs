using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoSize : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        const float PADDING_FACTOR = 1.1f;
        const float UI_TOP_PADDING_FACTOR = 1.1f;
        const float UI_TOP_OFFSET = 0.1f;

        var bounds = GameObject.Find("StageSize").GetComponent<BoxCollider2D>().bounds;
        var objectSize = bounds.max - bounds.min;
        var fitWidth = (objectSize.x * .5f) / Camera.main.aspect * PADDING_FACTOR;
        var fitHeight = objectSize.y * .5f * PADDING_FACTOR * UI_TOP_PADDING_FACTOR;

        Camera.main.orthographicSize = Mathf.Max(fitWidth, fitHeight);

        Camera.main.transform.position = bounds.center + Camera.main.transform.forward * -10 
            + Camera.main.transform.up * Camera.main.orthographicSize * UI_TOP_OFFSET;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
