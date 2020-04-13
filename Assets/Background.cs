using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    private new Renderer renderer;
    private ViewportUtils vu;

    private Character character;
    private Ball ball;

    private float lerpCameraMovement;
    private Vector3 pos;
    private float highestPosY;
    private float topMargin;

    void Awake()
    {
        //Initialize component references
        renderer = GetComponent<Renderer>();
    }

    void Start()
    {
        character = GameObject.FindGameObjectWithTag("Character").GetComponent<Character>();
        ball = GameObject.FindGameObjectWithTag("Ball").GetComponent<Ball>();
        vu = GameObject.FindGameObjectWithTag("Utils").GetComponent<ViewportUtils>();
        //Ball
        highestPosY = ball.rb.position.y;
        //Set transform scale
        transform.localScale = new Vector3(vu.cameraWidth, vu.cameraHeight, 1);
        //Set tiling
        renderer.material.SetTextureScale("_MainTex", new Vector2(vu.cameraWidth, vu.cameraHeight));
        //
        topMargin = 1.5f + ball.getRadius();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        highestPosY = highestPosY > ball.rb.position.y ? highestPosY : ball.rb.position.y;
        if(!character.rb.useGravity) character.rb.useGravity = true;
        //Character is play area
        if (character.rb.position.y > Camera.main.transform.position.y - vu.cameraHeight / 4) {
            //If ball is out of bounds y top
            if (vu.isOutOfBoundsTop(ball.rb.position + new Vector3(0, topMargin, 0))) {
                //And Ball is moving up
                if (ball.rb.velocity.y > 0) {
                    //And the character is still in the bottom quarter of the camera
                    if (character.rb.position.y > Camera.main.transform.position.y - vu.cameraHeight / 4) {
                        //Scroll background at less ball speed
                        scroll(ball.rb.velocity.y * 0.65f);
                        //Move camera and background
                        float nextPosY = highestPosY = ball.rb.velocity.y * 0.75f * Time.deltaTime;
                        transform.position += new Vector3(0, nextPosY, 0);
                        Camera.main.transform.position += new Vector3(0, nextPosY, -1);
                    }
                }
                //Ball stoped going up
                else {
                    float nextPosY = Mathf.Lerp(transform.position.y, highestPosY - topMargin, Time.deltaTime); ;
                    //Scroll background with units instead of velocity
                    scrollPos(nextPosY - transform.position.y);
                    //Move transform and camera
                    transform.position = new Vector3(0, nextPosY, 0);
                    Camera.main.transform.position = new Vector3(0, nextPosY, -1);
                }
            }
            //If it's moving and ball is in bounds
            else if (character.rb.velocity.y > 0 ) {
                //Scroll background at character speed
                scroll(character.rb.velocity.y);
            }
        //Player outside of playable area
        } else {
            character.rb.useGravity = false;
        }
    }

    private void scroll(float vel)
    {
        //Get current texture offset
        float currentOffsetY = renderer.material.mainTextureOffset.y;
        //Object to hold the new texture offset
        Vector2 offset = new Vector2(0, currentOffsetY + vel * Time.deltaTime);
        //Scroll the texture
        renderer.material.SetTextureOffset("_MainTex", offset);
    }

    private void scrollPos(float deltaPos)
    {
        //Get current texture offset
        float currentOffsetY = renderer.material.mainTextureOffset.y;
        //Object to hold the new texture offset
        Vector2 offset = new Vector2(0, currentOffsetY + deltaPos);
        //Scroll the texture
        renderer.material.SetTextureOffset("_MainTex", offset);
    }
}
