﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPanel : BaseUI
{
    private void Awake()
    {
        ui_info.type = ShowUIType.Stay;
    }
    public override void OnLoad()
    {
        base.OnLoad();
    }

    public override void OnEnter()
    {
        base.OnEnter();
    }

    public override void OnExit()
    {
        base.OnExit();
    }

    public override void OnPause()
    {
        base.OnPause();
    }

    public override void OnResume()
    {
        base.OnResume();
    }
}