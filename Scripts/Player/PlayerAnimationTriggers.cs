using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationTriggers : MonoBehaviour
{
    private Player player => GetComponentInParent<Player>();
    // ����֡��������
    private void AnimationTrigger()
    {
        player.AnimationTrigger();
    }

    // �����ؼ�֡�����¼�
    private void AttackTrigger()
    {
        // ��� Collider �Ƿ�����Բ�������ڡ����������Collider
        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.attackCheck.position, player.attackCheckRadius);

        foreach(var hit in colliders)
        {
            // ���������Χ�е��ˣ����õ������˺���
            if(hit.GetComponent<Enemy>() != null)
            {
                hit.GetComponent<Enemy>().Damage();
            }
        }
    }
    // �ӽ���������
    private void ThrowSword()
    {
        SkillManager.instance.sword.CreateSword();
    }
}
