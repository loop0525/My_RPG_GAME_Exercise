using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clone_Skill_Controller : MonoBehaviour
{
    private SpriteRenderer sr;
    private Animator anim;

    private float cloneTimer;

    private float colorLoosingSpeed;

    [SerializeField] private Transform attackCheck;
    [SerializeField] private float attackCheckRadius;

    private Transform closetEnemy;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        cloneTimer -= Time.deltaTime;
        if (cloneTimer < 0)
        {
            // 如果克隆时间到了就开始将Sprite逐渐设置不透明
            sr.color = new Color(1, 1, 1, sr.color.a - (Time.deltaTime * colorLoosingSpeed));
            // 完全透明就删除对象
            if (sr.color.a <= 0)
                Destroy(gameObject);
        }
    }

    public void SetupClone(Transform _newTransform, float _cloneDuration, float _colorLoosingSpeed, bool _canAttakk)
    {
        if (_canAttakk)
        {
            anim.SetInteger("AttackNumber", Random.Range(1, 4));
        }

        transform.position = _newTransform.position;

        cloneTimer = _cloneDuration;
        colorLoosingSpeed = _colorLoosingSpeed;

        // 面对最近敌人
        FaceClosetTarget();
    }

    // 动画帧触发函数
    private void AnimationTrigger()
    {
        cloneTimer = -.1f; // 攻击完成直接完成clone时间
    }

    // 攻击关键帧触发事件
    private void AttackTrigger()
    {
        // 检查 Collider 是否落在圆形区域内。返回落入的Collider
        Collider2D[] colliders = Physics2D.OverlapCircleAll(attackCheck.position, attackCheckRadius);

        foreach (var hit in colliders)
        {
            // 如果攻击范围有敌人，调用敌人受伤函数
            if (hit.GetComponent<Enemy>() != null)
            {
                hit.GetComponent<Enemy>().Damage();
            }
        }
    }

    // 找到最近敌人的方向让clone面对敌人
    private void FaceClosetTarget()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 25); // 检测范围25就差不多了

        float closetDistance = Mathf.Infinity;

        // 找出最近敌人
        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Enemy>() != null)
            {
                float distanceToEnemy = Vector2.Distance(transform.position, hit.transform.position);
                if (distanceToEnemy < closetDistance)
                {
                    closetDistance = distanceToEnemy;
                    closetEnemy = hit.transform;
                }
            }
        }

        // 处理clone面对敌人
        if(closetEnemy != null)
        {
            if(transform.position.x > closetEnemy.position.x)
            {
                transform.Rotate(0, 180, 0);
            }
        }

    }
}
