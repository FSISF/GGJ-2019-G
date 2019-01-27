using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TitleUI : MonoBehaviour
{
    public Image ImageEyeLeft = null;
    public Image ImageEyeRight = null;
    //public Image ImageTitle = null;
    public Image ImageBlockBe = null;
    public Image ImageBlood = null;

    private bool IsPlayTile = false;

    void Start()
    {
        MusicSystem.Instance.PlayBGM(eBGM.Stage);
        StartCoroutine(PlayTitle());
    }

    private void ResetTitleScreen()
    {
        ImageEyeLeft.color = Color.clear;
        ImageEyeRight.color = Color.clear;
        ImageBlockBe.color = Color.clear;
        ImageBlood.color = Color.clear;
    }

    IEnumerator PlayTitle()
    {
        IsPlayTile = true;
        ResetTitleScreen();
        yield return new WaitForSeconds(0.5f);

        ImageBlockBe.color = Color.black;
        yield return new WaitForSeconds(0.1f);

        ImageBlockBe.color = Color.clear;
        yield return new WaitForSeconds(0.1f);

        ImageBlockBe.color = Color.black;
        yield return new WaitForSeconds(0.1f);

        ImageBlockBe.color = Color.clear;
        yield return new WaitForSeconds(0.25f);

        ImageBlood.color = Color.white;
        yield return new WaitForSeconds(0.25f);

        ImageEyeLeft.DOColor(Color.white, 0.5f);
        ImageEyeRight.DOColor(Color.white, 0.5f);
        yield return new WaitForSeconds(0.5f);

        IsPlayTile = false;
    }

    private void Update()
    {
        if (!IsPlayTile && Input.anyKeyDown)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Startupup");
        }
    }
}