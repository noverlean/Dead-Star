using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AngularController : MonoBehaviour
{
    private int minAngle = -90;
    private int maxAngle = 90;

    public int value = 0;

    public Scrollbar scrollbar;
    [SerializeField] private Text inspector;

    [SerializeField] private Image sliderImage;
    [SerializeField] private RectTransform lamp;

    public void OnChange()
    {
        value = Mathf.RoundToInt(Mathf.Lerp(maxAngle, minAngle, scrollbar.value));
        inspector.text = value.ToString() + "°";
        lamp.rotation = Quaternion.Euler(0, 0, value);
    }

    public void Enable(bool state)
    {
        scrollbar.enabled = state;

        sliderImage.color = Colors.instance.colors[(int)(state ? Color.Off : Color.Disabled)];
    }
}
