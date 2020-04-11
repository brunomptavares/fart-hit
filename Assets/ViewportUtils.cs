using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewportUtils : MonoBehaviour
{
    //Camera dimensions in world coordinates
    public float cameraHeight;
    public float cameraWidth;

    // Start is called before the first frame update
    void Awake()
    {
        //Calculate camera dimensions in world coordinates
        cameraHeight = Camera.main.orthographicSize * 2.0f;
        cameraWidth = cameraHeight * Camera.main.aspect;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
