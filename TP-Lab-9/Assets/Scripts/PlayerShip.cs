using LitJson;
using UnityEngine;

public class PlayerShip : MonoBehaviour, ISaveable
{
    public float moveSpeed;
    private Vector3 _screenBounds;
    private float halfPlayerSizeX;

    // For saving player position
    [SerializeField] private string saveID = "PlayerShip";
    public string SaveID => saveID;

    // Save player position to JSON data
    public JsonData SavedData
    {
        get
        {
            JsonData data = new JsonData();

            Debug.Log("Saving Player Position: " + transform.position);

            data["positionX"] = (double)transform.position.x;
            Debug.Log(data["positionX"]);
            data["positionY"] = (double)transform.position.y;
            Debug.Log(data["positionY"]);
            data["positionZ"] = (double)transform.position.z;
            Debug.Log(data["positionZ"]);

            Debug.Log("Saving Player Position: " + transform.position);

            return data;
        }
    }

    // Load that data
    public void LoadFromData(JsonData data)
    {
        Debug.Log("Loading Player Position: " + transform.position);

        float x = (float)(double)data["positionX"];
        Debug.Log(x);
        float y = (float)(double)data["positionY"];
        Debug.Log(y);
        float z = (float)(double)data["positionZ"];
        Debug.Log(z);

        transform.position = new Vector3(x, y, z);
        Debug.Log(transform.position);
    }

    private void Start()
    {
        //Get the half width of the player sprite to use for clamping movement
        halfPlayerSizeX = GetComponent<SpriteRenderer>().bounds.size.x / 2;

        //Get the main camera and calculate the screen bounds in world coordinates
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
