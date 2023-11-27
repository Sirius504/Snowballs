using UnityEngine;

public class Movement : MonoBehaviour
{
    enum Direction
    {
        Down = 0,
        Left = 1,
        Up = 2,
        Right = 3,
    }

    private static readonly int DIRECTION_KEY = Animator.StringToHash("direction");
    private static readonly int SPEED_KEY = Animator.StringToHash("speed");


    [SerializeField] private float maxSpeed = 1f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator _animator;

    private Vector2 input;
    private Direction _direction;

    private void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            input.y += 1;
            _direction = Direction.Up;
        }
        if (Input.GetKey(KeyCode.S))
        {
            input.y += -1;
            _direction = Direction.Down;
        }
        if (Input.GetKey(KeyCode.A))
        {
            input.x += -1;
            _direction = Direction.Left;
        }
        if (Input.GetKey(KeyCode.D))
        {
            input.x += 1;
            _direction = Direction.Right;
        }
        
    }

    private void FixedUpdate()
    {
        rb.velocity = maxSpeed * (Vector3)input.normalized;
        input = Vector2.zero;
        _animator.SetInteger(DIRECTION_KEY, (int)_direction);
        _animator.SetFloat(SPEED_KEY, rb.velocity.magnitude / maxSpeed);
    }
}
