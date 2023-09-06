using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrightnessSlider : MonoBehaviour
{
    public Scrollbar scrollbar;

    [SerializeField] private Image sliderImage;

    public void Enable(bool state)
    {
        scrollbar.enabled = state;

        sliderImage.color = Colors.instance.colors[(int)(state ? Color.Off : Color.Disabled)];
    }
}
