using UnityEngine;

public class NormalTarget : TargetBuilder
{
    public override void BuildTarget()
    {
        target = new Target();

        target.points = 4;
        target.speed = 4;
        target.size = 1;
    }
}
