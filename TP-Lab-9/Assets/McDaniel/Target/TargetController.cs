using UnityEngine;

public class TargetController : MonoBehaviour
{
    //Target scripts
    Target target;
    TargetMovement targetMovement;
    [SerializeField] TargetHit targetHit;

    public void Update()
    {
        //Move the player
        if (targetMovement != null)
        {
            targetMovement.Move();
        }
    }

    public void AddTarget(Target newTarget, Direction direction)
    {  
        //Add components
        target = newTarget;

        if(targetHit == null)
            targetHit = gameObject.AddComponent<TargetHit>();

        targetMovement = new TargetMovement(gameObject, target.speed, direction);

        //Set scale;
        transform.localScale *= target.size;
    }

    public void OnDestroy()
    {
        
    }
}
