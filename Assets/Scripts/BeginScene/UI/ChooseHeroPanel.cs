using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ChooseHeroPanel : BasePanel
{
    public Button btnLeft;
    public Button btnRight;
    public Button btnUnlock;
    public Text txtUnlock;
    public Button btnStart;
    public Button btnBack;
    public Text txtMoney;
    public Text txtName;

    private Transform _heroPos;

    private GameObject _heroObj;
    private RoleInfo _nowRoleData;
    private int _nowIndex;

    protected override void Init()
    {
        _heroPos = GameObject.Find("HeroPos").transform;

        txtMoney.text = GameDataMgr.Instance.playerData.ownCoin.ToString();

        btnLeft.onClick.AddListener(() =>
        {
            --_nowIndex;
            if (_nowIndex < 0)
                _nowIndex = GameDataMgr.Instance.roleInfoList.Count - 1;

            ChangeHero();
        });
        btnRight.onClick.AddListener(() =>
        {
            ++_nowIndex;
            if (_nowIndex >= GameDataMgr.Instance.roleInfoList.Count)
                _nowIndex = 0;

            ChangeHero();
        });
        btnUnlock.onClick.AddListener(() =>
        {
            PlayerData data = GameDataMgr.Instance.playerData;
            if (data.ownCoin >= _nowRoleData.lockMoney)
            {
                data.ownCoin -= _nowRoleData.lockMoney;
                txtMoney.text = data.ownCoin.ToString();
                data.ownHero.Add(_nowRoleData.id);
                GameDataMgr.Instance.SavePlayerData();
                UpdateLockButton();
                //提示面板
                UIManager.Instance.ShowPanel<TipPanel>().ChangeInfo("解锁成功");
            }
            else
            {
                //提示面板显示金币不足
                UIManager.Instance.ShowPanel<TipPanel>().ChangeInfo("金币不足");
            }
        });
        btnStart.onClick.AddListener(() =>
        {
            GameDataMgr.Instance.nowSelectRole = _nowRoleData;
            UIManager.Instance.HidePanel<ChooseHeroPanel>();
            UIManager.Instance.ShowPanel<ChooseScenePanel>();
        });
        btnBack.onClick.AddListener(() =>
        {
            UIManager.Instance.HidePanel<ChooseHeroPanel>();
            Camera.main?.GetComponent<CameraAnimator>().TurnRight(() =>
            {
                UIManager.Instance.ShowPanel<BeginPanel>();
            });
        });

        ChangeHero();
    }

    private void ChangeHero()
    {
        if (_heroObj != null)
        {
            Destroy(_heroObj);
            _heroObj = null;
        }

        _nowRoleData = GameDataMgr.Instance.roleInfoList[_nowIndex];
        _heroObj = Instantiate(Resources.Load<GameObject>(_nowRoleData.res), _heroPos.position, _heroPos.rotation);

        UpdateLockButton();
    }

    private void UpdateLockButton()
    {
        if (_nowRoleData.lockMoney > 0 && !GameDataMgr.Instance.playerData.ownHero.Contains(_nowRoleData.id))
        {
            btnUnlock.gameObject.SetActive(true);
            txtUnlock.text = "¥" + _nowRoleData.lockMoney;
            btnStart.gameObject.SetActive(false);
        }
        else
        {
            btnUnlock.gameObject.SetActive(false);
            btnStart.gameObject.SetActive(true);
        }
    }

    public override void HideMe(UnityAction callBack)
    {
        base.HideMe(callBack);
        if (_heroObj != null)
        {
            DestroyImmediate(_heroObj);
            _heroObj = null;
        }
    }
}