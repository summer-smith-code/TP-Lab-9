using UnityEngine;

public class Row : MonoBehaviour
{
    [SerializeField] Builder builder;

    private void Start()
    {
        builder.BuildTargets();
    }
}
