using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public float speed = 6f;
    public float delay = 3;
    public bool inCoolDown = false;
    public Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {
        direction.x = 0;
        direction.y = 0;
    }

    // Update is called once per frame
    void Update()
    {
        pickingDirection();
        Vector3 newPosition = new Vector3(speed * direction.x * Time.deltaTime, speed * direction.y * Time.deltaTime, 0);
        this.transform.position += newPosition;
    }

    private void pickingDirection()
    {
        if (inCoolDown == false)
        {
            inCoolDown = true;
            StartCoroutine(CoolDown());
            int temp = Random.Range(1, 4);
            if (temp == 1)
                StartCoroutine(moveRight());
            if (temp == 2)
                StartCoroutine(moveLeft());
        }
    }
    IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(delay*3);
        inCoolDown = false;
    }
    IEnumerator moveRight()
    {
        direction.x = 1;
        yield return new WaitForSeconds(delay);
        direction.x = -1;
        yield return new WaitForSeconds(delay);
        direction.x = 0;
    }
    IEnumerator moveLeft()
    {
        direction.x = -1;
        yield return new WaitForSeconds(delay);
        direction.x = 1;
        yield return new WaitForSeconds(delay);
        direction.x = 0;
    }
}