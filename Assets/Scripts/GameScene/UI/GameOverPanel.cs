using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverPanel : BasePanel
{
    public Text txtEnd;
    public Text txtInfo;
    public Text txtMoney;

    public Button btnSure;

    protected override void Init()
    {
        btnSure.onClick.AddListener(() =>
        {
            UIManager.Instance.HidePanel<GameOverPanel>();
            UIManager.Instance.HidePanel<GamePanel>();
            GameLevelMgr.Instance.ClearInfo();

            SceneManager.LoadScene("BeginScene");
        });
    }

    public void InitInfo(int money, bool isWin)
    {
        txtEnd.text = isWin ? "你赢了" : "你输了";
        txtInfo.text = isWin ? "获得胜利奖励" : "获得失败奖励";
        txtMoney.text = "￥" + money;

        GameDataMgr.Instance.playerData.ownCoin += money;
        GameDataMgr.Instance.SavePlayerData();
    }

    public override void ShowMe()
    {
        base.ShowMe();
        Cursor.lockState = CursorLockMode.None;
    }
}