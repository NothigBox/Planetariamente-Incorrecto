using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MessagesController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] Button credits;

    public void SetOk() 
    {
        text.text = "OK";
        credits.gameObject.SetActive(false);
    }

    public void SetCompleteGame()
    {
        text.text = "Nos has salvado\r\nEstamos agradecidos!";
        credits.gameObject.SetActive(true);
    }

    public void SetNewFinal()
    {
        text.text = "Nuevo Final \r\nDescubierto!";
        credits.gameObject.SetActive(false);
    }
}
