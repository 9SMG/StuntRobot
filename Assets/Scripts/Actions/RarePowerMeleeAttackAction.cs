using UnityEngine;

public class RarePowerMeleeAttackAction : RobotAction
{
    public RarePowerMeleeAttackAction() : base() { }
    public RarePowerMeleeAttackAction(string msg) : base(msg) { }

    public override void PlayAction()
    {
        if (RobotActionable is IMeleeActionable)
        {
            Debug.Log($"Name: {_actionName}");
            (RobotActionable as IMeleeActionable)?.PowerMeleeAttack();
        }
    }
}
