using UnityEngine;

public class Builder : MonoBehaviour
{
    [SerializeField] GameObject targetController;

    public void BuildTarget(Vector3 position, TargetType targetType)
    {
        //switch (targetType)
        //{ }
        TargetBuilder builder = new BigTarget();
        builder.BuildTarget();
        GameObject controller = Instantiate(targetController, position, transform.rotation);

        targetController.GetComponent<TargetController>().AddTarget(builder.Target);
    }
}
