using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    private GameObject cam;
    // 背景相对摄像机位置的影响变量
    [SerializeField] private float parallaxEffect;

    private float length;

    private float xPosition;

    private float distanceMove;
    private float distanceToMove;

    // Start is called before the first frame update
    void Start()
    {
        // 获取摄像机对象
        cam = GameObject.Find("Main Camera");
        // 获取背景长度
        length = GetComponent <Renderer>().bounds.size.x;
        // 当前背景位置
        xPosition = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        // 背景和摄像机的相对距离
        distanceMove = cam.transform.position.x * (1 - parallaxEffect);
        // 背景衰减移动距离
        distanceToMove = cam.transform.position.x * parallaxEffect;

        transform.position = new Vector3(xPosition +  distanceToMove, transform.position.y);

        if(distanceMove > xPosition + length)
        {
            xPosition += length;
        }
        else if(distanceMove < xPosition - length)
        {
            xPosition -= length;
        }
    }
}
