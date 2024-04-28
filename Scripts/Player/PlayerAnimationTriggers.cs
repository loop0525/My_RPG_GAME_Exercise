using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationTriggers : MonoBehaviour
{
    private Player player => GetComponentInParent<Player>();
    // 动画帧触发函数
    private void AnimationTrigger()
    {
        player.AnimationTrigger();
    }

    // 攻击关键帧触发事件
    private void AttackTrigger()
    {
        // 检查 Collider 是否落在圆形区域内。返回落入的Collider
        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.attackCheck.position, player.attackCheckRadius);

        foreach(var hit in colliders)
        {
            // 如果攻击范围有敌人，调用敌人受伤函数
            if(hit.GetComponent<Enemy>() != null)
            {
                hit.GetComponent<Enemy>().Damage();
            }
        }
    }
    // 扔剑动画触发
    private void ThrowSword()
    {
        SkillManager.instance.sword.CreateSword();
    }
}
