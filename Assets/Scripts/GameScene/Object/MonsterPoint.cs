using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class MonsterPoint : MonoBehaviour
{
    public int maxWave;
    public int numEachWave;
    private int _nowNum;

    public List<int> monsterIds;
    private int _nowIds;

    public float createOffsetTime;

    public float delayTime;

    public float firstDelayTime;

    private void Start()
    {
        Invoke(nameof(CreateWave), firstDelayTime);

        GameLevelMgr.Instance.AddMonsterPoint(this);
        GameLevelMgr.Instance.UpdateMaxNum(maxWave);
    }

    private void CreateWave()
    {
        _nowIds = monsterIds[Random.Range(0, monsterIds.Count)];
        _nowNum = numEachWave;
        CreateMonster();
        --maxWave;
        GameLevelMgr.Instance.ChangeNowWaveNum(1);
    }

    private void CreateMonster()
    {
        MonsterInfo info = GameDataMgr.Instance.monsterInfoList[_nowIds - 1];
        GameObject obj = Instantiate(Resources.Load<GameObject>(info.res), transform.position, Quaternion.identity);
        MonsterObject monsterObj = obj.AddComponent<MonsterObject>();
        monsterObj.InitInfo(info);

        GameLevelMgr.Instance.AddMonster(monsterObj);

        _nowNum--;
        if (_nowNum == 0)
        {
            if (maxWave > 0)
            {
                Invoke(nameof(CreateWave), delayTime);
            }
        }
        else
        {
            Invoke(nameof(CreateMonster), createOffsetTime);
        }
    }

    public bool IsOver()
    {
        return _nowNum == 0 && maxWave == 0;
    }
}