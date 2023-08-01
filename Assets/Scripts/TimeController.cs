using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private TMPro.TextMeshProUGUI _sliderText;

    void Start() {
        _slider.onValueChanged.AddListener((v) => {
            _sliderText.text = v.ToString("0.0");
        });
    }
}
