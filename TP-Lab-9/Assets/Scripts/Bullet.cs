using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;

    // Moves bullet up constantly and sets it as inactive when off-screen
    void Update()
    {
        transform.position += transform.up * speed * Time.deltaTime;
        if (CheckIfOffScreen()) gameObject.SetActive(false);
    }

    // When hitting enemy, sets game object as inactive for later re-pooling
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("COLLISSION! " + collision.gameObject.name);
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<TargetHit>().Hit();
            gameObject.SetActive(false);
        }
    }

    // Checks if bullet is off-screen
    private bool CheckIfOffScreen()
    {
        // Get the main camera and find current position in viewport
        Vector3 pos = new();
        Camera mainCamera = Camera.main;
        if (mainCamera != null) pos = mainCamera.WorldToViewportPoint(transform.position);

        // return true if off-screen
        if (pos.y < 0f || pos.y > 1f) return true;

        // not off-screen
        return false;
    }
}
