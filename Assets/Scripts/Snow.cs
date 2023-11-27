using UnityEngine;

public class Snow : MonoBehaviour
{
    [SerializeField] private Vector2Int _levelSize;
    [SerializeField] private Grid _grid;
    [SerializeField] private SnowTile _snowTilePrefab;

    private SnowTile[,] _level;

    private void Start()
    {
        _level = new SnowTile[_levelSize.x, _levelSize.y];
        for (var x = 0; x < _levelSize.x; x++)
            for(var y = 0; y < _levelSize.y; y++)
            {
                var position = _grid.CellToWorld(new Vector3Int(x, y, 1));
                _level[x, y] = Instantiate(_snowTilePrefab, position, Quaternion.identity, transform);
            }
    }

    public SnowTile GetSnowTile(Vector3Int at)
    {
        return _level[at.x, at.y];
    }
}