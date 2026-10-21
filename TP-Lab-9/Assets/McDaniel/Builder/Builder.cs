using UnityEngine;

public class Builder : MonoBehaviour
{
    [SerializeField] GameObject targetController;

    public void BuildTargets()
    {
        TargetBuilder builder = new BigTarget();
        builder.BuildTarget();
        GameObject controller = Instantiate(targetController);

        targetController.GetComponent<TargetController>().AddTarget(builder.Target);
    }
}
