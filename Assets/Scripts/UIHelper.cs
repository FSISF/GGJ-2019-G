using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIHelper : MonoBehaviour
{
	[SerializeField]
	private Text MonsterCount;

    [SerializeField]
    private Image ImageHPBar;

	private void Awake()
	{
		CharManager.Instance.OnMonsterCountChange += OnMonsterCountChange;
        CharManager.Instance.MainChar.OnHPChange += OnPlayerHpChange;
		if (LoadSceneHelperr.IsLoadDone)
		{
			RefreshMonsterCountTxt();
		}
		else
		{
			LoadSceneHelperr.OnLoadAllDone += OnLoadSceneAllDone;
		}
	}

	private void OnLoadSceneAllDone()
	{
		LoadSceneHelperr.OnLoadAllDone -= OnLoadSceneAllDone;
		RefreshMonsterCountTxt();
	}

	private void OnMonsterCountChange()
	{
		RefreshMonsterCountTxt();
	}

	private void RefreshMonsterCountTxt()
	{
		MonsterCount.text = CharManager.Instance.LiveMonsterCount + "/" + CharManager.Instance.TotalMosterCount;
	}

    private void OnPlayerHpChange()
    {
        RefreshPlayerHP();
    }

    private void RefreshPlayerHP()
    {
        ImageHPBar.fillAmount = (float)CharManager.Instance.MainChar.Hp / (float)CharManager.Instance.MainChar.HpMax;
    }
}
