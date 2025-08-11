using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPoint : MonoBehaviour
{
    private GameObject _towerObject = null;
    public TowerInfo nowTowerInfo = null;

    public List<int> chooseIDs;

    public void CreateTower(int id)
    {
        TowerInfo towerInfo = GameDataMgr.Instance.towerInfoList[id - 1];
        if (towerInfo.money > GameLevelMgr.Instance.player.money)
            return;

        GameLevelMgr.Instance.player.AddMoney(-towerInfo.money);
        if (_towerObject != null)
        {
            Destroy(_towerObject);
            _towerObject = null;
        }

        _towerObject = Instantiate(Resources.Load<GameObject>(towerInfo.res), transform.position,
            Quaternion.identity);
        _towerObject.GetComponent<TowerObject>().InitInfo(towerInfo);

        nowTowerInfo = towerInfo;

        if (nowTowerInfo.nextLev != 0)
        {
            UIManager.Instance.GetPanel<GamePanel>().UpdateSelectTower(this);
        }
        else
        {
            UIManager.Instance.GetPanel<GamePanel>().UpdateSelectTower(null);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (nowTowerInfo != null && nowTowerInfo.nextLev == 0)
            return;

        UIManager.Instance.GetPanel<GamePanel>().UpdateSelectTower(this);
    }

    private void OnTriggerExit(Collider other)
    {
        UIManager.Instance.GetPanel<GamePanel>().UpdateSelectTower(null);
    }
}