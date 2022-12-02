using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    public struct Brick
    {
        public float width;
        public GameObject prefab;
        public Brick(float width, GameObject prefab)
        {
            this.width = width;
            this.prefab = prefab;
        }
    }
    public Brick[] bricks;
    public GameObject[] bricks2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
