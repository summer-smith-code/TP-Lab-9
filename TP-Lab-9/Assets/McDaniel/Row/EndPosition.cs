using UnityEngine;

public class EndPosition : MonoBehaviour
{
    EndPosition otherEndPosition;

    public void ConnectEndPositions(GameObject endPosition, Direction direction, Direction place)
    {
        if (otherEndPosition != null)
            return;

        EndPosition end = endPosition.GetComponent<EndPosition>();

        if (end == null)
        {
            return;
        }

        //Enable or disable collider
        if(direction == place)
        {
            GetComponent<BoxCollider2D>().enabled = true;
        }
        else
        {
            Destroy(GetComponent<BoxCollider2D>());
        }

        //Connect end position
        otherEndPosition = end;
        otherEndPosition.ConnectEndPositions(this.gameObject, direction, Direction.Left);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        //Transport target
        if(collision.CompareTag("Enemy"))
        {
            collision.transform.position = otherEndPosition.transform.position;
        }
    }
}
