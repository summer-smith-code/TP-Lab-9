using UnityEngine;

public class Row : MonoBehaviour
{
    //References
    [SerializeField] Builder builder;

    //Spacing
    [SerializeField] float spaceBetween;
    [SerializeField] int amountPerRow;

    //Type
    [SerializeField] TargetType targetType;

    private void Awake()
    {
        //Get builder if not assigned
        if (builder == null)
            builder = GameObject.FindFirstObjectByType<Builder>();
    }

    private void Start()
    {
        BuildRow();
    }

    public void BuildRow()
    {
        if (builder == null)
            return;

        float xPosition = transform.position.x;

        builder.BuildTarget(new Vector3(xPosition, transform.position.y, 0), targetType);
        amountPerRow--;

        float xPosition2 = transform.position.x;

        while(amountPerRow > 0)
        {
            xPosition += spaceBetween;
            xPosition2 -= spaceBetween;

            //Build target at row
            builder.BuildTarget(new Vector3(xPosition, transform.position.y, 0), targetType);
            amountPerRow--;

            //Check if can still build target
            if (amountPerRow <= 0)
                break;
            builder.BuildTarget(new Vector3(xPosition2, transform.position.y, 0), targetType);
            amountPerRow--;
        }
    }
}

public enum TargetType
{
    Small, 
    Normal, 
    Big
}
