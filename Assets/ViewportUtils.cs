using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewportUtils : MonoBehaviour
{
    //Camera dimensions in world coordinates
    public float cameraHeight;
    public float cameraWidth;
    //Components
    public Ball ball;

    // Start is called before the first frame update
    void Awake()
    {
        //Calculate camera dimensions in world coordinates
        cameraHeight = Camera.main.orthographicSize * 2.0f;
        cameraWidth = cameraHeight * Camera.main.aspect;
    }

    private void Start()
    {
        ball = GameObject.FindGameObjectWithTag("Ball").GetComponent<Ball>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (ball.position.y > Camera.main.transform.position.y)
    }

    public bool isOutOfBoundsTop(Vector2 pos)
    {
        return pos.y > Camera.main.transform.position.y + cameraHeight / 2;
    }

    public bool isOutOfBoundsBottom(Vector2 pos)
    {
        return pos.y < Camera.main.transform.position.y - cameraHeight / 2;
    }
}
