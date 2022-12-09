using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    public float speed;
    public Vector3 direction;
    public float boundX = 20;
    public float boundY = 8;
    public float english = 1;
    public float streakDecay = 2;
    private int streak = 0;
    public int streakMax = 5;
    public AudioSource bounceSound;
    public PaddleMove paddle;
    public PlayerShoot shoot;
    // Start is called before the first frame update
    void Start()
    {
        paddle = GameObject.FindGameObjectWithTag("Player").GetComponent<PaddleMove>();
        shoot = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerShoot>();

        StartCoroutine(DecayStreak());
        bounceSound = this.GetComponent<AudioSource>();
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
        if (offScreenBot(0.5f) && this.direction.y < 0)
        {
            paddle.Disable();
            StartCoroutine(resetBullet());
            Debug.Log("off");
        }
        this.transform.position += speed * (1+(streak/speed)) * Time.deltaTime * direction;
        
    }
    
    IEnumerator resetBullet()
    {
        yield return new WaitForSeconds(paddle.duration-paddle.blinkTime*1.75f);
        shoot.BulletReset();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject obj = collision.gameObject;
        Vector2 temp = collision.ClosestPoint(this.transform.position);
        Vector3 normal = this.transform.position - new Vector3(temp.x, temp.y, 0);
        Debug.Log("collision normal: " + normal.ToString());
        normal.Normalize();
        bounceSound.PlayOneShot(bounceSound.clip);
        if (obj.tag == "Wall" || obj.tag == "WallTop")
        {
            Debug.Log("collision normal: " + normal.ToString());
            Reflect(normal);
        }
        if (obj.tag == "Player")
        {
            PaddleMove script = obj.GetComponent<PaddleMove>();
            //normal = new Vector3(script.actualSpeed + normal.x + obj.transform.position.x*english, 1, 0);
            //normal.Normalize();
            Debug.Log("collision normal2: " + normal.ToString());
            if (!obj.GetComponent<PaddleMove>().disabled)
            {
                this.direction = new Vector3(this.direction.x + (this.transform.position.x - obj.transform.position.x) * english, -this.direction.y, 0).normalized;
            }
        }
        if (obj.tag == "Brick")
        {
            Reflect(normal);
            Destroy(obj);
            paddle.blocksBroken++;
            if (streak < streakMax)
                streak++;
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
    private bool offScreenBot(float tolerance = 0.5f)
    {
        return (this.transform.position.y - tolerance < -boundY);
    }

    void Reflect(Vector3 normal)
    {
        direction -= 2 * (Vector3.Dot(normal, direction)) * normal;
        Debug.Log("Reflecting " + direction.ToString());
    }
    IEnumerator DecayStreak()
    {
        //every second decrease streak by 1
        yield return new WaitForSeconds(streakDecay);
        if (streak > 0)
        {
            streak--;
        }
        StartCoroutine(DecayStreak());
    }
}
