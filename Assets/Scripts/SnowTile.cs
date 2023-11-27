using System.Linq;
using UnityEngine;

public class SnowTile : MonoBehaviour
{
    // How sprites are mapped onto their indices:

    //
    //       2  
    //     1   3
    //       0  
    //

    [SerializeField] private SpriteRenderer _highSnow;
    [SerializeField] private SpriteRenderer _lowSnow;

    [SerializeField] private Sprite[] _highSnowSprites;
    [SerializeField] private Sprite[] _lowSnowSprites;

    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private int _level;
    private SnowTile[] _neighbours;

    public int Level
    {
        get => _level;
        set
        {
            _level = value;
            UpdateSprite();
            foreach (var neighbour in _neighbours)
            {
                neighbour?.UpdateSprite();
            }
        }
    }

    public void InitLevel(int level)
    {
        _level = level;
    }

    public void InjectNeighbours(SnowTile[] neighbours)
    {
        _neighbours = neighbours;
        UpdateSprite();
    }


    public void UpdateSprite()
    {
        UpdateHighSnow(_level == 2);
        UpdateLowSnow(_level == 0);
    }


    private void UpdateHighSnow(bool enable)
    {
        _highSnow.gameObject.SetActive(enable);
        if (!enable)
        {
            return;
        }
        var index = CalculateSpriteIndexForLevel(2);
        _highSnow.sprite = _highSnowSprites[index];
    }

    private void UpdateLowSnow(bool enable)
    {
        _lowSnow.gameObject.SetActive(enable);
        if (!enable)
        {
            return;
        }
        _lowSnow.sprite = _lowSnowSprites[CalculateSpriteIndexForLevel(0)];
    }
    private int CalculateSpriteIndexForLevel(int targetLevel)
    {
        int spriteIndex = 0;
        spriteIndex |= (IsNeighbourOfLevel(0, targetLevel) ? 1 : 0) << 0;
        spriteIndex |= (IsNeighbourOfLevel(1, targetLevel) ? 1 : 0) << 1;
        spriteIndex |= (IsNeighbourOfLevel(2, targetLevel) ? 1 : 0) << 2;
        spriteIndex |= (IsNeighbourOfLevel(3, targetLevel) ? 1 : 0) << 3;
        return spriteIndex;
    }

    private bool IsNeighbourOfLevel(int index, int level)
    {
        return _neighbours[index] != null
            && _neighbours[index].Level == level;
    }
}