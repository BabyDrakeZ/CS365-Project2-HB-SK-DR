using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    public float speed;
    public Vector3 direction;
    public float boundX = 20;
    public float boundY = 8;
    public Manager manager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (offScreenLeft(0.5f) && this.direction.x < 0)
        {
            Reflect(Vector2.right);
            Debug.Log("offLeft");
        }
        if (offScreenRight(0.5f) && this.direction.x > 0)
        {
            Reflect(Vector2.left);
            Debug.Log("offRight");
        }
        if (offScreenTop(0.5f) && this.direction.y > 0)
        {
            Reflect(Vector2.down);
            Debug.Log("offTop");
        }
        this.transform.position += speed * Time.deltaTime * direction;
        
    }
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject obj = collision.gameObject;
        Vector2 temp = collision.ClosestPoint(this.transform.position);
        Vector3 normal = this.transform.position - new Vector3(temp.x, temp.y, 0);
        Debug.Log("collision normal: " + normal.ToString());
        normal.Normalize();

        if (obj.tag == "Wall" || obj.tag == "WallTop")
        {
            Debug.Log("collision normal: " + normal.ToString());
            Reflect(normal);
        }
        if (obj.tag == "Player")
        {
            PaddleMove script = obj.GetComponent<PaddleMove>();
            normal = new Vector3(script.actualSpeed + normal.x, 1, 0);
            normal.Normalize();
            Debug.Log("collision normal2: " + normal.ToString());
            this.direction = new Vector3(this.direction.x, -this.direction.y, 0);
        }
        if (obj.tag == "Brick")
        {
            Reflect(normal);
            Destroy(obj);
        }
    }

    private bool offScreenRight(float tolerance = 0.5f)
    {
        return (this.transform.position.x + tolerance > boundX);
    }
    private bool offScreenLeft(float tolerance = 0.5f)
    {
        return (this.transform.position.x - tolerance < -boundX);
    }
    private bool offScreenTop(float tolerance = 0.5f)
    {
        return (this.transform.position.y + tolerance > boundY);
    }

    void Reflect(Vector3 normal)
    {
        direction -= 2 * (Vector3.Dot(normal, direction)) * normal;
        Debug.Log("Reflecting " + direction.ToString());
    }
}
