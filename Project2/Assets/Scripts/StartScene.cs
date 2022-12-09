using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScene : MonoBehaviour
{
    public GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        Player = Instantiate(Player);
        Player.transform.position = new Vector3(-20, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
