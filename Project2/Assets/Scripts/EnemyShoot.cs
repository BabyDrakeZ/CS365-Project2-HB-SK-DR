using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    //public GameObject 
    public GameObject ShootPoint;
    public GameObject enemyBullet;

    private bool inCoolDown = false;
    public float coolDown = 5;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (inCoolDown == false)
        {

            inCoolDown = true;
            int temp = Random.Range(0, 2);
            if (temp == 0)
            {
                GameObject go = Instantiate(enemyBullet);
                //go.transform.SetPositionAndRotation(shootPoint.transform.position, shootPoint.transform.rotation);
                go.transform.position = ShootPoint.transform.position;
                go.transform.rotation = ShootPoint.transform.rotation;
                EnemyBullet b = go.GetComponent<EnemyBullet>();
                b.speed = 5;
            }

            StartCoroutine(CoolDown());

            var euler = transform.eulerAngles;
            euler.z = Random.Range(90, 180);
            ShootPoint.transform.eulerAngles = euler;
        }
    }

    IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(coolDown);
        inCoolDown = false;
    }
}
