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

    [SerializeField]
    private Image ImageGameOver;

    [SerializeField]
    private Button ResetButton;

	private void Awake()
	{
        ImageGameOver.color = Color.clear;
        ResetButton.interactable = false;
        ResetButton.GetComponent<CanvasGroup>().alpha = 0;

        CharManager.Instance.OnMonsterCountChange += OnMonsterCountChange;
        CharManager.Instance.MainChar.OnHPChange += OnPlayerHpChange;
        CharManager.Instance.MainChar.OnHpZero += OnPlayerHpZero;
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

    private void OnPlayerHpZero()
    {
        ImageGameOver.color = Color.white;
        ResetButton.interactable = true;
        ResetButton.GetComponent<CanvasGroup>().alpha = 1;
    }
}
