using UnityEngine;
using UnityEngine.UI;

public class ThrowProgressBar : MonoBehaviour
{
    [SerializeField] private SnowballThrower _thrower;
    [SerializeField] private Image _fill;
    [SerializeField] private GameObject _parent;

    private void Update()
    {
        _parent.SetActive(_thrower.IsThrowing);
        _fill.fillAmount = _thrower.Power;
    }
}
