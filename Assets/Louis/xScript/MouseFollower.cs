﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MouseFollower : MonoBehaviour
{
    public LookDire lookDire;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.rotation = Lookat2D(transform.position, mousePos, lookDire);
    }
    public enum LookDire { up = 0, down = 180, right = 270, left = 90 }
    Quaternion Lookat2D(Vector2 myPos, Vector2 TargetPos,LookDire dire)
    {
        Vector2 dir = TargetPos - myPos;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        angle -= (float)dire;
        return Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
