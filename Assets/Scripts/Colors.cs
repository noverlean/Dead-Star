using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colors : MonoBehaviour
{
    public static Colors instance;

    public List<UnityEngine.Color> colors;

    private void Awake()
    {
        instance = this;
    }
}

public enum Color
{
    Off = 0,
    On = 1,
    InProcess = 2,
    Disabled = 3,
}