using UnityEngine;

public class SmallTarget : TargetBuilder
{
    public override void BuildTarget()
    {
        target = new Target();

        target.points = 5;
        target.speed = 6;
        target.size = 0.75f;
    }
}
