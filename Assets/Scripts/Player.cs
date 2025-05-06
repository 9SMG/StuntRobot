using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour, IMeleeActionable, IRangedActionable
{
    private List<RobotAction> _robotActions;
    private int _currRobotActionIdx;
    private int _maxRobotActionCnt;

    [Header("Movement")]
    public float moveSpeed = 10f;

    [Header("Attack")]
    private GameObject _attackEffectPrefab;
    private Transform _attackPoint;
    public float AttackCooldown = 0.5f;

    private Vector2 _moveInput;
    private Vector2 _lookInput;
    private Rigidbody2D _rb;

    //private PlayerInputActions inputActions;

    private GameObject _meleeWeapon;
    private GameObject _RangeWeapon;
    private GameObject _hand;
    

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        
        InputSystem.actions.FindAction("Move").performed += ctx => _moveInput = ctx.ReadValue<Vector2>();
        InputSystem.actions.FindAction("Move").canceled += ctx => _moveInput = Vector2.zero;

        InputSystem.actions.FindAction("Look").performed += ctx => _lookInput = ctx.ReadValue<Vector2>();
        InputSystem.actions.FindAction("Look").canceled += ctx => _lookInput = Vector2.zero;

        InputSystem.actions.FindAction("Attack").performed += ctx => TryAttack();

        _deltaAttackedTime = 0;

        _hand = transform.GetChild(0).GetChild(0).gameObject;
        _meleeWeapon = _hand.transform.GetChild(0).gameObject;
        _RangeWeapon = _hand.transform.GetChild(1).gameObject;
    }

    private void Start()
    {
        InitAction();
    }

    void InitAction()
    {
        if (_robotActions == null)
        {
            _robotActions = new List<RobotAction>();
        }
        else
        {
            _robotActions.Clear();
        }

        _robotActions.Add(new NormalMeleeAttackAction("Melee1"));
        _robotActions.Add(new NormalRangedAttackAction("Ranged1"));
        _robotActions.Add(new NormalMeleeAttackAction("Melee2"));
        _robotActions.Add(new NormalMeleeAttackAction("Melee3"));
        _robotActions.Add(new NormalRangedAttackAction("Ranged2"));

        _maxRobotActionCnt = _robotActions.Count;
        _currRobotActionIdx = 0;

        _robotActions[_currRobotActionIdx].RobotActionable = this;
        ReadyAction();
    }

    float _deltaAttackedTime;
    private void Update()
    {
        _deltaAttackedTime += Time.deltaTime;
        Look();
    }

    #region Player Look (Aim)
    bool _isGamepad = false;
    private void Look()
    {
        if (_isGamepad)
        {
            AimWithGamepad();
        }
        else
        {
            AimWithMouse();
        }
    }

    void AimWithMouse()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mouseWorldPos - _hand.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        _hand.transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    void AimWithGamepad()
    {
        if (_lookInput.sqrMagnitude > 0.1f)
        {
            float angle = Mathf.Atan2(_lookInput.y, _lookInput.x) * Mathf.Rad2Deg;
            _hand.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
    #endregion

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        _rb.linearVelocity = _moveInput * moveSpeed;
    }

    void SwitchMeleeWeapon() => SwitchWeapon(0);
    void SwitchRangedWeapon() => SwitchWeapon(1);
    void SwitchWeapon(int weaponType)
    {
        switch (weaponType)
        {
            case 0:
                _meleeWeapon.SetActive(true);
                _RangeWeapon.SetActive(false);
                break;
            case 1:
                _RangeWeapon.SetActive(true);
                _meleeWeapon.SetActive(false);
                break;
        }
    }
    private void TryAttack()
    {
        if (_deltaAttackedTime < AttackCooldown)
            return;

        _deltaAttackedTime = 0;

        //// 공격 효과 예시 (이펙트 또는 타격 처리)
        //if (attackEffectPrefab && attackPoint)
        //    Instantiate(attackEffectPrefab, attackPoint.position, Quaternion.identity);

        UseAction();

        Debug.Log("Attack!");
    }

    void UseAction()
    {
        if (_robotActions.Count == 0)
            return;

        _robotActions[_currRobotActionIdx].PlayAction();
        NextAction();
    }

    void NextAction()
    {
        if (_robotActions.Count == 0)
            return;

        _currRobotActionIdx = (_currRobotActionIdx + 1) % _maxRobotActionCnt;
        _robotActions[_currRobotActionIdx].RobotActionable = this;

        ReadyAction();
    }

    void ReadyAction()
    {
        if (_robotActions[_currRobotActionIdx] is NormalMeleeAttackAction
            || _robotActions[_currRobotActionIdx] is RarePowerMeleeAttackAction)
        {
            SwitchMeleeWeapon();
        }
        else if (_robotActions[_currRobotActionIdx] is NormalRangedAttackAction
            || _robotActions[_currRobotActionIdx] is RarePowerRangedAttackAction)
        {
            SwitchRangedWeapon();
        }
    }

    public void ReadyMeleeAttack()
    {
        Debug.Log("ReadyMeleeAttack()");
    }

    public void MeleeAttack()
    {
        Debug.Log("Player.MeleeAttack()");
        //GetComponentInChildren<IMeleeWeapon>().Attack();
        _meleeWeapon.GetComponent<INormalMeleeAttack>()?.MeleeAttack();
    }

    public void PowerMeleeAttack()
    {
        Debug.Log("Player.PowerMeleeAttack()");
        _meleeWeapon.GetComponent<IRarePowerMeleeAttack>()?.PowerMeleeAttack();
    }

    public void ReadRangedAttack()
    {
        Debug.Log("ReadRangedAttack()");
    }

    public void RangedAttack()
    {
        Debug.Log("RangedAttack()");
        _RangeWeapon.GetComponent<INormalRangedAttack>()?.RangedAttack();
    }

    public void PowerRangedAttack()
    {
        Debug.Log("Player.PowerRangedAttack()");
        _RangeWeapon.GetComponent<IRareRangedAttack>()?.PowerRangedAttack();
    }
}
