using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScene : MonoBehaviour
{
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Constants.C.playing = true;
            UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
        }
    }
}
