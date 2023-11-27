using UnityEngine;

public class SnowballThrower : MonoBehaviour
{
    [SerializeField] private SnowballGrabber _grabber;
    [SerializeField] private Collider2D _collider;
    [SerializeField] private float _throwSpeed = 2f;
    [SerializeField] private Rigidbody2D _snowballPrefab;
    [SerializeField] private float _spawnRadius = 1.5f;
    [SerializeField] private Transform _throwOrigin;
    private Vector3? throwInput;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            throwInput = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }

    private void FixedUpdate()
    {
        if (throwInput != null && _grabber.HasSnowball)
        {
            var toTarget = throwInput.Value - _throwOrigin.position;
            toTarget.z = 0;
            toTarget.Normalize();
            var spawnPoint = _throwOrigin.position + toTarget * _spawnRadius;
            var snowball = Instantiate(_snowballPrefab, spawnPoint, Quaternion.identity, null);
            Physics2D.IgnoreCollision(snowball.GetComponent<Collider2D>(), _collider);
            var velocity = toTarget * _throwSpeed;
            snowball.velocity = velocity;
            _grabber.Clear();
        }
        throwInput = null;
    }
}
