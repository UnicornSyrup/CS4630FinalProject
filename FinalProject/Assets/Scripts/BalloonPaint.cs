using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BalloonPaint : MonoBehaviour
{
    public bool painting = false;
    public List<GameObject> prefabs = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (painting)
        {
            if (Input.GetMouseButtonDown(0))
            {
                GameObject p = PrefabUtility.InstantiatePrefab(prefabs[Mathf.FloorToInt(Random.value * prefabs.Count)]) as GameObject;
                Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                pos.z = 0;

                p.transform.position = pos;
            }
        }
    }
}
