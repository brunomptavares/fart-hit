using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableSceneObject : SceneObject
{
    // Start is called before the first frame update
    new void Awake()
    {
        base.Awake();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag.Equals("Character")) activate();
    }

    public abstract void activate();
}
  