using UnityEngine;
using UnityEngine.UI;

public class PickUpProgressBar : MonoBehaviour
{
    [SerializeField] private GameObject _parent;
    [SerializeField] private Image _fillImage;
    [SerializeField] private SnowballGrabber _grabber;

    private void Update()
    {
        _parent.gameObject.SetActive(_grabber.IsPickingUp);
        _fillImage.fillAmount = _grabber.PickUpProgress;
    }
}
