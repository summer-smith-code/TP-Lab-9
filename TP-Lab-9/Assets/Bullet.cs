using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;

    // Moves bullet up constantly
    void Update()
    {
        transform.position += transform.up * speed * Time.deltaTime;
    }

    // When hitting enemy, sets game object as inactive for later re-pooling
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            gameObject.SetActive(false);
        }
    }
}
