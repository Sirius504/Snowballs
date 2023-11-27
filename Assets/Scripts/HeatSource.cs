using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HeatSource : MonoBehaviour
{
    [SerializeField] private float _tickTime = 3f;
    [SerializeField] private int _healPerTick = 1;

    private Dictionary<Health, float> _timers = new();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (TryGetHealth(collision, out var health)
            && !_timers.ContainsKey(health))
        {
            _timers.Add(health, 0f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (TryGetHealth(collision, out var health)
            && _timers.ContainsKey(health))
        {
            _timers.Remove(health);
        }
    }

    private void FixedUpdate()
    {
        foreach(var health in _timers.Keys.ToList())
        {
            if (health.Value == health.Max)
            {
                _timers[health] = 0f;
                continue;
            }
            var timer = _timers[health];
            timer += Time.fixedDeltaTime;
            if (timer >= _tickTime)
            {
                timer -= _tickTime;
                health.Change(_healPerTick);
            }
            _timers[health] = timer;
        }
    }

    private bool TryGetHealth(Collider2D collision, out Health health)
    {
        if (collision.TryGetComponent(out health))
        {
            return true;
        }
        health = collision.GetComponentInParent<Health>();
        if (health != null)
        {
            return true;
        }
        return false;
    }
}
