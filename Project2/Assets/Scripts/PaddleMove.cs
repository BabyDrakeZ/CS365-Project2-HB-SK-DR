using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleMove : MonoBehaviour
{
    public Vector2 direction = Vector2.zero; //used for determining which way the player is moving
    public float speed = 1;
    public float actualSpeed = 0;
    public float maxSpeed = 5;
    public float lerpConstant = 0.8f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        actualSpeed = Mathf.Lerp(actualSpeed, getInput(), lerpConstant);
        if (Mathf.Abs(actualSpeed) < 0.01) actualSpeed = 0;
        actualSpeed = Mathf.Clamp(actualSpeed, -maxSpeed, maxSpeed);
        this.transform.position += Vector3.right * actualSpeed * Time.deltaTime;
    }
    float getInput()
    {
        float push = 0;
        bool left = (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow));
        bool right = (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow));


        if (left && right)
        {
            push = 0;
        } else if (left)
        {
            push = -speed;
        } else if (right)
        {
            push = speed;
        }
        return push;
    }
}
