using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct ExamStruct
{
    [SerializeField] string exam;
    [SerializeField] Sprite illustration;
    [SerializeField] Sprite goodFinalIllustration;
    [SerializeField] Sprite badFinalIllustration;
    [SerializeField] string goodEndingMessage;
    [SerializeField] string badEndingMessage;
    [SerializeField] string neutralEndingMessage;

    [Header("Ending Indexes")]
    [SerializeField] int goodEnding;
    [SerializeField] int badEnding;

    public string Exam => exam;
    public Sprite Illustration => illustration;
    public Sprite GoodFinalIllustration => goodFinalIllustration;
    public Sprite BadFinalIllustration => badFinalIllustration;
    public string GoodEndingMessage => goodEndingMessage;
    public string BadEndingMessage => badEndingMessage;
    public string NeutralEndingMessage => neutralEndingMessage;

    public int GoodEnding => goodEnding;
    public int BadEnding => badEnding;

    [HideInInspector] public bool goodEndingFound;
    [HideInInspector] public bool badEndingFound;
}