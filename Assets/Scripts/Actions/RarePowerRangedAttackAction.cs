using UnityEngine;

public class RarePowerRangedAttackAction : RobotAction
{
    public RarePowerRangedAttackAction() : base() { }
    public RarePowerRangedAttackAction(string msg) : base(msg) { }

    public override void PlayAction()
    {
        if (RobotActionable is IRangedActionable)
        {
            Debug.Log($"Name: {_actionName}");
            (RobotActionable as IRangedActionable)?.PowerRangedAttack();
        }
    }
}
