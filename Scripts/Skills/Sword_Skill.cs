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
            // 归一化使相对位置在1内再乘以设置速度
            finalDir = new Vector2(AimDirection().normalized.x * launchForce.x, AimDirection().normalized.y * launchForce.y);
        }

        if (Input.GetKey(KeyCode.Mouse1))
        {
            for (int i = 0; i < dots.Length; i++)
            {
                // 设置瞄准点位置(每个描点的位置由传入的时间参数决定)
                dots[i].transform.position = DotsPosition(i * spaceBeetwenDots);
            }
        }
    }


    public void CreateSword()
    {
        GameObject newSword = Instantiate(swordPrefab, player.transform.position, transform.rotation);

        Sword_Skill_Controller sword_Skill_Controller = newSword.GetComponent<Sword_Skill_Controller>();
        sword_Skill_Controller.SetupSword(finalDir, swordGravity);

        DotsActive(false); // 关闭瞄准点
    }

    // 获得瞄准相对player位置
    public Vector2 AimDirection()
    {
        Vector2 playerPosition = player.transform.position;                          // player位置
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); // 鼠标瞄准位置(参考点mouse从屏幕坐标系转换到世界坐标系)
        Vector2 direction = mousePosition - playerPosition;

        return direction;
    }

    // 设置瞄准点显示状态
    public void DotsActive(bool _isActive)
    {
        for (int i = 0; i < dots.Length; i++)
        {
            dots[i].SetActive(_isActive);
        }
    }

    // 生成瞄准点
    public void GenereateDots()
    {
        dots = new GameObject[numberOfDots];
        for (int i = 0;i < dots.Length; i++)
        {
            dots[i] = Instantiate(dotPrefab, player.transform.position, Quaternion.identity, dotsParent);
            dots[i].SetActive(false);
        }
    }

    // 计算给定时间点瞄准点位置
    private Vector2 DotsPosition(float t)
    {
        Vector2 position = (Vector2)player.transform.position + new Vector2(
            AimDirection().normalized.x * launchForce.x,
            AimDirection().normalized.y * launchForce.y) * t +    // 计算经过时间t瞄准点的位置
            0.5f * (Physics2D.gravity * swordGravity) * (t * t);  // 受重力影响1/2*(g)*(t*t)
        return position;
    }

}
