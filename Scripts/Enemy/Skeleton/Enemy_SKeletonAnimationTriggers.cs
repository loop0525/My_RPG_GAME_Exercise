using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_SKeletonAnimationTriggers : MonoBehaviour
{
    private Enemy_Skeleton enemy => GetComponentInParent<Enemy_Skeleton>();

    private void AnimationTrigger()
    {
        enemy.AnimationFinishTrigger();
    }

    // 攻击关键帧触发事件
    private void AttackTrigger()
    {
        // 检查 Collider 是否落在圆形区域内。返回落入的Collider
        Collider2D[] colliders = Physics2D.OverlapCircleAll(enemy.attackCheck.position, enemy.attackCheckRadius);

        foreach (var hit in colliders)
        {
            // 如果攻击范围有敌人，调用敌人受伤函数
            if (hit.GetComponent<Player>() != null)
            {
                hit.GetComponent<Player>().Damage();
            }
        }
    }

    // 攻击窗口的触发调用
    private void OpenCounterWindow() => enemy.OpenCounterAttackWindow();
    private void CloseCounterWindow() => enemy.CloseCounterAttackWindow();
}
