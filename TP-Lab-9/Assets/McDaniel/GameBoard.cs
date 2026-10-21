using UnityEngine;

public class GameBoard : MonoBehaviour
{
    [SerializeField] Builder builder;

    [SerializeField] Vector2 gameSpots;

    private void Start()
    {
        builder.BuildTargets();
    }
}
