using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textMesh;
    [SerializeField] private Health _target;

    private Dictionary<int, string> _healthToTextMap = new()
    {
        {0, "Freezing!!!" },
        {1, "Very Cold" },
        {2, "Cold" },
        {3, "Chilly" },
        {4, "OK" },
        {5, "Warm" }
    };

    private void Update()
    {
        _textMesh.text = $"Temperature: {_healthToTextMap[_target.Value]}";
    }
}
