using UnityEngine;

public class BigTarget : TargetBuilder
{
    public override void BuildTarget()
    {
        target = new Target();

        target.points = 5;
        target.speed = 2;
        target.size = 5;
    }
}
