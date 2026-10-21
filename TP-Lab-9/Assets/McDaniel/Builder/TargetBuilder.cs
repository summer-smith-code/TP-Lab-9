using UnityEngine;

public abstract class TargetBuilder
{
    //Target instance
    protected Target target;
    public Target Target
    { get { return target; } }

    //Build methods
    public abstract void BuildTarget();
}