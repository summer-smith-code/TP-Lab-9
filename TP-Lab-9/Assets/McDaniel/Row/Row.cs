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

    private GameObject endL;
    private GameObject endR;

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
        //If new game build new row
        if (newGame)
        {
            print("newGame");
            BuildRow();
        }
        //Else rebuild row based on loaded data
        else
        {
            RebuildRow();
        }
    }

    private void BuildRow()
    {
        if (builder == null)
            return;

        int remainingTargets = amountPerRow;

        //Initial spot
        float xPosition = transform.position.x;
        float xPosition2 = transform.position.x; ;

        //Build first target
        targets.Add(builder.BuildTarget(new Vector3(xPosition, transform.position.y, 0), targetType, direction));
        amountPerRow--;

        remainingTargets--;

        while (remainingTargets > 0)
        {
            //Go to next position
            xPosition += spaceBetween;

            //Build target at row
            targets.Add(builder.BuildTarget(new Vector3(xPosition, transform.position.y, 0), targetType, direction));
            amountPerRow--;

            remainingTargets--;

            //Check if can still build target
            if (remainingTargets <= 0)
                break;

            //Go to next position
            xPosition2 -= spaceBetween;

            //Build target at row
            targets.Add(builder.BuildTarget(new Vector3(xPosition2, transform.position.y, 0), targetType, direction));
            amountPerRow--;

            remainingTargets--;
        }

        //Go to next positions
        xPosition += spaceBetween;


        endRPos = new Vector3(xPosition, transform.position.y, 0);
        endLPos = new Vector3(xPosition2, transform.position.y, 0);

        //Spawn in the endPosition 
        endR = Instantiate(endPosition, endRPos, transform.rotation);
        endL = Instantiate(endPosition, endLPos, transform.rotation);

        endR.GetComponent<EndPosition>().ConnectEndPositions(endL, direction, Direction.Right);

        newGame = false;
    }

    //Rebuilds the row
    private void RebuildRow()
    {
        //Place targets 
        foreach(Vector3 target in targetPositions)
        {
            targets.Add(builder.BuildTarget(target, targetType, direction));
        }

        //Place ends
        endR = Instantiate(endPosition, endRPos, transform.rotation);
        endL = Instantiate(endPosition, endLPos, transform.rotation);

        //Connect end positions
        endR.GetComponent<EndPosition>().ConnectEndPositions(endL, direction, Direction.Right);
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

    // For saving, destroys enemies in scene
    public void ClearRow()
    {
        foreach (GameObject target in targets)
        {
            if (target != null)
            {
                Destroy(target);
            }
        }

        targets.Clear();

        if (endL != null)
        {
            Destroy(endL);
        }

        if (endR != null)
        {
            Destroy(endR);
        }

        endL = null;
        endR = null;
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