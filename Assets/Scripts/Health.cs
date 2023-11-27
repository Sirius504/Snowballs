using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 5;
    public int Value { get; private set; }
    public int Max => _maxHealth;

    private void Start()
    {
        Value = _maxHealth;
    }

    public void Change(int delta)
    {
        Value = Mathf.Clamp(Value + delta, 0, _maxHealth);
    }
}
