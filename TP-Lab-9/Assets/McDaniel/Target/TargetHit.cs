using UnityEngine;

public class TargetHit : MonoBehaviour
{
    //Runs when target is hit Add observer behaviour here
    public void Hit()
    {
        Destroy(gameObject);
    }
}