using UnityEngine;

public class SnowballGrabber : MonoBehaviour
{
    [SerializeField] private Grid _grid;
    [SerializeField] private Snow _snow;

    public bool HasSnowball { get; private set; }

    private void Update()
    {
        if (HasSnowball)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            var position = _grid.WorldToCell(transform.position);
            var tile = _snow.GetSnowTile(position);
            if (tile.Level == 0)
            {
                return;
            }
            tile.Level--;
            HasSnowball = true;
        }
    }

    public void Clear()
    {
        HasSnowball = false;
    }
}
