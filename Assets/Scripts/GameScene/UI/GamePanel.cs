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

    private TowerPoint nowTowerPoint;
    private bool _isInputLegal;

    protected override void Init()
    {
        btnQuit.onClick.AddListener(() =>
        {
            UIManager.Instance.HidePanel<GamePanel>();
            SceneManager.LoadScene("BeginScene");
        });

        botTrans.gameObject.SetActive(false);

        Cursor.lockState = CursorLockMode.Confined;
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

    public void UpdateSelectTower(TowerPoint towerPoint)
    {
        nowTowerPoint = towerPoint;

        if (nowTowerPoint == null)
        {
            _isInputLegal = false;
            botTrans.gameObject.SetActive(false);
        }
        else
        {
            _isInputLegal = true;
            botTrans.gameObject.SetActive(true);

            if (nowTowerPoint.nowTowerInfo == null)
            {
                for (var i = 0; i < towerBtns.Count; i++)
                {
                    towerBtns[i].gameObject.SetActive(true);
                    towerBtns[i].InitInfo(towerPoint.chooseIDs[i], "数字键" + (i + 1));
                }
            }
            else
            {
                foreach (var t in towerBtns)
                {
                    t.gameObject.SetActive(false);
                }

                towerBtns[1].gameObject.SetActive(true);
                towerBtns[1].InitInfo(nowTowerPoint.nowTowerInfo.nextLev, "空格键");
            }
        }
    }

    protected override void Update()
    {
        base.Update();

        if (!_isInputLegal)
            return;

        if (nowTowerPoint.nowTowerInfo == null)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                nowTowerPoint.CreateTower(nowTowerPoint.chooseIDs[0]);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                nowTowerPoint.CreateTower(nowTowerPoint.chooseIDs[1]);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                nowTowerPoint.CreateTower(nowTowerPoint.chooseIDs[2]);
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                nowTowerPoint.CreateTower(nowTowerPoint.nowTowerInfo.nextLev);
            }
        }
    }
}