using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerShoot : MonoBehaviour
{
    bool hasShot = false;
    public float angleDivisor = 10;
    public GameObject paddleShot;
    public GameObject bulletPrefab;
    private GameObject shot;
    private PaddleMove paddle;
    public TMP_Text stopwatch;
    public float time=0f;
    
    // Start is called before the first frame update
    void Start()
    {
        paddle = GameObject.FindGameObjectWithTag("Player").GetComponent<PaddleMove>();
        stopwatch.text = time.ToString("0.00");
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        stopwatch.text = time.ToString("0.00");
        bool bulletDestroyCase = Input.GetKey(KeyCode.Return); //Set to occur when bullet is off screne.
        if (bulletDestroyCase)
            BulletReset();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
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
    public void BulletReset()
    {
        if (hasShot)
        {
            Destroy(shot);
            paddleShot.SetActive(true);
            hasShot = false;
            //lives.text = "Press Enter to retrieve new ball. Lives: " + livescount;
        }
    }
}
