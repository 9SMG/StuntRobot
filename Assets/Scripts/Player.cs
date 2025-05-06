using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour, IMeleeActionable, IRangedActionable
{
    List<RobotAction> robotActions;
    private int _currRobotActionIdx;
    private int _maxRobotActionCnt;

    [Header("Movement")]
    public float moveSpeed = 20f;

    [Header("Attack")]
    GameObject attackEffectPrefab;
    Transform attackPoint;
    public float attackCooldown = 0.5f;

    private Vector2 moveInput;
    private Rigidbody2D rb;

    //private PlayerInputActions inputActions;

    GameObject meleeWeapon;
    GameObject RangeWeapon;
    

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        
        InputSystem.actions.FindAction("Move").performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        InputSystem.actions.FindAction("Move").canceled += ctx => moveInput = Vector2.zero;

        InputSystem.actions.FindAction("Attack").performed += ctx => TryAttack();

        InitAction();

        _deltaAttackedTime = 0;
    }

    private void Start()
    {
        meleeWeapon = transform.GetChild(2).gameObject;
        RangeWeapon = transform.GetChild(3).gameObject;
    }

    float _deltaAttackedTime;
    private void Update()
    {
        _deltaAttackedTime += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        rb.linearVelocity = moveInput * moveSpeed;
    }

    private void TryAttack()
    {
        if (_deltaAttackedTime < attackCooldown)
            return;

        _deltaAttackedTime = 0;

        //// 공격 효과 예시 (이펙트 또는 타격 처리)
        //if (attackEffectPrefab && attackPoint)
        //    Instantiate(attackEffectPrefab, attackPoint.position, Quaternion.identity);

        UseAction();

        Debug.Log("Attack!");
    }

    void InitAction()
    {
        if(robotActions == null)
        {
            robotActions = new List<RobotAction>();
        }
        else
        {
            robotActions.Clear();
        }

        ////Add
        //for(int i = 0; i < 99; i++)
        //{
        //    robotActions.Add();
        //}

        robotActions.Add(new NormalMeleeAttackAction("Melee1"));
        robotActions.Add(new NormalRangedAttackAction("Ranged1"));
        robotActions.Add(new NormalMeleeAttackAction("Melee2"));
        robotActions.Add(new NormalMeleeAttackAction("Melee3"));
        robotActions.Add(new NormalRangedAttackAction("Ranged2"));

        _maxRobotActionCnt = robotActions.Count;
        _currRobotActionIdx = 0;

        robotActions[_currRobotActionIdx].RobotActionable = this;
    }

    void UseAction()
    {
        if (robotActions.Count == 0)
            return;

        robotActions[_currRobotActionIdx].PlayAction();
        NextAction();
    }

    void NextAction()
    {
        if (robotActions.Count == 0)
            return;

        _currRobotActionIdx = (_currRobotActionIdx + 1) % _maxRobotActionCnt;
        robotActions[_currRobotActionIdx].RobotActionable = this;
    }

    public void ReadyMeleeAttack()
    {
        Debug.Log("ReadyMeleeAttack()");
    }

    public void MeleeAttack()
    {
        Debug.Log("MeleeAttack()");
    }

    public void ReadRangedAttack()
    {
        Debug.Log("ReadRangedAttack()");
    }

    public void RangedAttack()
    {
        Debug.Log("RangedAttack()");
    }

    //public void MeleeAttack()
    //{
    //    Debug.Log("MeleeAttack()");
    //    ///throw new System.NotImplementedException();
    //}

    //public void RangedAttack()
    //{
    //    Debug.Log("RangedAttack()");
    //    //throw new System.NotImplementedException();
    //}

    //public void SpecialAttack()
    //{
    //    Debug.Log("SpecialAttack()");
    //    //throw new System.NotImplementedException();
    //}
}
