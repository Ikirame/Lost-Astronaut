using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderToText : MonoBehaviour
{
    public Text textValue;

    private Slider slider;

    public bool IsOptionSlider;

    // Use this for initialization
    private void Start()
    {
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    private void Update()
    {
        string newText;

        if (IsOptionSlider)
            newText = (int) (slider.value * 100) + "%";
        else
            newText = slider.value + "%";

        textValue.text = newText;
    }
}