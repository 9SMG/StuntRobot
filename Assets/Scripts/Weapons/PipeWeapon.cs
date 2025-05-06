using UnityEngine;

public class PipeWeapon : MonoBehaviour, INormalMeleeAttack, IRarePowerMeleeAttack//ISwingable, IPowerSwingable
{
    private const string _animSwing = "Swing";
    private const string _animPowerSwing = "PowerSwing";

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    void Swing()
    {
        _animator.SetTrigger(_animSwing);
    }
    
    void PowerSwing()
    {
        _animator.SetTrigger(_animPowerSwing);
    }

    public void MeleeAttack()
    {
        Swing();
    }

    public void PowerMeleeAttack()
    {
        PowerSwing();
    }

}

