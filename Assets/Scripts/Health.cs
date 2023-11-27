using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 5;
    public int Value { get; private set; }

    private void Start()
    {
        Value = _maxHealth;
    }

    public void ChangeHealth(int delta)
    {
        Value = Mathf.Clamp(Value + delta, 0, _maxHealth);
    }
}
