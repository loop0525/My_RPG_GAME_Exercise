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

    // �����ؼ�֡�����¼�
    private void AttackTrigger()
    {
        // ��� Collider �Ƿ�����Բ�������ڡ����������Collider
        Collider2D[] colliders = Physics2D.OverlapCircleAll(enemy.attackCheck.position, enemy.attackCheckRadius);

        foreach (var hit in colliders)
        {
            // ���������Χ�е��ˣ����õ������˺���
            if (hit.GetComponent<Player>() != null)
            {
                hit.GetComponent<Player>().Damage();
            }
        }
    }

    // �������ڵĴ�������
    private void OpenCounterWindow() => enemy.OpenCounterAttackWindow();
    private void CloseCounterWindow() => enemy.CloseCounterAttackWindow();
}
