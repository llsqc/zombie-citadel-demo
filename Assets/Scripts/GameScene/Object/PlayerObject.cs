using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObject : MonoBehaviour
{
    private Animator _animator;
    private static readonly int vSpeed = Animator.StringToHash("VSpeed");
    private static readonly int hSpeed = Animator.StringToHash("HSpeed");
    private static readonly int roll = Animator.StringToHash("Roll");
    private static readonly int fire = Animator.StringToHash("Fire");

    private int atk;
    public int money;
    public float roundSpeed = 50f;

    public Transform gunPoint;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void InitPlayerInfo(int atk, int money)
    {
        this.atk = atk;
        this.money = money;
        UpdateMoney();
    }

    private void Update()
    {
        //移动变化 动作变化
        _animator.SetFloat(vSpeed, Input.GetAxis("Vertical"));
        _animator.SetFloat(hSpeed, Input.GetAxis("Horizontal"));

        transform.Rotate(Vector3.up, Input.GetAxis("Mouse X") * roundSpeed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            _animator.SetLayerWeight(1, 1);
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            _animator.SetLayerWeight(1, 0);
        }

        if (Input.GetKeyDown(KeyCode.Q))
            _animator.SetTrigger(roll);

        if (Input.GetMouseButtonDown(0))
            _animator.SetTrigger(fire);
    }

    //攻击动作的不同处理

    public void KnifeEvent()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position + transform.forward + transform.up, 1,
            1 << LayerMask.NameToLayer("Monster"));

        for (int i = 0; i < colliders.Length; i++)
        {
            //knife logic
        }
    }

    public void ShootEvent()
    {
        RaycastHit[] hits = new RaycastHit[50];
        int hitCount = Physics.RaycastNonAlloc(gunPoint.position, gunPoint.forward, hits, 1000,
            1 << LayerMask.NameToLayer("Monster"));

        for (int i = 0; i < hitCount; i++)
        {
            //shoot logic
        }
    }

    //金币更新的逻辑
    private void UpdateMoney()
    {
        UIManager.Instance.GetPanel<GamePanel>().UpdateMoney(money);
    }

    public void AddMoney(int money)
    {
        this.money += money;
        UpdateMoney();
    }
}