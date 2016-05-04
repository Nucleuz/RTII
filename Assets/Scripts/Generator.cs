using UnityEngine;
using System.Collections;

public class Generator : MonoBehaviour
{
    public GameObject walls;

    // Use this for initialization
    void Start()
    {
        InvokeRepeating("CreateObstacle", 5f, 0.4f);
    }

    void CreateObstacle()
    {
        GameObject g = Instantiate(walls);
        g.transform.parent = transform;
    }
}
