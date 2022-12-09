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
    public float difficultyRate = 5;
    public float coolDownAcceleration = 0.05f;
    void Start()
    {
        StartCoroutine(DifficultyRamp());
    }
    IEnumerator DifficultyRamp()
    {
        yield return new WaitForSeconds(difficultyRate);
        coolDown -= coolDownAcceleration;
        StartCoroutine(DifficultyRamp());
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
            }

            StartCoroutine(CoolDown());

            var euler = transform.eulerAngles;
            euler.z = Random.Range(120, 240);
            ShootPoint.transform.eulerAngles = euler;
        }
    }

    IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(coolDown);
        inCoolDown = false;
        coolDown = Mathf.Max(0, coolDown);
    }
}
