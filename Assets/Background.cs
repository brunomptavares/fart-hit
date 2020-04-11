using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    private new Renderer renderer;
    private ViewportUtils vu;
    //Character
    private Character character;

    void Awake()
    {
        //Initialize component references
        renderer = GetComponent<Renderer>();
    }

    void Start()
    {
        character = GameObject.FindGameObjectWithTag("Character").GetComponent<Character>();
        vu = GameObject.FindGameObjectWithTag("Utils").GetComponent<ViewportUtils>();
        //Set transform scale
        transform.localScale = new Vector3(vu.cameraWidth, vu.cameraHeight, 1);
        //Set tiling
        renderer.material.SetTextureScale("_MainTex", new Vector2(vu.cameraWidth, vu.cameraHeight));
    }

    // Update is called once per frame
    void Update()
    {
       /* //Get current texture offset
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
        }*/

        if (character.rb.velocity.y > 0 && character.rb.position.y > - vu.cameraHeight/4) {
            //Get current texture offset
            float currentOffsetY = renderer.material.mainTextureOffset.y;
            //Object to hold the new texture offset
            Vector2 offset = new Vector2(0, currentOffsetY + character.rb.velocity.y * Time.deltaTime);
            renderer.material.SetTextureOffset("_MainTex", offset);
        }
    }
}
