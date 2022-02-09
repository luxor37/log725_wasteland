using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldConfig : MonoBehaviour
{
    public static float YMin;
    public static float YMax;

    public static float XMin;
    public static float XMax;

    public float yMin = -100;
    public float yMax = 100;

    public float xMin = -100;
    public float xMax = 100;

    // Start is called before the first frame update
    void Start()
    {
        YMin = yMin;
        YMax = yMax;
        XMin = xMin;
        XMax = xMax;
    }

    // Update is called once per frame
    void Update()
    {
        YMin = yMin;
        YMax = yMax;
        XMin = xMin;
        XMax = xMax;
    }
}
