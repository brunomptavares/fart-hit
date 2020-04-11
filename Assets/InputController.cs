using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    private Character character;

    // Start is called before the first frame update
    void Awake()
    {
        character = GetComponent<Character>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            character.setTarget(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        } else if (Input.GetMouseButtonUp(0)) {
            character.stopFarting();
        }
    }
}
