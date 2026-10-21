using UnityEngine;

public class TargetController : MonoBehaviour
{
    Target target;
    [SerializeField] TargetHit targetHit;

    public void AddTarget(Target newTarget)
    {  
        target = newTarget;
        if(targetHit == null)
            targetHit = gameObject.AddComponent<TargetHit>();
    }
}
