using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Row : MonoBehaviour
{
    //References
    [SerializeField] Builder builder;
    [SerializeField] GameObject endPosition;

    [HideInInspector] public List<Vector3> targetPositions = new List<Vector3>();
    [HideInInspector] public List<GameObject> targets = new List<GameObject>();

    [HideInInspector] public Vector3 endLPos;
    [HideInInspector] public Vector3 endRPos;
    [HideInInspector] public bool newGame;

    public string _saveID;

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
            RebuildRow();
        }
    }

    private void BuildRow()
    {
        if (builder == null)
            return;

        //Initial spot
        float xPosition = transform.position.x;

        //Build first target
        targets.Add(builder.BuildTarget(new Vector3(xPosition, transform.position.y, 0), targetType, direction));
        amountPerRow--;

        float xPosition2 = transform.position.x;

        while(amountPerRow > 0)
        {
            //Go to next position
            xPosition += spaceBetween;

            //Build target at row
            targets.Add(builder.BuildTarget(new Vector3(xPosition, transform.position.y, 0), targetType, direction));
            amountPerRow--;

            //Check if can still build target
            if (amountPerRow <= 0)
                break;

            //Go to next position
            xPosition2 -= spaceBetween;

            //Build target at row
            targets.Add(builder.BuildTarget(new Vector3(xPosition2, transform.position.y, 0), targetType, direction));
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

    private void RebuildRow()
    {
        if (targetPositions.Count == 0)
        {
            BuildRow();
            return;
        }

        print("Re");

        foreach(Vector3 target in targetPositions)
        {
            targets.Add(builder.BuildTarget(target, targetType, direction));
        }

        GameObject endR = Instantiate(endPosition, endRPos, transform.rotation);
        GameObject endL = Instantiate(endPosition, endLPos, transform.rotation);

        endL.GetComponent<EndPosition>().ConnectEndPositions(endR, direction, Direction.Right);
    }

    public List<Vector3> GetTargetTranforms()
    {
        List<Vector3> newTransforms = new List<Vector3>();

        foreach (GameObject target in targets)
        {
            if (target == null)
                continue;
            newTransforms.Add(target.transform.position);
        }

        return newTransforms;
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