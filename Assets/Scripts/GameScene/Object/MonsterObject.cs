using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterObject : MonoBehaviour
{
    private static readonly int wound = Animator.StringToHash("Wound");
    private static readonly int dead = Animator.StringToHash("Dead");
    private static readonly int run = Animator.StringToHash("Run");
    private static readonly int atk = Animator.StringToHash("Atk");
    private Animator _animator;
    private NavMeshAgent _agent;
    private MonsterInfo _monsterInfo;

    private int hp;
    public bool isDead = false;

    private float _frontTime;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
        _agent.stoppingDistance = 5.0f;
    }

    public void InitInfo(MonsterInfo info)
    {
        _monsterInfo = info;
        _animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>(info.animator);
        hp = info.hp;
        _agent.speed = _agent.acceleration = info.moveSpeed;
        _agent.angularSpeed = info.roundSpeed;
    }

    public void Wound(int dmg)
    {
        if (isDead)
            return;

        hp -= dmg;
        _animator.SetTrigger(wound);
        if (hp <= 0)
        {
            Dead();
        }
        else
        {
            GameDataMgr.Instance.PlaySound("Music/Wound");
        }
    }

    public void Dead()
    {
        isDead = true;
        _agent.enabled = false;
        _animator.SetBool(dead, true);
        GameDataMgr.Instance.PlaySound("Music/dead");
        GameLevelMgr.Instance.player.AddMoney(10);
    }

    public void DeadEvent()
    {
        GameLevelMgr.Instance.RemoveMonster(this);
        Destroy(gameObject);

        if (GameLevelMgr.Instance.IsGameOver())
        {
            GameOverPanel panel = UIManager.Instance.ShowPanel<GameOverPanel>();
            panel.InitInfo(GameLevelMgr.Instance.player.money, true);
        }
    }

    public void BornOver()
    {
        _agent.SetDestination(MainTowerObject.Instance.transform.position);
        _animator.SetBool(run, true);
    }

    void Update()
    {
        if (isDead)
            return;
        _animator.SetBool(run, _agent.velocity != Vector3.zero);
        if (Vector3.Distance(this.transform.position, MainTowerObject.Instance.transform.position) < 5 &&
            Time.time - _frontTime >= _monsterInfo.atkOffset)
        {
            _frontTime = Time.time;
            _animator.SetTrigger(atk);
        }
    }

    public void AtkEvent()
    {
        Collider[] colliders = new Collider[100];
        var size = Physics.OverlapSphereNonAlloc(transform.position + transform.forward + transform.up, 1, colliders,
            1 << LayerMask.NameToLayer("MainTower"));

        GameDataMgr.Instance.PlaySound("Music/Eat");
        for (int i = 0; i < size; i++)
        {
            if (MainTowerObject.Instance.gameObject == colliders[i].gameObject)
            {
                MainTowerObject.Instance.Wound(_monsterInfo.atk);
            }
        }
    }
}