using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class TargetMovement
{
    GameObject targetObject;
    int speed;
    Direction direction;


    //Set up the component
    public TargetMovement(GameObject targetObject, int speed, Direction direction)
    {
        this.targetObject = targetObject;
        this.speed = speed;
        this.direction = direction;
    }

    public void Move()
    {
        targetObject.transform.position += Vector3.right * (int)direction * speed * Time.deltaTime;
    }
}
