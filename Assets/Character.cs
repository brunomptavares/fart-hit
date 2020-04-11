using UnityEngine;

public class Character : SceneObject
{

    //State stuff
    private enum CharacterState { INITIAL, IDLE, FALLING, FARTING, BEFORE_FARTING, COLLISION, EAT }
    private CharacterState state = CharacterState.INITIAL;

    //Target
    private Vector2 target;
    private Vector2 targetDir;

    //Status
    private float speed = 15f;
    private float acceleration = 5f;

    //Timers
    private float timerAdjustAngle;

    // Start is called before the first frame update
    new void Awake() {
        base.Awake();
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case CharacterState.INITIAL:
                //initialization
                //Debug.Log("CharacterState:" + state);
                break;
            case CharacterState.IDLE:
                adjustAngle();
                //Fall if vertical velocity is negative
                if (state == CharacterState.IDLE & rb.velocity.y < 0) {
                    state = CharacterState.FALLING;
                    Debug.Log("CharacterState:" + state);
                }
                break;
            case CharacterState.FALLING:
                adjustAngle();
                break;
            case CharacterState.BEFORE_FARTING:
                if (state == CharacterState.BEFORE_FARTING) {
                    state = CharacterState.FARTING;
                    Debug.Log("CharacterState:" + state);
                }
                break;
            case CharacterState.FARTING:
                fart();
                break;
            case CharacterState.COLLISION:
                break;
            case CharacterState.EAT:
                break;
        }
    }

    private void FixedUpdate()
    {
        switch (state)
        {
            case CharacterState.FARTING:
                //Set velocity
                Vector3 vel = targetDir.normalized * (speed * Time.fixedDeltaTime * 1f);
                //Show direction
                Debug.DrawRay(rb.position, targetDir, Color.red);
                rb.MovePosition(rb.position + vel);
            break;
        }
    }

    public void setTarget(Vector2 pos)
    {
        target = pos;
        //Calculate direction
        targetDir = new Vector3(target.x, target.y, 0) - rb.position;
        //Calculate angle between (0,1,0) vector and direction, always positive value
        //If click is on the right side of x axis the rotation must be negative
        float angle = targetDir.x < 0 ? Vector3.Angle(targetDir, Vector3.up) : Vector3.Angle(targetDir, Vector3.up) * -1f;
        rb.MoveRotation(Quaternion.Euler(0, 0, angle));
        //Change state
        state = CharacterState.BEFORE_FARTING;
        Debug.Log("CharacterState:" + state);
    }

    public void fart()
    {
        //Reset angle ajustment timer
        timerAdjustAngle = 0;
        //Stop if reach target
        if (targetDir.magnitude < transform.localScale.y && state == CharacterState.FARTING) stopFarting();
    }

    public void stopFarting()
    {
        //Reset angle ajustment timer
        //timerAdjustAngle = 0;
        //Reset velocity
        //rb.velocity = Vector3.zero;
        if (state == CharacterState.FARTING) {
            state = CharacterState.IDLE;
            Debug.Log("CharacterState:" + state);
        }
    }

    private void adjustAngle()
    {
        if (timerAdjustAngle > 1f) return;
        rb.rotation = Quaternion.Lerp(rb.rotation, Quaternion.AngleAxis(-rb.rotation.z, Vector3.forward), timerAdjustAngle);
        timerAdjustAngle += Time.deltaTime;
    }
}
