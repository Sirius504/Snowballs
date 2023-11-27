using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Health _health;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            _health.Change(-1);
        }
    }
}
