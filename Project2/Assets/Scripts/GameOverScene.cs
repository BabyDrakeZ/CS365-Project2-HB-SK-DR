using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverScene : MonoBehaviour
{
    public TMP_Text GameOverTxt;
    public TMP_Text PlayerTimeTxt;
    public TMP_Text HighTimeTxt;

    // Start is called before the first frame update
    void Start()
    {
        Constants.C.playing = false;
        PlayerTimeTxt.text = "Time: " + Constants.C.timeCount.ToString("0.00");
        HighTimeTxt.text = "Best Time: " + Constants.C.highTime.ToString("0.00");
        PlayerPrefs.SetFloat("highTime", Constants.C.highTime);
    }

    // Update is called once per frame
    void Update()
    {
        WaitResponse();
    }

    void WaitResponse()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("StartScene");
        }

    }
}

