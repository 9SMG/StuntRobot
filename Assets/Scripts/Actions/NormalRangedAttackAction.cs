using UnityEngine;

public class NormalRangedAttackAction : RobotAction
{
    public NormalRangedAttackAction() : base() { }
    public NormalRangedAttackAction(string msg) : base(msg) { }

    public override void PlayAction()
    {
        if(RobotActionable is IRangedActionable)
        {
            Debug.Log($"Name: {_actionName}");
            (RobotActionable as IRangedActionable)?.RangedAttack();
        }
    }
}
