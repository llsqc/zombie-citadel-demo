using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameLevelMgr
{
    private static GameLevelMgr _instance = new GameLevelMgr();
    public static GameLevelMgr Instance => _instance;

    public PlayerObject player;

    private List<MonsterPoint> _monsterPoints = new List<MonsterPoint>();

    private int _nowWaveNum;
    private int _maxWaveNum;

    private int _aliveCount;


    private GameLevelMgr()
    {
    }

    public void InitInfo(SceneInfo info)
    {
        UIManager.Instance.ShowPanel<GamePanel>();
        RoleInfo roleInfo = GameDataMgr.Instance.nowSelectRole;
        Transform bornPos = GameObject.Find("BornPos").transform;
        GameObject heroObj =
            GameObject.Instantiate(Resources.Load<GameObject>(roleInfo.res), bornPos.position, bornPos.rotation);

        player = heroObj.GetComponent<PlayerObject>();
        player.InitPlayerInfo(roleInfo.atk, info.money);

        Camera.main.GetComponent<CameraMove>().SetTarget(heroObj.transform);

        MainTowerObject.Instance.UpdateHp(info.towerHp, info.towerHp);
    }

    public void AddMonsterPoint(MonsterPoint point)
    {
        _monsterPoints.Add(point);
    }

    public void UpdateMaxNum(int num)
    {
        _maxWaveNum += num;
        _nowWaveNum = _maxWaveNum;
        UIManager.Instance.ShowPanel<GamePanel>().UpdateWaveNum(_nowWaveNum, _maxWaveNum);
    }

    public void ChangeNowWaveNum(int num)
    {
        _nowWaveNum -= num;
        UIManager.Instance.ShowPanel<GamePanel>().UpdateWaveNum(_nowWaveNum, _maxWaveNum);
    }

    public bool IsGameOver()
    {
        if (_monsterPoints.Any(t => !t.IsOver()))
            return false;
        return _aliveCount <= 0;
    }

    public void ChangeMonsterNum(int num)
    {
        _aliveCount += num;
    }

    public void ClearInfo()
    {
        _monsterPoints.Clear();
        _aliveCount = 0;
        _nowWaveNum = 0;
        _maxWaveNum = 0;
        player = null;
    }
}