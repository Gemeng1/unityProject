using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public enum ShowUIType:int
{
    CloseAll  =0,
    Stay,
    tanchuang,
}

public enum UIKind
{
    tanchuang,
    panel,
}

public struct uiInfo
{
    public ShowUIType type;
    public UIKind kind;
}

public class BaseUI : MonoBehaviour
{
    public uiInfo ui_info;
    protected CanvasGroup _canvasGroup;
    protected CanvasGroup canvasGroup
    {
        get
        {
            if(_canvasGroup == null)
            {
                _canvasGroup = gameObject.AddComponent<CanvasGroup>();
            }
            return _canvasGroup;
        }
    }
    private void Awake()
    {
        ui_info.type = ShowUIType.CloseAll;
        ui_info.kind = UIKind.panel;
    }
    public virtual void OnLoad()
    {
       
    }

    public virtual void OnEnter()
    {
        gameObject.SetActive(true);
       
        if (ui_info.type == ShowUIType.CloseAll)
        {
            canvasGroup.alpha = 0;
            Vector3 temp = transform.localPosition;
            temp.x = 600;
            transform.localPosition = temp;
            transform.DOLocalMoveX(0.0f, 0.5f);
            canvasGroup.DOFade(1, .3f);
        }
       
    }

    public virtual void OnExit()
    {
        if (ui_info.type == ShowUIType.CloseAll)
        {
            canvasGroup.DOFade(0, .5f);
            transform.DOLocalMoveX(600, 0.5f).OnComplete(()=>
            {
                gameObject.SetActive(false);
            });
        }
        else
        {
            gameObject.SetActive(false);
        }
        
       
        
    }

    public virtual void OnPause()
    {
        canvasGroup.blocksRaycasts = false;
    }

    public virtual void OnResume()
    {
        canvasGroup.blocksRaycasts = true;
    }
}
