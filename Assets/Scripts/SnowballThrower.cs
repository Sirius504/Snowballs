using UnityEngine;

public class SnowballThrower : MonoBehaviour
{
    [SerializeField] private SnowballGrabber _grabber;
    [SerializeField] private Collider2D _collider;
    [SerializeField] private float _throwSpeed = 2f;
    [SerializeField] private float _minThrowSpeed = 4f;
    [SerializeField] private Rigidbody2D _snowballPrefab;
    [SerializeField] private float _spawnRadius = 1.5f;
    [SerializeField] private Transform _throwOrigin;
    [SerializeField] private float _fullLoadTime = 1f;

    private Vector3? throwTarget;
    private float _throwStartTime;
    public float Power { get; private set; } = 0f;
    public bool IsThrowing { get; private set; }

    private void Update()
    {
        if (!_grabber.HasSnowball)
        {
            return;
        }

        throwTarget = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            _throwStartTime = Time.time;
            IsThrowing = true;
        }

        if (Input.GetMouseButton(0))
        {
            Power = (Time.time - _throwStartTime) / _fullLoadTime;
            Power = Mathf.Clamp01(Power);
            Debug.Log(Power);
        }

        if (Input.GetMouseButtonUp(0))
        {
            Throw();
            IsThrowing = false;
        }
    }

    private void Throw()
    {
        var toTarget = throwTarget.Value - _throwOrigin.position;
        toTarget.z = 0;
        toTarget.Normalize();
        var spawnPoint = _throwOrigin.position + toTarget * _spawnRadius;
        var snowball = Instantiate(_snowballPrefab, spawnPoint, Quaternion.identity, null);
        Physics2D.IgnoreCollision(snowball.GetComponent<Collider2D>(), _collider);
        var velocity = toTarget * Mathf.Lerp(_minThrowSpeed, _throwSpeed, Power);
        snowball.velocity = velocity;
        _grabber.Clear();
        throwTarget = null;
        Power = 0f;
    }
}
