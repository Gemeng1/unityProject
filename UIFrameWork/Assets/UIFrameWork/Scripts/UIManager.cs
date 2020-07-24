using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class UIManager 
{
    private static UIManager _instance;
    private Dictionary<UIPanelType, BaseUI> ui_dic;
    private Stack<BaseUI> ui_Stack;//显示的ui
    private Transform canvasTransform;
    private Transform CanvasTransform
    {
        get
        {
            if(canvasTransform == null)
            {
                canvasTransform = GameObject.Find("Canvas").transform;
            }
            return canvasTransform;
        }
    }
    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new UIManager();
            }

            return _instance;
        }
    }

    
    

    public void PushUI(UIPanelType type)
    {
        if(ui_Stack == null)
        {
            ui_Stack = new Stack<BaseUI>();
        }

        BaseUI ui = getPanel(type);
        if(ui.ui_info.type == ShowUIType.CloseAll)
        {
            for (int i = 0; i < ui_Stack.Count; i++)
            {
                if (ui_Stack.Peek().ui_info.type != ShowUIType.Stay)
                {
                    PopUI();
                }
            }
        }

        if (ui.ui_info.kind == UIKind.tanchuang)
        {
            foreach (var go in ui_Stack)
            {
                go.OnPause();
            }
        }
        ui.OnEnter();
        ui_Stack.Push(ui);



    }

    public void PopUI()
    {
        if (ui_Stack.Count <= 0)
            return;
        BaseUI ui = ui_Stack.Pop();
        ui.OnExit();
        if(ui.ui_info.kind == UIKind.tanchuang)
        {
            foreach (var go in ui_Stack)
            {
                go.OnResume();
            }
        }
    }

    private BaseUI getPanel(UIPanelType type)
    {
        if (ui_dic == null)
        {
            ui_dic = new Dictionary<UIPanelType, BaseUI>();
        }
        BaseUI panel = ui_dic.TryGet(type);
        if (panel == null)
        {
            GameObject go = GameObject.Instantiate(Resources.Load<GameObject>(type.ToString() + "Panel"));
            go.transform.SetParent(CanvasTransform, false);
            panel = go.GetComponent<BaseUI>();
            ui_dic.Add(type, panel);
            panel.OnLoad();
        }
        return panel;
    }
}
