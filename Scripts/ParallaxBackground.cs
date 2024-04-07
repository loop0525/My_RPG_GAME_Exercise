using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    private GameObject cam;
    // ������������λ�õ�Ӱ�����
    [SerializeField] private float parallaxEffect;

    private float length;

    private float xPosition;

    private float distanceMove;
    private float distanceToMove;

    // Start is called before the first frame update
    void Start()
    {
        // ��ȡ���������
        cam = GameObject.Find("Main Camera");
        // ��ȡ��������
        length = GetComponent <Renderer>().bounds.size.x;
        // ��ǰ����λ��
        xPosition = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        // ���������������Ծ���
        distanceMove = cam.transform.position.x * (1 - parallaxEffect);
        // ����˥���ƶ�����
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
