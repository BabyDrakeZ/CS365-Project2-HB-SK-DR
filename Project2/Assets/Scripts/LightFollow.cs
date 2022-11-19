using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFollow : MonoBehaviour
{
    private GameObject parent;
    public float speed = 18;
    private float actualSpeed = 0;
    public float maxSpeed = 18;
    public float lerpConstant = 0.04f;
    // Start is called before the first frame update
    void Start()
    {
        parent = this.transform.parent.gameObject;
        this.transform.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = new Vector3(parent.transform.position.x, parent.transform.position.y,0) - new Vector3(this.transform.position.x, this.transform.position.y, 0);
        
        if (dir != Vector3.zero && dir.magnitude >= 0.01)
        {
            actualSpeed = Mathf.Lerp(actualSpeed, speed, lerpConstant);
            actualSpeed = Mathf.Clamp(actualSpeed, 0, maxSpeed);
            this.transform.position += dir * actualSpeed * Time.deltaTime;
        }
    }
}
