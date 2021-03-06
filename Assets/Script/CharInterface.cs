﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Team
{
    Monster,
    Hero,
}

public class CharInterface : MonoBehaviour
{
    public int HpMax = 10;
    public int Hp = 10;
    public event Action OnHpZero = delegate { };
    public event Action OnHPChange = delegate { };
    public bool IsDead = false;

    [SerializeField]
    private MonsterNav _monsterNav;
    public Team Team;
    CharacterState charState;
    public void TakeHealth(int addHP)
    {
        if (IsDead)
            return;
        Hp += addHP;
        Hp = Mathf.Clamp(Hp, 0, HpMax);
        OnHPChange.Invoke();
    }
    public void TakeDamage(int damage)
    {
        if (IsDead)
            return;


        if (Team == Team.Monster)
        {
            Debug.Log(GetComponent<Animator>());
            GetComponent<Animator>()?.SetTrigger("Hit");
            GetComponent<CharacterState>()?.Hit();
        }

        Hp -= damage;
        OnHPChange.Invoke();

        if (Hp <= 0)
        {
            Hp = 0;
            IsDead = true;
            GetComponent<CharacterState>().OnDeathTrigger();
            OnHpZero.Invoke();

            if (Team == Team.Monster)
            {
                _monsterNav.SwitchNav(NavType.Stop);
                GetComponent<Animator>()?.Play("Dead");
                CharManager.Instance.MainChar.TakeHealth(1);
            }

            if (CharManager.Instance == null)
            {
                Debug.LogFormat("[CharInterface.Start] Not Found CharManager.Instance");
                return;
            }

            CharManager.Instance.UnRegister(this);
            //TODO Death Show
        }
    }

    public void ChangeNav(NavType nav)
    {
        if (_monsterNav != null)
        {
            _monsterNav.SwitchNav(nav);
        }
        else
        {
            Debug.LogError("Not Found ");
        }
    }

    private void Start()
    {
        Hp = HpMax;
        if (CharManager.Instance == null)
        {
            Debug.LogFormat("[CharInterface.Start] Not Found CharManager.Instance");
            return;
        }
        CharManager.Instance.Register(this);
    }

    private void Awake()
    {
        charState = GetComponent<CharacterState>();
    }
}
