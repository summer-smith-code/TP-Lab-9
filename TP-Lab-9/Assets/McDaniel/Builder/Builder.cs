using UnityEngine;

public class Builder : MonoBehaviour
{
    [SerializeField] GameObject targetController;

    public GameObject BuildTarget(Vector3 position, TargetType targetType, Direction direction)
    {
        TargetBuilder builder = null;

        switch (targetType)
        {
            case TargetType.Big:
                builder = new BigTarget();
                break;
            case TargetType.Normal:
                builder = new NormalTarget();
                break;
            case TargetType.Small:
                builder = new SmallTarget();
                break;
        }

        builder.BuildTarget();
        GameObject controller = Instantiate(targetController, position, transform.rotation);

        controller.GetComponent<TargetController>().AddTarget(builder.Target, direction);

        return controller;
    }
}
