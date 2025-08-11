using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerBtn : MonoBehaviour
{
    public Image imgPic;
    public Text txtTip;
    public Text txtMoney;

    public void InitInfo(int id, string inputStr)
    {
        TowerInfo towerInfo = GameDataMgr.Instance.towerInfoList[id - 1];
        imgPic.sprite = Resources.Load<Sprite>(towerInfo.imgRes);
        txtMoney.text = "￥" + towerInfo.money;
        txtTip.text = inputStr;

        if (towerInfo.money > GameLevelMgr.Instance.player.money)
            txtMoney.text = "金钱不足";
    }
}