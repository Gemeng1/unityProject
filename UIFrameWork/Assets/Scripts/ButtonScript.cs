using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class ButtonScript : MonoBehaviour
{
    private void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(delegate
        {
            onClick(gameObject.name);

        });
    }
    private void onClick(string name)
    {
       if(name == "Battle")
        {
            return;
        }
        if (name == "CloseBtton")
        {
            UIManager.Instance.PopUI();
        }
        else {
            UIPanelType type = (UIPanelType)System.Enum.Parse(typeof(UIPanelType), name);
            UIManager.Instance.PushUI(type);
        }

        
    }
}
