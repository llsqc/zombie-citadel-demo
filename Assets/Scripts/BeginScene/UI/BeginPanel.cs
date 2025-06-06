using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeginPanel : BasePanel
{
    public Button btnStart;
    public Button btnSetting;
    public Button btnQuit;

    protected override void Init()
    {
        btnStart.onClick.AddListener(() => { });

        btnSetting.onClick.AddListener(() => { });

        btnQuit.onClick.AddListener(Application.Quit);
    }
}