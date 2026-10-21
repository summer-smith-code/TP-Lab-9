using UnityEngine;

public class PlayerShip : MonoBehaviour
{
    public float moveSpeed;
    private Vector3 _screenBounds;

    private void Start()
    {
        Camera mainCamera = Camera.main;
        if (mainCamera != null )
        {
            _screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A)) transform.position += transform.right * -moveSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.D)) transform.position += transform.right * moveSpeed * Time.deltaTime;
    }
}
