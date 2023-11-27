using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private Vector2Int _levelSize;
    [SerializeField] private AnimationCurve _spread;
    [SerializeField] private int _maxLevel = 2;
    [SerializeField] private Grid _grid;
    [SerializeField] private SnowTile _snowTilePrefab;
    [SerializeField] private Player _player;

    private SnowTile[,] _level;
    private Bounds _levelBounds;

    private void Awake()
    {
        _level = new SnowTile[_levelSize.x, _levelSize.y];
        for (var col = 0; col < _levelSize.x; col++)
            for(var row = 0; row < _levelSize.y; row++)
            {
                var position = _grid.CellToWorld(new Vector3Int(col, row, 1));
                var newTile = Instantiate(_snowTilePrefab, position, Quaternion.identity, transform);
                newTile.InitLevel(Mathf.RoundToInt(_spread.Evaluate(UnityEngine.Random.value) * _maxLevel));
                _level[col, row] = newTile;
            }

        for (var col = 0; col < _levelSize.x; col++)
            for (var row = 0; row < _levelSize.y; row++)
            {
                InitTile(col, row);
            }

        _levelBounds = new Bounds(_grid.CellToWorld(Vector3Int.zero), _grid.cellSize);
        _levelBounds.Encapsulate(_grid.CellToWorld(new Vector3Int(_levelSize.x, _levelSize.y, 0)) + _grid.cellSize * 0.5f);
    }

    private void Update()
    {
        var tilePos = _grid.WorldToCell(_player.transform.position);
        tilePos.x = Mathf.Clamp(tilePos.x, 0, _level.GetLength(0)-1);
        tilePos.y = Mathf.Clamp(tilePos.y, 0, _level.GetLength(1)-1);
        var tile = _level[tilePos.x, tilePos.y];
    }

    private void InitTile(int col, int row)
    {
        var neighbours = new SnowTile[4];
        neighbours[0] = row == 0 ? null : _level[col, row - 1]; 
        neighbours[1] = col == 0 ? null : _level[col - 1, row]; 
        neighbours[2] = row == _level.GetLength(1) - 1 ? null : _level[col, row + 1]; 
        neighbours[3] = col == _level.GetLength(0) - 1 ? null : _level[col + 1, row];
        _level[col, row].InjectNeighbours(neighbours);
    }

    public SnowTile GetSnowTile(Vector3Int at)
    {
        return _level[at.x, at.y];
    }

    public Bounds GetLevelBounds()
    {
        return _levelBounds;
    }
}