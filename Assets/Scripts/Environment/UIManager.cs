using System.Collections;
using System.Collections.Generic;
using SKCell;
using UnityEngine;
using UnityEngine.Events;

public enum UI_Layer
{
    LowMost,
    Low,
    Main,
    High,
    Higher,
    TopMost,
    Constant
}

public class UIManager
{
    private Transform lowMost;
    private Transform low;
    private Transform main;
    private Transform high;
    private Transform higher;
    private Transform topMost;
    private Transform constant;
    public GameObject root;

    public UIManager()
    {
        root = GameObject.Find("UIRoot");
        GameObject.DontDestroyOnLoad(root);
        lowMost =root.transform.Find("UILowMost -- 1");
        low =root.transform.Find("UILow -- 2");
        main =root.transform.Find("UIMain -- 3");
        high =root.transform.Find("UIHigh -- 4");
        higher =root.transform.Find("UIHigher -- 5");
        topMost =root.transform.Find("UITopMost -- 6");
        constant =root.transform.Find("UIConstant -- 7");
    }
    
}
