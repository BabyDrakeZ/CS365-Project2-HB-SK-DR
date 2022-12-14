using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    //DEFINES A COLUMN OF BRICKS
    public float width = 3.3f;
    public float height = 0.9f;
    public float padding = 0.1f;
    public float offset = 0;
    public int numColumns = 6;
    public GameObject[] bricks;
    public PaddleMove paddle;
    public TMPro.TMP_Text stopwatch;
    public int blocksRequired = 108;

    // Start is called before the first frame update
    void Start()
    {
        paddle = GameObject.FindGameObjectWithTag("Player").GetComponent<PaddleMove>();
        //render bricks from top left corner (where this manager is)
        for (int i = 0; i < numColumns; i++)
        {
            for (int j = 0; j < bricks.Length; j++)
            {
                try
                {
                    GameObject brick = Instantiate(bricks[j]);
                    brick.transform.SetParent(this.transform);
                    Debug.Log(brick.ToString() + "[" + i.ToString() + "," + j.ToString() + "]");
                    brick.transform.position = new Vector3(this.transform.position.x + i * (width+padding), this.transform.position.y - j * (height+padding), 0);
                } catch { Debug.Log("Failed to Instantiate[" + i.ToString() + "," + j.ToString() + "]"); }
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (paddle.GetComponent<PaddleMove>().blocksBroken >= blocksRequired)
        {
            Constants.C.EndTimer();
            UnityEngine.SceneManagement.SceneManager.LoadScene("GameOverScene");
        }
        Constants.C.UpdateTimer();
        stopwatch.text = Constants.C.timeCount.ToString("0.00");
    }
}
