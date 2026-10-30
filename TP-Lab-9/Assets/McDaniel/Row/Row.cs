using System.Collections.Generic;
using UnityEngine;

public class Row : MonoBehaviour
{
    //References
    [SerializeField] Builder builder;
    [SerializeField] GameObject endPosition;

    List<Vector3> targetPositions = new();
    [HideInInspector] public List<Vector3> Targets { get { return targetPositions; } set { targetPositions = Targets; } }

    [HideInInspector] public Vector3 endLPos;
    [HideInInspector] public Vector3 endRPos;
    [HideInInspector] public bool newGame;

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

    public void StartBuild(bool newGame)
    {
        if (newGame)
        {
            print("newGame");
            BuildRow();
        }
        else
        {
            print("not");
        }
    }

    private void BuildRow()
    {
        if (builder == null)
            return;

        //Initial spot
        float xPosition = transform.position.x;

        //Build first target
        builder.BuildTarget(new Vector3(xPosition, transform.position.y, 0), targetType, direction);
        amountPerRow--;
        targetPositions.Add(new Vector3(xPosition, transform.position.y, 0));

        float xPosition2 = transform.position.x;

        while(amountPerRow > 0)
        {
            //Go to next position
            xPosition += spaceBetween;

            //Build target at row
            builder.BuildTarget(new Vector3(xPosition, transform.position.y, 0), targetType, direction);
            amountPerRow--;
            targetPositions.Add(new Vector3(xPosition, transform.position.y, 0));

            //Check if can still build target
            if (amountPerRow <= 0)
                break;

            //Go to next position
            xPosition2 -= spaceBetween;

            //Build target at row
            builder.BuildTarget(new Vector3(xPosition2, transform.position.y, 0), targetType, direction);
            targetPositions.Add(new Vector3(xPosition2, transform.position.y, 0));
            amountPerRow--;
        }

        //Go to next positions
        xPosition += spaceBetween;

        //Spawn in the endPosition 
        GameObject endR = Instantiate(endPosition, new Vector3(xPosition, transform.position.y, 0), transform.rotation);
        GameObject endL = Instantiate(endPosition, new Vector3(xPosition2, transform.position.y, 0), transform.rotation);

        endLPos = new Vector3(xPosition, transform.position.y, 0);
        endRPos = new Vector3(xPosition2, transform.position.y, 0);

        endR.GetComponent<EndPosition>().ConnectEndPositions(endL, direction, Direction.Right);

        newGame = false;
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