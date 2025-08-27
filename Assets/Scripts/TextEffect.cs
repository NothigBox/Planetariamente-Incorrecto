using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TextEffect : MonoBehaviour
{
    [SerializeField] float timeBetweenChars;

    float timer;
    char[] chars;
    int charIndex;
    bool isActive;
    TextMeshProUGUI text;

    public static Action<bool> OnTextWritting;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    private void FixedUpdate()
    {
        if (!isActive) return;

        if(timer >= timeBetweenChars) 
        {
            timer = 0;

            text.text += chars[charIndex];

            charIndex++;

            if (charIndex >= chars.Length) 
            {
                isActive = false; 

                OnTextWritting?.Invoke(false);
            }
        }

        timer += Time.fixedDeltaTime;
    }

    public void Show(string message) 
    {
        if(!text) text = GetComponent<TextMeshProUGUI>();

        text.text = "";
        charIndex = 0;
        timer = 0;

        chars = message.ToCharArray();

        isActive = true;

        OnTextWritting.Invoke(true);
    }
}
