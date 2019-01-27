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

	private void Awake()
	{
		CharManager.Instance.OnMonsterCountChange += OnMonsterCountChange;
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
}
