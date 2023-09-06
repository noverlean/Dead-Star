using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Elevator : MonoBehaviour
{
    private float minHeight = 170f;
    private float maxHeight = 20f;

    public int value = 20;
    public bool left;

    public Scrollbar scrollbar;
    [SerializeField] private Text inspector;

    [SerializeField] private Image sliderImage;

    public void OnChange()
    {
        value = Mathf.RoundToInt(Mathf.Lerp(minHeight, maxHeight, scrollbar.value)); 
        inspector.text = value.ToString() + " cm";
    }

    public void Enable(bool state)
    {
        scrollbar.enabled = state;

        sliderImage.color = Colors.instance.colors[(int)(state ? Color.Off : Color.Disabled)];
    }

    public void SetValue(int value)
    {
        this.value = value;
        inspector.text = value.ToString() + " cm";
        scrollbar.value = Mathf.Lerp(1f, 0f, (value - 20f) / 150f);
    }
}
