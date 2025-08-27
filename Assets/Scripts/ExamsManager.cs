using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExamsManager : MonoBehaviour
{
    const int RE_ROLL_MAX = 1;
    const int OPTIONS_COUNT = 6;
    const int EXAM_QUEUE_MAX = 7;
    const string SAVE_EXAMS_NAME = "ExamsNames";
    const string SAVE_WASTED_NAME = "WastedButtons";

    [Header("Gameplay")]
    [SerializeField] Image gameplayBackground;
    [SerializeField] Slider finalsDiscovered;
    [SerializeField] Sprite[] backgrounds;
    [Space]

    [SerializeField] ResultController result;
    [Space]

    [SerializeField] TextEffect examText;
    [SerializeField] Image illustrationImage;

    [Header("Buttons")]
    [SerializeField] Color optionWasted;
    [SerializeField] Button option1, option2, option3, option4, option5, option6, option7;
    [Space]
    [SerializeField] Button reRoll;
    [SerializeField] Transform reRollRotationgPivot;

    [Header("Exams")]
    [SerializeField] ExamStruct[] exams;
    [SerializeField] string[] options;

    int option1Index, option2Index, option3Index, option4Index, option5Index, option6Index, examIndex;
    int endingsFound;
    bool doActivateFinalAnimation;

    Animator[] optionsAnimators;

    int reRollCount;
    List<int> ints;
    Queue<int> lastExams;
    List<int> availableExams;
    List<int> availableOptions;
    List<int> shownWastedOptions;
    List<int>[] wastedOptionsPerExam;

    ExamStruct CurrentExam => exams[examIndex];

    public string SaveExamsName => SAVE_EXAMS_NAME;
    public string SaveWastedName => SAVE_WASTED_NAME;
    public int EndingsFound => endingsFound;
    public int FinalsCount => exams.Length * 2;

    private void Awake()
    {
        ints = new List<int>();
        lastExams = new Queue<int>();
        availableExams = new List<int>();
        availableOptions = new List<int>();
        shownWastedOptions = new List<int>();
        wastedOptionsPerExam = new List<int>[exams.Length];
        for (int i = 0; i < wastedOptionsPerExam.Length; i++) wastedOptionsPerExam[i] = new List<int>();

        optionsAnimators = new Animator[OPTIONS_COUNT];
        optionsAnimators[0] = option1.GetComponent<Animator>();
        optionsAnimators[1] = option2.GetComponent<Animator>();
        optionsAnimators[2] = option3.GetComponent<Animator>();
        optionsAnimators[3] = option4.GetComponent<Animator>();
        optionsAnimators[4] = option5.GetComponent<Animator>();
        optionsAnimators[5] = option6.GetComponent<Animator>();

        for (int i = 0; i < exams.Length; i++) availableExams.Add(i);
        for (int i = 0; i < options.Length; i++) availableOptions.Add(i);

        FillInts();

        SetUpSavedData();
    }

    private void FixedUpdate()
    {
        if(reRoll.gameObject.activeInHierarchy) reRollRotationgPivot.Rotate(Vector3.forward * 3f);
    }

    void FillInts() 
    {
        ints.Clear();

        for (int i = 0; i < availableOptions.Count; i++) ints.Add(availableOptions[i]);
        for (int i = 0; i < wastedOptionsPerExam[examIndex].Count; i++) ints.Remove(wastedOptionsPerExam[examIndex][i]);
    }

    public void SetRandomExam()
    {
        reRollCount = 0;
        doActivateFinalAnimation = false;
        finalsDiscovered.value = endingsFound;

        do
            examIndex = availableExams[UnityEngine.Random.Range(0, availableExams.Count)];
        while (lastExams.Contains(examIndex) && availableExams.Count > EXAM_QUEUE_MAX);
        examText.Show(CurrentExam.Exam);
        illustrationImage.sprite = CurrentExam.Illustration;
        gameplayBackground.sprite = backgrounds[UnityEngine.Random.Range(0, backgrounds.Length)];

        lastExams.Enqueue(examIndex);
        if(lastExams.Count > EXAM_QUEUE_MAX) lastExams.Dequeue();

        shownWastedOptions.Clear();
        reRoll.gameObject.SetActive(true);

        SetRandomOptions();
    }
    
    public void SetRandomOptions(bool canRepeat = true)
    {
        if (canRepeat) FillInts();
        else
        {
            ++reRollCount;

            if (reRollCount >= RE_ROLL_MAX) reRoll.gameObject.SetActive(false);
        }

        option1Index = ints[UnityEngine.Random.Range(0, ints.Count)];
        option1.GetComponentInChildren<TextMeshProUGUI>().text = options[option1Index];
        option1.transform.GetChild(1).gameObject.SetActive(false);
        ints.Remove(option1Index);

        option2Index = ints[UnityEngine.Random.Range(0, ints.Count)];
        option2.GetComponentInChildren<TextMeshProUGUI>().text = options[option2Index];
        option2.transform.GetChild(1).gameObject.SetActive(false);
        ints.Remove(option2Index);
        
        option3Index = ints[UnityEngine.Random.Range(0, ints.Count)];
        option3.GetComponentInChildren<TextMeshProUGUI>().text = options[option3Index];
        option3.transform.GetChild(1).gameObject.SetActive(false);
        ints.Remove(option3Index);

        option4Index = ints[UnityEngine.Random.Range(0, ints.Count)];
        option4.GetComponentInChildren<TextMeshProUGUI>().text = options[option4Index];
        option4.transform.GetChild(1).gameObject.SetActive(false);
        ints.Remove(option4Index);

        option5Index = ints[UnityEngine.Random.Range(0, ints.Count)];
        option5.GetComponentInChildren<TextMeshProUGUI>().text = options[option5Index];
        option5.transform.GetChild(1).gameObject.SetActive(false);
        ints.Remove(option5Index);

        option6Index = ints[UnityEngine.Random.Range(0, ints.Count)];
        option6.GetComponentInChildren<TextMeshProUGUI>().text = options[option6Index];
        option6.transform.GetChild(1).gameObject.SetActive(false);
        ints.Remove(option6Index);

        for (int i = 0; i < optionsAnimators.Length; i++)
        {
            optionsAnimators[i].enabled = true;
            optionsAnimators[i].Play("ButtonColorAnimation", 0, UnityEngine.Random.Range(0f, 1f));
        }

        SetOptionsWasted();
    }

    public void Option(int optionIndex) 
    { 
        int chosenOptionIndex = -1;

        switch (optionIndex)
        {
            case 1:
                chosenOptionIndex = option1Index;
                break;

            case 2:
                chosenOptionIndex = option2Index;
                break;

            case 3:
                chosenOptionIndex = option3Index;
                break;

            case 4:
                chosenOptionIndex = option4Index;
                break;

            case 5:
                chosenOptionIndex = option5Index;
                break;

            case 6:
                chosenOptionIndex = option6Index;
                break;
        }

        if (chosenOptionIndex < 0) return;

        if (CurrentExam.GoodEnding == chosenOptionIndex + 1)
        {
            result.ShowGoodEnding(CurrentExam.GoodEndingMessage, CurrentExam.GoodFinalIllustration);

            if (!CurrentExam.goodEndingFound)
            {
                ++endingsFound; 
                doActivateFinalAnimation = true;
            }
            exams[examIndex].goodEndingFound = true;
            result.SetFinalsSlider(endingsFound, FinalsCount, doActivateFinalAnimation);
        }
        else if (CurrentExam.BadEnding == chosenOptionIndex + 1)
        {
            result.ShowBadEnding(CurrentExam.BadEndingMessage, CurrentExam.BadFinalIllustration);

            if (!CurrentExam.badEndingFound) 
            {
                ++endingsFound;
                doActivateFinalAnimation = true;
            }
            exams[examIndex].badEndingFound = true;
            result.SetFinalsSlider(endingsFound, FinalsCount, doActivateFinalAnimation);
        }
        else
        {
            result.ShowNeutralEnding(CurrentExam.NeutralEndingMessage, CurrentExam.Illustration);
        }

        if(CurrentExam.goodEndingFound && CurrentExam.badEndingFound)
        {
            availableExams.Remove(examIndex);
            availableOptions.Remove(chosenOptionIndex);
        }

        wastedOptionsPerExam[examIndex].Add(chosenOptionIndex);

        if(endingsFound >= FinalsCount)
        {
            PlayerPrefs.SetInt("ShowCompleted", 1);
        }

        SaveExams();
    }

    void SetOptionsWasted() 
    {
        if (wastedOptionsPerExam == default) wastedOptionsPerExam = new List<int>[exams.Length];
        if (wastedOptionsPerExam[examIndex] == null) wastedOptionsPerExam[examIndex] = new List<int>();

        // Set minimum 2 maximum 4 wasted options intentionally, each time (if available)
        int maxWastedCount = UnityEngine.Random.Range(2, 5);
        int wastedCount = 0;
        List<int> wastedIndexes = new List<int>();
        List<int> optionsIndexes = new List<int>();
        List<int> wasted = new List<int>();
        wasted.AddRange(wastedOptionsPerExam[examIndex]);

        for(int i = 0; i < OPTIONS_COUNT; i++) optionsIndexes.Add(i);
        for (int i = 0; i < shownWastedOptions.Count; i++) wasted.Remove(shownWastedOptions[i]);

        for (int i = 0; i < wasted.Count; i++)
        {
            if (wastedCount >= maxWastedCount) break;

            int r = optionsIndexes[UnityEngine.Random.Range(0, optionsIndexes.Count)];
            wastedIndexes.Add(r);
            optionsIndexes.Remove(r);

            wastedCount++;
        }

        for (int i = 0; i < wastedIndexes.Count; i++) 
        {
            int r = UnityEngine.Random.Range(0, wasted.Count);

            switch (wastedIndexes[i]) 
            {
                case 0:
                    ints.Add(option1Index);
                    option1Index = wasted[r];
                    option1.GetComponentInChildren<TextMeshProUGUI>().text = options[option1Index];
                    break;

                case 1:
                    ints.Add(option2Index);
                    option2Index = wasted[r];
                    option2.GetComponentInChildren<TextMeshProUGUI>().text = options[option2Index];
                    break;

                case 2:
                    ints.Add(option3Index);
                    option3Index = wasted[r];
                    option3.GetComponentInChildren<TextMeshProUGUI>().text = options[option3Index];
                    break;

                case 3:
                    ints.Add(option4Index);
                    option4Index = wasted[r];
                    option4.GetComponentInChildren<TextMeshProUGUI>().text = options[option4Index];
                    break;

                case 4:
                    ints.Add(option5Index);
                    option5Index = wasted[r];
                    option5.GetComponentInChildren<TextMeshProUGUI>().text = options[option5Index];
                    break;

                case 5:
                    ints.Add(option6Index);
                    option6Index = wasted[r];
                    option6.GetComponentInChildren<TextMeshProUGUI>().text = options[option6Index];
                    break;
            }

            shownWastedOptions.Add(wasted[r]);
            wasted.Remove(wasted[r]);
        }

        // Set the visual of wasted option
        if (wastedOptionsPerExam[examIndex].Contains(option1Index)) 
        {
            optionsAnimators[0].enabled = false;
            option1.GetComponent<Image>().color = optionWasted;
            option1.transform.GetChild(1).gameObject.SetActive(true);
        }
        if (wastedOptionsPerExam[examIndex].Contains(option2Index))
        {
            optionsAnimators[1].enabled = false;
            option2.GetComponent<Image>().color = optionWasted;
            option2.transform.GetChild(1).gameObject.SetActive(true);
        }
        if (wastedOptionsPerExam[examIndex].Contains(option3Index))
        {
            optionsAnimators[2].enabled = false;
            option3.GetComponent<Image>().color = optionWasted;
            option3.transform.GetChild(1).gameObject.SetActive(true);
        }
        if (wastedOptionsPerExam[examIndex].Contains(option4Index))
        {
            optionsAnimators[3].enabled = false;
            option4.GetComponent<Image>().color = optionWasted;
            option4.transform.GetChild(1).gameObject.SetActive(true);
        }
        if (wastedOptionsPerExam[examIndex].Contains(option5Index))
        {
            optionsAnimators[4].enabled = false;
            option5.GetComponent<Image>().color = optionWasted;
            option5.transform.GetChild(1).gameObject.SetActive(true);
        }
        if (wastedOptionsPerExam[examIndex].Contains(option6Index))
        {
            optionsAnimators[5].enabled = false;
            option6.GetComponent<Image>().color = optionWasted;
            option6.transform.GetChild(1).gameObject.SetActive(true);
        }
    }

    void SaveExams()
    {
        ExamResultDataForJSON examsDataForJSON = new ExamResultDataForJSON();
        examsDataForJSON.data = new List<ExamResultData>();

        for (int i = 0; i < exams.Length; i++) examsDataForJSON.data.Add(new ExamResultData(i + 1, exams[i].goodEndingFound, exams[i].badEndingFound));

        WastedButtonsDataForJSON wastedDataForJSON = new WastedButtonsDataForJSON();
        wastedDataForJSON.data = new WastedButtonsData[wastedOptionsPerExam.Length];

        for (int i = 0; i < wastedOptionsPerExam.Length; i++) wastedDataForJSON.data[i] = new WastedButtonsData(wastedOptionsPerExam[i]);


        string JSON_examsData = JsonUtility.ToJson(examsDataForJSON);
        string JSON_wastedData = JsonUtility.ToJson(wastedDataForJSON);

        PlayerPrefs.SetString(SAVE_EXAMS_NAME, JSON_examsData);
        PlayerPrefs.SetString(SAVE_WASTED_NAME, JSON_wastedData);
    }

    public List<ExamResultData> LoadExams() 
    {
        string JSON_examsData = PlayerPrefs.GetString(SAVE_EXAMS_NAME);

        ExamResultDataForJSON examsDataJSON = JsonUtility.FromJson<ExamResultDataForJSON>(JSON_examsData);
        List<ExamResultData> examsData = examsDataJSON.data;

        // DEBUG: Terms of the ExamResultData list
        /*
        for (int i = 0; i < examsData.Count; i++) 
        {
            Debug.Log($"Index: {examsData[i].index} | Good: {examsData[i].goodEndingFound} | Bad: {examsData[i].badEndingFound}");
        }
        */

        return examsData;
    }

    public void SetUpSavedData() 
    {
        string JSON_wastedData = PlayerPrefs.GetString(SAVE_WASTED_NAME);
        WastedButtonsDataForJSON wastedDataJSON = JsonUtility.FromJson<WastedButtonsDataForJSON>(JSON_wastedData);

        //Debug.Log(JSON_wastedData);

        if(wastedDataJSON.data.Length > 0) for (int i = 0; i < wastedOptionsPerExam.Length; i++) wastedOptionsPerExam[i] = wastedDataJSON.data[i].wastedOptions;


        List<ExamResultData> examResults = LoadExams();
        for (int i = 0; i < examResults.Count; i++) 
        {
            ExamResultData examResult = examResults[i];
            int examResultIndex = examResults[i].index - 1;

            if (examResult.goodEndingFound)
            {
                exams[examResultIndex].goodEndingFound = true;
                endingsFound++;

                if (examResult.badEndingFound) 
                {
                    exams[examResultIndex].badEndingFound = true;
                    endingsFound++;

                    availableExams.Remove(examResultIndex);

                    int r = UnityEngine.Random.Range(0, 2);
                    if (r == 0) availableOptions.Remove(exams[examResultIndex].GoodEnding - 1);
                    else availableOptions.Remove(exams[examResultIndex].BadEnding - 1);
                }
            }
            else if (examResult.badEndingFound)
            {
                exams[examResultIndex].badEndingFound = true;
                endingsFound++;

                if (examResult.goodEndingFound)
                {
                    exams[examResultIndex].goodEndingFound = true;
                    endingsFound++;

                    availableExams.Remove(examResultIndex);

                    int r = UnityEngine.Random.Range(0, 2);
                    if(r == 0) availableOptions.Remove(exams[examResultIndex].GoodEnding - 1);
                    else availableOptions.Remove(exams[examResultIndex].BadEnding - 1);
                }
            }
        }
    }
}
