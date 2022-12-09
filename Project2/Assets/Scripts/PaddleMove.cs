using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleMove : MonoBehaviour
{
    public Vector2 direction = Vector2.zero; //used for determining which way the player is moving
    public float speed = 1;
    public float actualSpeed = 0;
    public float maxSpeed = 5;
    public float bounds = 5;
    public float lerpConstant = 0.8f;
    public bool disabled = false;
    public Light paddleLight;
    private float maxLight;
    private bool lightOff = false;
    public float blinkTime = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        maxLight = paddleLight.intensity;
    }

    // Update is called once per frame
    void Update()
    {
        if (lightOff)
        {
            paddleLight.intensity = Mathf.Lerp(paddleLight.intensity, 0.1f, lerpConstant);
        }
        else
        {
            paddleLight.intensity = Mathf.Lerp(paddleLight.intensity, maxLight, lerpConstant);
        }

        actualSpeed = Mathf.Lerp(actualSpeed, getInput(), lerpConstant);
        if (Mathf.Abs(actualSpeed) < 0.01) actualSpeed = 0;
        if (transform.position.x < -bounds && actualSpeed < 0) { actualSpeed = 0; }
        if (transform.position.x > bounds && actualSpeed > 0) { actualSpeed = 0; }
        actualSpeed = Mathf.Clamp(actualSpeed, -maxSpeed, maxSpeed);

        this.transform.position += Vector3.right * actualSpeed * Time.deltaTime;
    }
    float getInput()
    {
        float push = 0;
        bool left = (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) && transform.position.x > -bounds;
        bool right = (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) && transform.position.x < bounds;


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
    public void Disable()
    {
        Debug.Log("Player Disabled");
        if (!disabled) {
            StartCoroutine(DisablePaddle());
        }
    }
    private IEnumerator DisablePaddle()
    {
        disabled = true;
        StartCoroutine(Blink());
        yield return new WaitForSeconds(5);
        disabled = false;
        lightOff = false;
        yield return new WaitForSeconds(blinkTime + 0.1f);
        lightOff = false;
    }
    IEnumerator Blink()
    {
        if (lightOff)
        {
            yield return new WaitForSeconds(blinkTime);
        }
        else { yield return new WaitForSeconds(blinkTime / 2); }
        if (disabled)
        {
            lightOff = !lightOff;
            StartCoroutine(Blink());
        }
    }
}
