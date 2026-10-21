using UnityEngine;

public class PlayerShip : MonoBehaviour
{
    public float moveSpeed;
    private Vector3 _screenBounds;
    private float halfPlayerSizeX;

    private void Start()
    {
        halfPlayerSizeX = GetComponent<SpriteRenderer>().bounds.size.x / 2;
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
        if (Input.GetMouseButtonDown(0)) Shoot();
        clampPlayerMovement();
    }

    private void Shoot()
    {
        Debug.Log("Shoot!");    
    }

    private void clampPlayerMovement()
    {
        Vector3 pos = transform.position;
            pos.x = Mathf.Clamp(pos.x, -_screenBounds.x + halfPlayerSizeX, _screenBounds.x - halfPlayerSizeX);
            transform.position = pos;
    }
}
