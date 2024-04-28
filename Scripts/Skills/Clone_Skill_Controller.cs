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
            // �����¡ʱ�䵽�˾Ϳ�ʼ��Sprite�����ò�͸��
            sr.color = new Color(1, 1, 1, sr.color.a - (Time.deltaTime * colorLoosingSpeed));
            // ��ȫ͸����ɾ������
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

        // ����������
        FaceClosetTarget();
    }

    // ����֡��������
    private void AnimationTrigger()
    {
        cloneTimer = -.1f; // �������ֱ�����cloneʱ��
    }

    // �����ؼ�֡�����¼�
    private void AttackTrigger()
    {
        // ��� Collider �Ƿ�����Բ�������ڡ����������Collider
        Collider2D[] colliders = Physics2D.OverlapCircleAll(attackCheck.position, attackCheckRadius);

        foreach (var hit in colliders)
        {
            // ���������Χ�е��ˣ����õ������˺���
            if (hit.GetComponent<Enemy>() != null)
            {
                hit.GetComponent<Enemy>().Damage();
            }
        }
    }

    // �ҵ�������˵ķ�����clone��Ե���
    private void FaceClosetTarget()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 25); // ��ⷶΧ25�Ͳ����

        float closetDistance = Mathf.Infinity;

        // �ҳ��������
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

        // ����clone��Ե���
        if(closetEnemy != null)
        {
            if(transform.position.x > closetEnemy.position.x)
            {
                transform.Rotate(0, 180, 0);
            }
        }

    }
}
