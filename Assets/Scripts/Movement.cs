using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    [SerializeField] private Rigidbody2D rb;

    private Vector2 input;

    private void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            input.y += 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            input.y += -1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            input.x += -1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            input.x += 1;
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = speed * (Vector3)input.normalized;
        input = Vector2.zero;
    }
}
