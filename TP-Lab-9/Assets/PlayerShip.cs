using UnityEngine;
using static UnityEditor.FilePathAttribute;

public class PlayerShip : MonoBehaviour
{
    public float moveSpeed;
    private Vector3 _screenBounds;
    private float halfPlayerSizeX;

    private void Start()
    {
        // Get the half width of the player sprite to use for clamping movement
        halfPlayerSizeX = GetComponent<SpriteRenderer>().bounds.size.x / 2;

        // Get the main camera and calculate the screen bounds in world coordinates
        Camera mainCamera = Camera.main;
        if (mainCamera != null )
        {
            _screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));
        }
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.A)) transform.position += transform.right * -moveSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.D)) transform.position += transform.right * moveSpeed * Time.deltaTime;
        if (Input.GetMouseButtonDown(0)) Shoot();
        clampPlayerMovement();
    }

    // Shoot a bullet from the object pool
    private void Shoot()
    {
        Debug.Log("Shoot!");
        GameObject bullet = ObjectPool.SharedInstance.GetPooledObject();
        if (bullet != null)
        {
            bullet.transform.position = transform.position;
            bullet.transform.rotation = transform.rotation;
            bullet.SetActive(true);
        }
    }

    //Clamp player movement inside of screen bounds
    private void clampPlayerMovement()
    {
        Vector3 pos = transform.position;
            pos.x = Mathf.Clamp(pos.x, -_screenBounds.x + halfPlayerSizeX, _screenBounds.x - halfPlayerSizeX);
            transform.position = pos;
    }
}
