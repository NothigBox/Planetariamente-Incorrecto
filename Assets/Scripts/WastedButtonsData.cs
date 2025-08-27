using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct WastedButtonsData
{
    public List<int> wastedOptions;

    public WastedButtonsData(List<int> wastedOptions) 
    {
        this.wastedOptions = wastedOptions;
    }
}
