using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class UIManager : MonoBehaviour
{
    [HideInInspector] public static TCP sender;
    public static GlobalParameters globalParamenters;
    public static GlobalParameters savedGlobalParamenters;

    [SerializeField] private float timer = 0;
    [SerializeField] private float delay;

    [SerializeField] private Button powerButton;
    [SerializeField] private ConnectButton connectButton;
    [SerializeField] private BrightnessSlider brightnessSlider;
    [SerializeField] private ColorCursor colorCursor;
    [SerializeField] private AngularController angularController;

    public GameObject blocker;

    private int successStatus = 200;

    private string previousGlobalPropertiesHash;

    private void Start()
    {
        sender = new TCP("192.168.0.120", 80);
        sender.blocker = blocker;
        sender.TryConnect();

        TCP.AnswerWasGetted += ActionReturnedCallback;

        AskEveryone();
        SubmitUI();
    }

    private void Update()
    {
        if (TCP.connected)
        {
            if (blocker.activeInHierarchy)
                blocker.SetActive(false);

            timer += Time.deltaTime;

            if (timer >= delay)
            {
                timer = 0;

                if (TCP.connected)
                {
                    AskEveryone();

                    string hash = globalParamenters.CreateHash();
                    if (!globalParamenters.HashesAreEquals(hash, previousGlobalPropertiesHash))
                    {
                        previousGlobalPropertiesHash = hash;

                        sender.SendGlobalProperties(globalParamenters);
                    }
                }
            }
        }
    }

    private void AskEveryone()
    {
        savedGlobalParamenters = GlobalParameters.Clone(globalParamenters);
        globalParamenters = new GlobalParameters();

        globalParamenters.power = powerButton.state;
        globalParamenters.connected = TCP.connected;
        globalParamenters.brightness = brightnessSlider.scrollbar.value;
        globalParamenters.color = colorCursor.color.ToHexString();
        globalParamenters.angle = angularController.value;
    }

    private void SubmitUI()
    {
        if (!globalParamenters.connected)
        {
            powerButton.Enable(false);
            brightnessSlider.Enable(false);
            colorCursor.Enable(false);
            angularController.Enable(false);

            connectButton.ChangeValue(Button.States.Off);
        }
        else
        {
            powerButton.Enable(true);
            powerButton.ChangeValue(globalParamenters.power ? Button.States.On : Button.States.Off);

            brightnessSlider.Enable(true);
            brightnessSlider.scrollbar.value = globalParamenters.brightness;

            colorCursor.Enable(true);
            if (UnityEngine.ColorUtility.TryParseHtmlString("#" + globalParamenters.color, out UnityEngine.Color newColor))
            {
                colorCursor.color = newColor;
            }

            angularController.Enable(true);

            connectButton.ChangeValue(Button.States.On);
        }
    }

    public void ActionReturnedCallback(string message)
    {
        if (int.Parse(message) != successStatus)
        {
            globalParamenters = GlobalParameters.Clone(savedGlobalParamenters);
            Debug.Log(int.Parse(message));
        }

        SubmitUI();
    }
}

[Serializable]
public class GlobalParameters
{
    public bool power;
    public bool connected;
    public float brightness;
    public string color;
    public int angle;

    public GlobalParameters()
    {
        power = false;
        connected = false;
        brightness = 1f;
        color = "#FFFFFFFF";
        angle = 0;
    }

    public static GlobalParameters Clone(GlobalParameters temp)
    {
        if (temp != null)
            return temp;
        else
            return new GlobalParameters();
    }

    public string CreateHash() => Hash128.Compute(JsonUtility.ToJson(this)).ToString();

    public bool HashesAreEquals(string hash, string previousHash) => hash.Equals(previousHash);
}
