using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct ExamResultData
{
    public int index;
    public bool goodEndingFound;
    public bool badEndingFound;

    public ExamResultData(int index, bool goodEndingFound, bool badEndingFound)
    {
        this.index = index;
        this.goodEndingFound = goodEndingFound;
        this.badEndingFound = badEndingFound;
    }
}
