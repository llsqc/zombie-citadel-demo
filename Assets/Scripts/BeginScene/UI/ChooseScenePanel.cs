using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChooseScenePanel : BasePanel
{
    public Button btnLeft;
    public Button btnRight;
    public Button btnStart;
    public Button btnBack;

    public Text txtInfo;
    public Image imgScene;

    private int _nowIndex;
    private SceneInfo _nowSceneInfo;

    protected override void Init()
    {
        btnLeft.onClick.AddListener(() =>
        {
            --_nowIndex;
            if (_nowIndex < 0)
                _nowIndex = GameDataMgr.Instance.sceneInfoList.Count - 1;
            ChangeScene();
        });
        btnRight.onClick.AddListener(() =>
        {
            ++_nowIndex;
            if (_nowIndex >= GameDataMgr.Instance.sceneInfoList.Count)
                _nowIndex = 0;
            ChangeScene();
        });
        btnStart.onClick.AddListener(() =>
        {
            UIManager.Instance.HidePanel<ChooseScenePanel>();
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(_nowSceneInfo.sceneName);
            asyncOperation!.completed += (obj) => { GameLevelMgr.Instance.InitInfo(_nowSceneInfo); };
        });
        btnBack.onClick.AddListener(() =>
        {
            UIManager.Instance.HidePanel<ChooseScenePanel>();
            UIManager.Instance.ShowPanel<ChooseHeroPanel>();
        });
        ChangeScene();
    }

    private void ChangeScene()
    {
        _nowSceneInfo = GameDataMgr.Instance.sceneInfoList[_nowIndex];
        txtInfo.text = "名称:\n" + _nowSceneInfo.name + "\n" + "描述:\n" + _nowSceneInfo.tips;
        imgScene.sprite = Resources.Load<Sprite>(_nowSceneInfo.imgRes);
    }
}