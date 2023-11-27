using UnityEngine;

public class SnowTile : MonoBehaviour
{
    [SerializeField] private Gradient _gradient;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private AnimationCurve _spread;
    [SerializeField] private int _maxLevel = 5;
    [SerializeField] private int _level;

    public int Level
    {
        get => _level;
        set
        {
            _level = value;
            UpdateColor();
        }
    }

    private void UpdateColor()
    {
        spriteRenderer.color = _gradient.Evaluate(_level / (float)_maxLevel);
    }

    private void Start()
    {
        Level = Mathf.RoundToInt(_spread.Evaluate(Random.value) * _maxLevel);
    }

}