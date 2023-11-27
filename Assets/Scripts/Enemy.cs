using System;
using System.Linq;
using UnityEngine;

[SelectionBase]
public class Enemy : MonoBehaviour
{
    enum State
    {
        Aiming,
        Pushing
    }

    [SerializeField] private GameObject _player;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private Health _health;
    [Space(5)]
    [Header("Rotation")]
    [SerializeField] private float _rotationTime = 1f;
    [SerializeField] private float _maxRotationSpeed = 15f;
    [SerializeField] private float _rotationEpsilon = .1f;
    [Space(5)]
    [Header("Movement")]
    [SerializeField] private float _pushDistance = 5f;
    [SerializeField] private float _obstacleOffset = 2f;
    [SerializeField] private float _maxSpeed = 4f;
    [SerializeField] private float _pushTime = 2f;
    [SerializeField] private float _movementEpsilon = .1f;

    private State _state = State.Aiming;
    private float currentRotation;
    private Vector2 _pushTarget;
    private Vector2 _currentVelocity;

    private void Start()
    {
        _pushTarget = CalculatePushTarget();
    }

    private void FixedUpdate()
    {
        _rigidbody.angularVelocity = 0f;
        switch (_state)
        {
            case State.Aiming:
                HandleAimingMovement();
                break;
            case State.Pushing:
                HandlePushingMovement();
                break;
            default:
                throw new NotImplementedException();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Destroy(gameObject);
        }

        if (collision.collider.CompareTag("Projectile"))
        {
            _health.Change(-1);
            if (_health.Value == 0)
            {
                Destroy(gameObject);
            }
        }
    }

    private void HandlePushingMovement()
    {
        var newPosition = Vector2.SmoothDamp(_rigidbody.position,
            _pushTarget,
            ref _currentVelocity,
            _pushTime,
            _maxSpeed,
            Time.fixedDeltaTime);
        _rigidbody.MovePosition(newPosition);
        if (Mathf.Abs((newPosition - _pushTarget).magnitude) <= _movementEpsilon)
        {
            _state = State.Aiming;
            _currentVelocity = Vector2.zero;
        }
    }

    private void HandleAimingMovement()
    {        
        var toPlayer = _player.transform.position - transform.position;
        var targetRotation = Vector2.SignedAngle(Vector2.up, toPlayer);
        var newRotation = Mathf.SmoothDampAngle(_rigidbody.rotation,
            targetRotation,
            ref currentRotation,
            _rotationTime,
            _maxRotationSpeed,
            Time.fixedDeltaTime);
        newRotation %= 360;

        _rigidbody.MoveRotation(newRotation);
        _rigidbody.velocity = Vector2.zero;
        if (Mathf.Abs(Mathf.DeltaAngle(newRotation, targetRotation)) <= _rotationEpsilon)
        {
            _state = State.Pushing;
            _pushTarget = CalculatePushTarget();
        }
    }

    private Vector3 CalculatePushTarget()
    {
        var target = _rigidbody.position + (Vector2)transform.up * _pushDistance;
        var hits = Physics2D.RaycastAll(transform.position, transform.up, _pushDistance + _obstacleOffset);
        if (hits.Length > 0 && hits.Any(hit => hit.collider.CompareTag("Wall")))
        {
            var wallHit = hits.First(hit => hit.collider.CompareTag("Wall"));
            var toHit = wallHit.point - _rigidbody.position;
            toHit = Vector2.ClampMagnitude(toHit, toHit.magnitude - _obstacleOffset);
            target = _rigidbody.position + toHit;
        }
        return target;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(_pushTarget, Vector3.one * 0.2f);
    }
}