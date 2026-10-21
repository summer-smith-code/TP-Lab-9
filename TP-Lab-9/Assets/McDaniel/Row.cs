using UnityEngine;

public class Row : MonoBehaviour
{
    //References
    [SerializeField] Builder builder;

    //Spacing
    [SerializeField] float spaceBetween;
    [SerializeField] int amountPerRow;

    private void Awake()
    {
        if (builder == null)
            builder = GameObject.FindFirstObjectByType<Builder>();
    }

    private void Start()
    {
        builder.BuildTarget();
    }

    public void BuildRow()
    {

    }
}
