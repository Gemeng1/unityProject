using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BagPanel : BaseUI
{
    private Transform Content;
    private GameObject icon;
    private List<GameObject> iconList = new List<GameObject>();
    public override void OnLoad()
    {
        base.OnLoad();
    }

    private void Awake()
    {
        Content = transform.Find("ScrollView/Viewport/Content");
        icon = Resources.Load<GameObject>("icon");
        ui_info.type = ShowUIType.CloseAll;
        ui_info.kind = UIKind.panel;

    }

    public override void OnEnter()
    {
        base.OnEnter();
        for(int i = 0; i < 20; i++)
        {
            GameObject go = Instantiate(icon);
            go.transform.SetParent(Content, false);
            iconList.Add(go);
            go.GetComponent<Button>().onClick.AddListener(delegate
            {
                UIManager.Instance.PushUI(UIPanelType.ItemDetail);

            });
        }
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
