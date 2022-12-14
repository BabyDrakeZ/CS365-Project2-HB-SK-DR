using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{

    public Vector2 direction;
    public float speed = 5f;
    public float duration = 6f;

    public ParticleSystem ImpactPS;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Despawn());
        
    }
    IEnumerator Despawn()
    {
        yield return new WaitForSeconds(duration);
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector2 currentPosition = Camera.main.WorldToScreenPoint(this.transform.position);

        Vector3 newPosition = new Vector3(speed * transform.up.x * Time.deltaTime, speed * transform.up.y * Time.deltaTime, 0);
        this.transform.position += newPosition;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject gameObject = collision.gameObject;
        GameObject obj = collision.gameObject;
        Vector2 temp = collision.ClosestPoint(this.transform.position);
        Vector3 normal = this.transform.position - new Vector3(temp.x, temp.y, 0);

        if (gameObject.tag == "Player")
        {
            //ImpactPS = Instantiate(ImpactPS);
            //ImpactPS.transform.position = BulletPosition;
            //var euler = transform.eulerAngles;
            //euler.z = this.transform.rotation.eulerAngles.z;
            //ImpactPS.transform.eulerAngles = euler;
            //ImpactPS.Play();

            PaddleMove paddle = obj.GetComponent<PaddleMove>();
            paddle.Disable();
        }
    }
    void Reflect(Vector3 normal)
    {
        //direction -= 2 * (Vector3.Dot(normal, direction)) * normal;
        Debug.Log("Reflecting " + direction.ToString());
    }
}
