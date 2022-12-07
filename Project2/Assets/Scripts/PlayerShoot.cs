using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    bool hasShot = false;
    public float angleDivisor = 10;
    public GameObject paddleShot;
    public GameObject bulletPrefab;
    private GameObject shot;
    private PaddleMove paddle;
    
    // Start is called before the first frame update
    void Start()
    {
        paddle = GameObject.FindGameObjectWithTag("Player").GetComponent<PaddleMove>();
    }

    // Update is called once per frame
    void Update()
    {
        bool bulletDestroyCase = Input.GetKey(KeyCode.Return); //Set to occur when bullet is off screne.
        if (bulletDestroyCase)
            BulletReset();
        if (Input.GetKeyDown(KeyCode.Space))
            Shoot();    
    }
    void Shoot()
    {
        if (!hasShot)
        {
            hasShot = true;
            shot = Instantiate(bulletPrefab);
            shot.transform.position = paddleShot.transform.position;
            shot.GetComponent<BulletMove>().direction = Direction();
            paddleShot.SetActive(false);
        }
    }
    Vector3 Direction()
    {
        Vector3 dir = Vector3.up + new Vector3(paddle.actualSpeed,0, 0)/angleDivisor;
        dir.Normalize();
        return dir;
    }
    private void BulletReset()
    {
        if (hasShot)
        {
            Destroy(shot);
            paddleShot.SetActive(true);
            hasShot = false;
        }
    }
}
