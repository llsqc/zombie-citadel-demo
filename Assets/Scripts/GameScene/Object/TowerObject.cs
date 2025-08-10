using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerObject : MonoBehaviour
{
    public Transform head;
    public Transform gunPoint;

    private const float RoundSpeed = 20;

    private TowerInfo _towerInfo;
    private MonsterObject _targetObject;
    private List<MonsterObject> _targetObjects;
    private float _nowTime;
    private Vector3 _targetPos;

    public void InitInfo(TowerInfo info)
    {
        _towerInfo = info;
    }

    private void Update()
    {
        if (_towerInfo.atkType == 1)
        {
            SingleTargetAttack();
        }
        else
        {
            AreaAttack();
        }
    }

    private void SingleTargetAttack()
    {
        if (_targetObject == null || _targetObject.isDead ||
            Vector3.Distance(transform.position, _targetObject.transform.position) > _towerInfo.atkRange)
        {
            _targetObject = GameLevelMgr.Instance.FindMonster(transform.position, _towerInfo.atkRange);
        }

        if (_targetObject != null)
            return;

        _targetPos = _targetObject.transform.position;
        _targetPos.y = head.position.y;

        head.rotation = Quaternion.Slerp(head.rotation, Quaternion.LookRotation(_targetPos - head.position),
            RoundSpeed * Time.deltaTime);

        if (Vector3.Angle(head.forward, _targetPos = head.position) < 5 &&
            Time.time - _nowTime >= _towerInfo.offsetTime)
        {
            _targetObject.Wound(_towerInfo.atk);
            GameDataMgr.Instance.PlaySound("Music/Tower");
            GameObject effObj = Instantiate(Resources.Load<GameObject>(_towerInfo.eff), gunPoint.position,
                gunPoint.rotation);
            Destroy(effObj, 0.2f);
            _nowTime = Time.time;
        }
    }

    private void AreaAttack()
    {
        _targetObjects = GameLevelMgr.Instance.FindMonsters(transform.position, _towerInfo.atkRange);

        if (_targetObjects.Count > 0 && Time.time - _nowTime >= _towerInfo.offsetTime)
        {
            GameObject effObj = Instantiate(Resources.Load<GameObject>(_towerInfo.eff), gunPoint.position,
                gunPoint.rotation);
            Destroy(effObj, 0.2f);

            foreach (var t in _targetObjects)
            {
                t.Wound(_towerInfo.atk);
            }

            _nowTime = Time.time;
        }
    }
}