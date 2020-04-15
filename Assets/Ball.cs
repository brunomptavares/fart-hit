using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : InteractableSceneObject
{
    //Components and GameObjects
    private ViewportUtils vu;


    // Start is called before the first frame update
    new void Awake()
    {
        base.Awake();
    }

    void Start()
    {
        vu = GameObject.FindGameObjectWithTag("Utils").GetComponent<ViewportUtils>();
    }

    // Update is called once per frame
    void Update()
    {
        if (vu.isOutOfBoundsBottom(transform.position)) {
            rb.isKinematic = true;
            rb.position = new Vector3(0, Camera.main.transform.position.y + vu.cameraHeight / 2, 0);
            rb.isKinematic = false;
        }
    }

    void FixedUpdate()
    {
        //
        rb.AddForce(new Vector3(0,-1.25f,0));
    }

    public override void activate()
    {
        Debug.Log("BALL HIT");
    }

    public float getRadius() {
       return GetComponent<SphereCollider>().radius;
    }

}
