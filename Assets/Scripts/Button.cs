using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button : MonoBehaviour
{
    public enum States
    { 
        Off = 0,
        On = 1,
        InProcess = 2,
    }

    public bool state;
    private States visualState;
    public new bool enabled;

    public Text text;
    public List<string> texts;
    public Image buttonImage;
    public Color unenabledColor;

    public void ButtonPressed() => ButtonPressed(!state);
    public void ButtonPressed(bool state)
    {
        if (!enabled)
            return;

        this.state = state;
        DoAction();
    }

    public void ChangeValue(States value)
    {
        text.text = texts[(int)value];
        buttonImage.color = Colors.instance.colors[(int)value];
        visualState = value;

        enabled = value != States.InProcess;
    }

    public void Enable(bool state)
    {
        enabled = state;
        if (enabled)
        {
            ChangeValue(visualState);
        }
        else
        {
            buttonImage.color = Colors.instance.colors[(int)Color.Disabled];
            text.text = "UNAVAILABLE";
        }
    }

    public virtual void DoAction()
    {
        ChangeValue(States.InProcess);
    }
}
