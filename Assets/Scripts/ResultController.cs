using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResultController : MonoBehaviour
{
    const string CREDITS = "Musica y SFX:\r\nLuisa Zarama\r\n\r\nGame Design, Desarrollo y Guion:\r\nJose Agudelo\r\n\r\nArte IA:\r\nDall-E 2\r\nLearn With Hasan\r\nAI Photo Shoot\r\n\r\n\r\nGracias especiales a\r\nPablo Arango\r\npor su aporte al guion del juego";

    const float TIME_NEW_FINAL = 3f;
    const float TIME_BETWEEN_TICKS = 0.1f;
    const int TICKS = 10;

    [SerializeField] Image resultIllustration;
    [Space]

    [Header("Home")]
    [SerializeField] Image homeIllustration;
    [SerializeField] Sprite[] illustrations;
    [SerializeField] Slider finalsDiscovered;

    [Header("Panels")]
    [SerializeField] GameObject homePanel;
    [SerializeField] GameObject finalPanel;
    [SerializeField] GameObject resultPanel;
    [SerializeField] GameObject creditsPanel;
    [SerializeField] GameObject gameplayPanel;

    [SerializeField] MessagesController messages;

    [Header("Texts")]
    [SerializeField] TextEffect final;
    [SerializeField] TextEffect result;
    [SerializeField] TextEffect credits;

    [Header("Final")]
    [SerializeField] Slider finalsSlider;
    [SerializeField] TextEffect finalsText;
    [SerializeField] AudioManager audio;
    [SerializeField] Image finalIllustration;

    public bool? doShowCompletedGame = false;

    public void ShowGoodEnding(string message, Sprite illustration) 
    {
        final.Show(message);
        SetScreenState(ScreenState.Final);

        finalIllustration.sprite = illustration;
        audio.PlayGoodFinal();
    }

    public void ShowBadEnding(string message, Sprite illustration)
    {
        final.Show(message);
        SetScreenState(ScreenState.Final);

        finalIllustration.sprite = illustration;
        audio.PlayBadFinal();
    }

    public void ShowNeutralEnding(string message, Sprite illustration)
    {
        result.Show(message);
        SetScreenState(ScreenState.Result);

        resultIllustration.sprite = illustration;
    }

    public void ShowCredits()
    {
        credits.Show(CREDITS);
        SetScreenState(ScreenState.Credits);
    }

    public void HidePanels() 
    {
        homePanel.SetActive(false);
        finalPanel.SetActive(false);
        resultPanel.SetActive(false);
        creditsPanel.SetActive(false);
        gameplayPanel.SetActive(false);

        messages.gameObject.SetActive(false);
    }

    public void SetFinalsSlider(int amount, int max, bool doActivateAnimation = true) 
    {
        if(doActivateAnimation) StartCoroutine(FinalSliderCorroutine(amount, max));
        else 
        { 
            finalsSlider.value = amount;
            finalsText.Show($"{amount} / {max}");
        }
    }

    IEnumerator FinalSliderCorroutine(int amount, int max)
    {
        messages.gameObject.SetActive(true);
        messages.SetNewFinal();

        yield return new WaitForSeconds(TIME_NEW_FINAL);

        messages.gameObject.SetActive(false);

        yield return new WaitForSeconds(0.2f);

        finalsText.Show($"{amount} / {max}");

        for (int i = 0; i < TICKS; i++) 
        {
            finalsSlider.value = amount - 1;
            yield return new WaitForSeconds(TIME_BETWEEN_TICKS);
            finalsSlider.value = amount;
            yield return new WaitForSeconds(TIME_BETWEEN_TICKS);
        }

        finalsSlider.value = amount;
    }

    public void SetFinalsSliderHome(int amount)
    { 
        finalsDiscovered.value = amount;
    }

    public void ShowOkPopUp()
    {
        messages.gameObject.SetActive(true);
        messages.SetOk();
    }

    public void ShowCompleteGamePopUp()
    {
        messages.gameObject.SetActive(true);
        messages.SetCompleteGame();
    }

    public void DeactivateCompletedGamePopUp() 
    {
        PlayerPrefs.SetInt("ShowCompleted", 0);
    }

    public void SetScreenState(ScreenState state) 
    {
        HidePanels();

        switch (state)
        {
            case ScreenState.Home:
                homePanel.SetActive(true);
                homeIllustration.sprite = illustrations[UnityEngine.Random.Range(0, illustrations.Length)];
                break;

            case ScreenState.Gameplay:
                gameplayPanel.SetActive(true);
                break;

            case ScreenState.Final:
                finalPanel.SetActive(true);
                break;

            case ScreenState.Result:
                resultPanel.SetActive(true);
                break;

            case ScreenState.Credits:
                creditsPanel.SetActive(true);
                break;
        }
    }
}

public enum ScreenState { Home, Gameplay, Final, Result, Credits }
