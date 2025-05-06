using UnityEngine;

public class NormalMeleeAttackAction : RobotAction
{
    public NormalMeleeAttackAction() : base() { }
    public NormalMeleeAttackAction(string msg) : base(msg) { }

    public override void PlayAction()
    {
        if (RobotActionable is IMeleeActionable)
        {
            Debug.Log($"Name: {_actionName}");
            (RobotActionable as IMeleeActionable)?.MeleeAttack();
        }
    }
}
