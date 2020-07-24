using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDetailPanel : BaseUI
{
    private void Awake()
    {
        ui_info.type = ShowUIType.tanchuang;
        ui_info.kind = UIKind.tanchuang;
    }
}
