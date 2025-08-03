using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainTowerObject : MonoBehaviour
{
    private int maxHp;
    private int hp;

    private bool isDead;

    private static MainTowerObject _instance;
    public static MainTowerObject Instance => _instance;

    private void Awake()
    {
        _instance = this;
    }

    public void UpdateHp(int hp, int maxHp)
    {
        this.hp = hp;
        this.maxHp = maxHp;

        UIManager.Instance.GetPanel<GamePanel>().UpdateTowerHp(hp, maxHp);
    }

    public void Wound(int damage)
    {
        if (isDead)
            return;
        hp -= damage;
        if (hp <= 0)
        {
            hp = 0;
            isDead = true;
            //end game
        }

        UpdateHp(hp, maxHp);
    }

    private void OnDestroy()
    {
        _instance = null;
    }
}