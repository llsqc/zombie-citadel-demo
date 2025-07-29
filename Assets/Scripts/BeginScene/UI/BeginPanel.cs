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
        btnStart.onClick.AddListener(() =>
        {
            Camera.main?.GetComponent<CameraAnimator>().TurnLeft(() =>
            {
                UIManager.Instance.ShowPanel<ChooseHeroPanel>();
                
            });
            UIManager.Instance.HidePanel<BeginPanel>();
        });

        btnSetting.onClick.AddListener(() => { UIManager.Instance.ShowPanel<SettingPanel>(); });

        btnQuit.onClick.AddListener(Application.Quit);
    }
}