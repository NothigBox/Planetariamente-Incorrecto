using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroController : MonoBehaviour
{
    const string BEGIN_MESSAGE = "Los extraterrestres nos han puesto a prueba y (por desgracia para nuestra raza) eres el encargado de demostrar que merecemos una oportunidad entre los seres inteligentes.\r\n¿Deseas continuar con las pruebas que decidiran el destino de la humanidad?";

    [SerializeField] Toggle showIntro;
    [SerializeField] GameObject beginButtonsParent;
    [SerializeField] GameObject introButtonsParent;
    [SerializeField] TextEffect introText;
    [SerializeField] string[] introParts;

    int partCount;

    public bool DoShowIntro => showIntro.isOn;

    private void OnEnable()
    {
        partCount = 0;
    }

    void ShowIntro(int part) 
    {
        introText.Show(introParts[part]);
    }

    public void Continue() 
    {
        if(partCount >= introParts.Length) 
        {
            ShowBeginButtons();
            introText.Show(BEGIN_MESSAGE);
            return;
        }
        else ShowBeginButtons(false);

        ShowIntro(partCount);

        partCount++;
    }

    void ShowBeginButtons(bool areActive = true) 
    {
        introButtonsParent.SetActive(!areActive);
        beginButtonsParent.SetActive(areActive);
    }
}
