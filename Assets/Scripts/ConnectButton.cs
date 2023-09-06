using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConnectButton : Button
{
    //[Header("Additions")]
    //[SerializeField] private Panel panel;
    //[SerializeField] private InputField IP;
    //[SerializeField] private GameObject blocker;

    //public override void DoAction()
    //{
    //    panel?.ShowPanel(!panel.state);
    //}

    //public void TryConnect()
    //{
    //    blocker.SetActive(true);
    //    StartCoroutine(Connect());
    //    //TCP.connected = !TCP.connected;
    //}

    //private IEnumerator Connect()
    //{
    //    UIManager.sender = SenderIPIsNull() ? new TCP("192.168.0.120", 80) : new TCP(IP.text, 80);

    //    //yield return new WaitUntil(() => UIManager.sender.Connect());
    //    blocker.SetActive(false);
    //}

    //private bool SenderIPIsNull() => IP.text == "";

    //private void OnApplicationQuit()
    //{
    //    UIManager.sender.CloseStream();
    //}
}
