using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword_Skill : Skill
{
    [Header("Sword info")]
    [SerializeField] private GameObject swordPrefab;
    [SerializeField] private Vector2 launchForce;
    [SerializeField] private float swordGravity;

    [Header("Aim dots")]
    [SerializeField] private int numberOfDots;
    [SerializeField] private float spaceBeetwenDots;
    [SerializeField] private GameObject dotPrefab;
    [SerializeField] private Transform dotsParent;

    private GameObject[] dots;

    private Vector2 finalDir;
    protected override void Start()
    {
        base.Start();

        GenereateDots();
    }

    protected override void Update()
    {
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            // ��һ��ʹ���λ����1���ٳ��������ٶ�
            finalDir = new Vector2(AimDirection().normalized.x * launchForce.x, AimDirection().normalized.y * launchForce.y);
        }

        if (Input.GetKey(KeyCode.Mouse1))
        {
            for (int i = 0; i < dots.Length; i++)
            {
                // ������׼��λ��(ÿ������λ���ɴ����ʱ���������)
                dots[i].transform.position = DotsPosition(i * spaceBeetwenDots);
            }
        }
    }


    public void CreateSword()
    {
        GameObject newSword = Instantiate(swordPrefab, player.transform.position, transform.rotation);

        Sword_Skill_Controller sword_Skill_Controller = newSword.GetComponent<Sword_Skill_Controller>();
        sword_Skill_Controller.SetupSword(finalDir, swordGravity);

        DotsActive(false); // �ر���׼��
    }

    // �����׼���playerλ��
    public Vector2 AimDirection()
    {
        Vector2 playerPosition = player.transform.position;                          // playerλ��
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); // �����׼λ��(�ο���mouse����Ļ����ϵת������������ϵ)
        Vector2 direction = mousePosition - playerPosition;

        return direction;
    }

    // ������׼����ʾ״̬
    public void DotsActive(bool _isActive)
    {
        for (int i = 0; i < dots.Length; i++)
        {
            dots[i].SetActive(_isActive);
        }
    }

    // ������׼��
    public void GenereateDots()
    {
        dots = new GameObject[numberOfDots];
        for (int i = 0;i < dots.Length; i++)
        {
            dots[i] = Instantiate(dotPrefab, player.transform.position, Quaternion.identity, dotsParent);
            dots[i].SetActive(false);
        }
    }

    // �������ʱ�����׼��λ��
    private Vector2 DotsPosition(float t)
    {
        Vector2 position = (Vector2)player.transform.position + new Vector2(
            AimDirection().normalized.x * launchForce.x,
            AimDirection().normalized.y * launchForce.y) * t +    // ���㾭��ʱ��t��׼���λ��
            0.5f * (Physics2D.gravity * swordGravity) * (t * t);  // ������Ӱ��1/2*(g)*(t*t)
        return position;
    }

}
