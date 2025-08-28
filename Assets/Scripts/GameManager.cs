using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ExamsManager))]
public class GameManager : MonoBehaviour
{
    const float TIME_TO_QUIT_OK = 4.5f;
    const string SHOW_COMLPETED_GAME = "ShowCompleted";

    [SerializeField] AudioManager audio;
    [SerializeField] IntroController intro;
    [SerializeField] ResultController result;
    
    ExamsManager exams;

    public bool resetSavedExams;

    private void Awake()
    {
        exams = GetComponent<ExamsManager>();
        if (resetSavedExams) 
        {
            PlayerPrefs.SetInt(SHOW_COMLPETED_GAME, 0);
            PlayerPrefs.SetString(exams.SaveExamsName, JsonUtility.ToJson(new ExamResultDataForJSON()));
            PlayerPrefs.SetString(exams.SaveWastedName, JsonUtility.ToJson(new WastedButtonsDataForJSON()));
        }
    }

    void Start()
    {
        intro.gameObject.SetActive(false);
        GoHome();
    }

    public void ReRoll()
    {
        exams.SetRandomOptions(false);
        audio.PlayReRollClick();
    }

    public void Option(int index)
    {
        exams.Option(index);
        audio.PlayButtonClick();
    }

    public void SetNewExam() 
    {
        intro.gameObject.SetActive(false);

        result.SetScreenState(ScreenState.Gameplay);
        exams.SetRandomExam();
    }

    public void ContinueIntro()
    {
        if(intro.DoShowIntro == false)
        {
            SetNewExam();
            return; 
        }

        intro.gameObject.SetActive(true);
        intro.Continue();
    }

    public void GoHome() 
    {
        //Also PlayerPrefs
        /*
        */
        if (PlayerPrefs.GetInt(SHOW_COMLPETED_GAME) == 1)
        {
            result.ShowCompleteGamePopUp();
            return;
        }

        audio.SetTextLoop(false);
        result.SetScreenState(ScreenState.Home);
        
        result.SetFinalsSliderHome(exams.EndingsFound);
    }

    public void GoCredits()
    {
        result.ShowCredits();
    }

    public void QuitGame(bool doOkWait = false) 
    {
        if(!doOkWait) Application.Quit();
        else StartCoroutine(nameof(QuitGameCorroutine));
    }

    IEnumerator QuitGameCorroutine()
    {
        result.ShowOkPopUp();

        yield return new WaitForSeconds(TIME_TO_QUIT_OK);

        QuitGame();
    }
}

