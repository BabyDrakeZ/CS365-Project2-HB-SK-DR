using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants : MonoBehaviour
{
    // Start is called before the first frame update
    static public Constants C;
    public float timeCount;
    public float highTime;
    public bool playing = false;
    // Start is called before the first frame update
    void Start()
    {
        C = this;
        if (PlayerPrefs.HasKey("highTime"))
        {
            highTime = PlayerPrefs.GetFloat("highTime");
        }
    }

        // Update is called once per frame
    void Update()
    {
        if (playing)
        {
            timeCount = Time.timeSinceLevelLoad;
            if (timeCount > highTime) highTime = timeCount;
        }
    }
}
