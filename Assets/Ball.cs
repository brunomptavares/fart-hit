using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : InteractableSceneObject
{
    private Vector3 initialPos;
    //Components and GameObjects
    private ViewportUtils vu;

    // Start is called before the first frame update
    new void Awake()
    {
        base.Awake();
        initialPos = transform.position;
        vu = GameObject.FindGameObjectWithTag("Utils").GetComponent<ViewportUtils>();
        Debug.Log(vu);
    }

    void Start()
    {
        vu = GameObject.FindGameObjectWithTag("Utils").GetComponent<ViewportUtils>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -vu.cameraHeight / 2)
        {
            rb.isKinematic = true;
            rb.position = initialPos;
            rb.isKinematic = false;
        }
    }

    public override void activate()
    {
        Debug.Log("BALL HIT");
    }

}
