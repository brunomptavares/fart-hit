using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScript : MonoBehaviour
{

    //Camera dimensions in world coordinates
    private float cameraHeight;
    private float cameraWidth;
    private Renderer renderer;
    // Start is called before the first frame update
    void Start()
    {
        //Initialize component references
        renderer = GetComponent<Renderer>();
        //Get camera dimensions
        cameraHeight = Camera.main.orthographicSize * 2.0f;
        cameraWidth = cameraHeight * Camera.main.aspect;
        //Set transform scale
        transform.localScale = new Vector3(cameraWidth, cameraHeight, 1);
        //Set tiling
        renderer.material.SetTextureScale("_MainTex", new Vector2(cameraWidth, cameraHeight));
    }

    // Update is called once per frame
    void Update()
    {
        //Get current texture offset
        float currentOffsetY = renderer.material.mainTextureOffset.y;
        //Object to hold the new texture offset
        Vector2 offset;
        //Scroll down when user presses up
        if (Input.GetKey(KeyCode.W)) {
            offset = new Vector2(0, currentOffsetY + 1.5f * Time.deltaTime);
            renderer.material.SetTextureOffset("_MainTex", offset);
        }
        //Scroll down when user presses down
        else if (Input.GetKey(KeyCode.S)) {
            offset = new Vector2(0, currentOffsetY - 1.5f * Time.deltaTime);
            renderer.material.SetTextureOffset("_MainTex", offset);
        }
    }
}
