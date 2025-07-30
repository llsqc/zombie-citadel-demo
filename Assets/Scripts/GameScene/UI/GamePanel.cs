using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamePanel : BasePanel
{
    public Image imgHP;

    public Text txtHP;
    public Text txtMoney;
    public Text txtWave;

    public float hpW = 500;

    public Button btnQuit;

    public Transform botTrans;

    public List<TowerBtn> towerBtns = new List<TowerBtn>();

    protected override void Init()
    {
        btnQuit.onClick.AddListener(() =>
        {
            UIManager.Instance.HidePanel<GamePanel>();
            SceneManager.LoadScene("BeginScene");
        });

        botTrans.gameObject.SetActive(false);
    }

    public void UpdateTowerHp(int hp, int maxHp)
    {
        txtHP.text = hp + "/" + maxHp;
        ((RectTransform)imgHP.transform).sizeDelta = new Vector2((float)hp / maxHp * hpW, 38);
    }

    public void UpdateMoney(int money)
    {
        txtMoney.text = money.ToString();
    }

    public void UpdateWaveNum(int nowNum, int maxNum)
    {
        txtWave.text = nowNum + "/" + maxNum;
    }
}