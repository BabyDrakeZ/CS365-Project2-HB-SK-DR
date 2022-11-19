using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    public float speed;
    public Vector3 direction;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += speed * Time.deltaTime * direction;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject obj = collision.gameObject;
        Vector2 temp = collision.ClosestPoint(this.transform.position);
        Vector3 normal = this.transform.position - new Vector3(temp.x, temp.y, 0);
        normal.Normalize();

        if (obj.tag == "Wall" || obj.tag == "Player")
        {
            Debug.Log("collision normal: " + normal.ToString());
            Reflect(normal);
        }
    }
    void Reflect(Vector3 normal)
    {
        direction -= 2 * (Vector3.Dot(normal, direction)) * normal;
        Debug.Log("Reflecting " + direction.ToString());
    }
}
