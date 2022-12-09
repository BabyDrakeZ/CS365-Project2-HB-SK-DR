using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants : MonoBehaviour
{
    // Start is called before the first frame update
    static public Constants C;
    public float timeCount = 0;
    public float highTime = 10000;
    public bool playing = false;
    // Start is called before the first frame update
    void Start()
    {
        C = this;
        if (PlayerPrefs.HasKey("highTime"))
        {
            highTime = PlayerPrefs.GetFloat("highTime");
        }
        UpdateTimer();
    }
    public void UpdateTimer()
    {
        if (playing)
        {
            timeCount = Time.timeSinceLevelLoad;
        }
    }
    public void EndTimer()
    {
        playing = false;
        if (timeCount < highTime)
        {
            highTime = timeCount;
        }
    }

        // Update is called once per frame
    void Update()
    {
    }
}
