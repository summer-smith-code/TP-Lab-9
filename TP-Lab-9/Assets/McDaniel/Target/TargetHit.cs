using UnityEngine;

public class TargetHit : MonoBehaviour
{
    public delegate void HitTarget(int score);
    public event HitTarget OnHit;

    //Runs when target is hit Add observer behaviour here
    public void Hit()
    {
        OnHit?.Invoke(1);
        Destroy(gameObject);
    }
}