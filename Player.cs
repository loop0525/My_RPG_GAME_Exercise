using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Tilemaps;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Attack detail info")]
    public Vector2[] attackMovement;

    public bool isBusy {  get; private set; }

    [Header("Move info")]
    public float moveSpeed = 7f;
    public float jumpForce = 12f;

    [Header("Dash info")]
    [SerializeField] private float dashCoooldown = 1f;
    private float dashUsageTimer;
    public float dashSpeed = 25f;
    public float dashDuration = 0.2f;
    public float dashDir { get; private set; }

    [Header("Collision info")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private float wallCheckDistance;
    [SerializeField] private LayerMask wallLayerMask;

    public int facingDir { get; private set; } = 1;
    private bool facingRight = true;


    #region Components
    public Animator anim {  get; private set; }
    public Rigidbody2D rb { get; private set; }
    #endregion


    #region States
    public PlayerStateMachine stateMachine {  get; private set; }

    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerAirState   airState { get; private set; }
    public PlayerWallSlideState wallSlideState { get; private set; }
    public PlayerWallJumpState wallJumpState { get; private set; }
    public PlayerDashState dashState { get; private set; }
    public PlayerPrimaryAttack primaryAttackState { get; private set; }

    #endregion

    private void Awake()
    {
        stateMachine = new PlayerStateMachine();

        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        jumpState = new PlayerJumpState(this, stateMachine, "Jump");
        airState = new PlayerAirState(this, stateMachine, "Jump");
        dashState = new PlayerDashState(this, stateMachine, "Dash");
        wallSlideState = new PlayerWallSlideState(this, stateMachine, "WallSlide");
        wallJumpState = new PlayerWallJumpState(this, stateMachine, "Jump");
        primaryAttackState = new PlayerPrimaryAttack(this, stateMachine, "Attack");
    }

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();

        stateMachine.Initialize(idleState);
    }

    private void Update()
    {
        stateMachine.currentState.Update();

        CheckForDashInput();

    }

    // 协程，用于忙时间延时
    public IEnumerator BusyFor(float _seconds)
    {
        isBusy = true;
        yield return new WaitForSeconds(_seconds);
        isBusy = false;
    }

    // 动画帧触发调用
    public void AnimationTrigger() => stateMachine.currentState.AnimationFinishTrigger();

    // 检测冲刺方向，切换冲刺状态
    private void CheckForDashInput()
    {
        dashUsageTimer -= Time.deltaTime;

        // 放在时间计算后面保证冲刺冷却正常
        if(IsWallDetected())
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && dashUsageTimer<0)
        {
            dashUsageTimer = dashCoooldown;

            dashDir = Input.GetAxisRaw("Horizontal");
            if(dashDir == 0) // 如果没有移动就按当前方向冲刺
            {
                dashDir = facingDir;
            }
            stateMachine.ChangeState(dashState);
        }
    }
    #region Velocity
    // 设置速度
    public void ZeroVelocity()
    {
        rb.velocity = new Vector2(0, 0);
    }
    public void SetVelocity(float _xVelocity, float _yVelocity)
    {
        rb.velocity = new Vector2(_xVelocity, _yVelocity);
        FlipController(_xVelocity);
    }
    #endregion
    #region Collsion
    // 碰撞检测
    public bool IsGroundDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, groundLayerMask);
    public bool IsWallDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, wallCheckDistance, wallLayerMask);

    // 画线
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + (facingDir * wallCheckDistance), wallCheck.position.y));
    }
    #endregion
    #region Flip
    // 翻转
    public void Flip()
    {
        facingDir = facingDir * -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }
    // 翻转控制
    public void FlipController(float _x)
    {
        if(_x >0 && !facingRight)
            Flip();
        else if(_x <0 && facingRight) 
            Flip();
    }
    #endregion
}
