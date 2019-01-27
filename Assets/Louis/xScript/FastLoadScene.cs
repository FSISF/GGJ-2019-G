﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FastLoadScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LoadScene(string SceneName)
    {
        SceneManager.LoadSceneAsync(SceneName);
        PlayerController.Instance.Release();
        LoadSceneHelperr.IsLoadDone = false;
    }
}
