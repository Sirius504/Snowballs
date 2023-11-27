using UnityEngine;

public class SnowballGrabber : MonoBehaviour
{
    [SerializeField] private Grid _grid;
    [SerializeField] private Snow _snow;
    [SerializeField] private float _pickUpTime = 0.5f;
    [SerializeField] private Movement _movement;

    private float _pickUpTimeEnd;
    private SnowTile _tile;

    public bool IsPickingUp { get; private set; } = false;
    public bool HasSnowball { get; private set; }
    public float PickUpProgress { get; private set; }

    private void Update()
    {
        if (HasSnowball)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {            
            if (!CanPickup(out _tile))
            {
                return;
            }

            _pickUpTimeEnd = Time.time + _pickUpTime;
            IsPickingUp = true;
        }

        if (Input.GetKey(KeyCode.Space) && IsPickingUp)
        {
            PickUpProgress = 1 - ((_pickUpTimeEnd - Time.time) / _pickUpTime);
            Debug.Log(PickUpProgress);
            if (PickUpProgress >= 1f)
            {
                _tile.Level--;
                HasSnowball = true;
                IsPickingUp = false;
                _tile = null;
                PickUpProgress = 0f;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space) && IsPickingUp)
        {
            IsPickingUp = false;
            _tile = null;
            PickUpProgress = 0f;
        }

        _movement.CanMove = !IsPickingUp;
    }

    public void Clear()
    {
        HasSnowball = false;
    }

    private bool CanPickup(out SnowTile tile)
    {
        var position = _grid.WorldToCell(transform.position);
        tile = _snow.GetSnowTile(position);
        return tile.Level > 0;
    }
}
