using UnityEngine;

public class Row : MonoBehaviour
{
    //References
    [SerializeField] Builder builder;
    [SerializeField] GameObject endPosition;

    //Spacing
    [SerializeField] float spaceBetween;
    [SerializeField] int amountPerRow;

    //Type
    [SerializeField] TargetType targetType;
    [SerializeField] Direction direction;
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

        //Initial spot
        float xPosition = transform.position.x;

        //Build first target
        builder.BuildTarget(new Vector3(xPosition, transform.position.y, 0), targetType, direction);
        amountPerRow--;

        float xPosition2 = transform.position.x;

        while(amountPerRow > 0)
        {
            //Go to next position
            xPosition += spaceBetween;

            //Build target at row
            builder.BuildTarget(new Vector3(xPosition, transform.position.y, 0), targetType, direction);
            amountPerRow--;

            //Check if can still build target
            if (amountPerRow <= 0)
                break;

            //Go to next position
            xPosition2 -= spaceBetween;

            //Build target at row
            builder.BuildTarget(new Vector3(xPosition2, transform.position.y, 0), targetType, direction);
            amountPerRow--;
        }

        //Go to next positions
        xPosition += spaceBetween;

        //Spawn in the endPosition 
        GameObject firstEnd = Instantiate(endPosition, new Vector3(xPosition, transform.position.y, 0), transform.rotation);
        GameObject secondEnd = Instantiate(endPosition, new Vector3(xPosition2, transform.position.y, 0), transform.rotation);

        firstEnd.GetComponent<EndPosition>().ConnectEndPositions(secondEnd, direction, Direction.Right);
    }
}

public enum TargetType
{
    Small, 
    Normal, 
    Big
}

public enum Direction
{
    Left = -1,
    Right = 1
}