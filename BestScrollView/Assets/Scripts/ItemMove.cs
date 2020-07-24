using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMove : MonoBehaviour
{
    private Vector3 _centerPos;
    private Vector2 _spacing;
    private RectTransform _gridRect;
    private float _scaleRatio;
    private bool isset = false;
    private bool _vertical;

    public Vector3 centerPos
    {
        get
        {
            return _centerPos;
        }
        set
        {
            _centerPos = value;
            isset = true;
        }
    }

    public Vector2 spacing
    {
        get
        {
            return _spacing;
        }

        set
        {
            _spacing = value;
        }
    }

    public RectTransform gridRect
    {
        get
        {
            return _gridRect;
        }


        set
        {
            _gridRect = value;
        }
    }

    public float scaleRatio
    {
        get
        {
            return _scaleRatio;
        }

        set
        {
            _scaleRatio = value;
        }
    }

    public bool vertical
    {
        get
        {
            return _vertical;
        }

        set
        {
            _vertical = value;
        }
    }

    // Start is called before the first frame update
    private void FixedUpdate()
    {
        if (isset)
        {
            changeItemPos();
        }
    }

    void changeItemPos() {
        if (vertical) {
            float new_y = centerPos.y - gridRect.anchoredPosition.y;
            float scal = 1 - Mathf.Abs(new_y - gameObject.GetComponent<RectTransform>().anchoredPosition.y) / (spacing.y * 10) * scaleRatio;
            gameObject.transform.localScale = new Vector3(scal, scal, scal);
            float pos_x = centerPos.x + spacing.x * (Mathf.Abs(new_y - gameObject.GetComponent<RectTransform>().anchoredPosition.y) / (spacing.y));
            gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(pos_x, transform.GetComponent<RectTransform>().anchoredPosition.y);
        }
        else {
            float new_x = centerPos.x -gridRect.anchoredPosition.x;
            //Debug.LogFormat("======new_x====={0}====", new_x);
            //Debug.LogFormat("======gameObject.GetComponent<RectTransform>().anchoredPosition.x===={0}====",
            //    gameObject.GetComponent<RectTransform>().anchoredPosition.x);
            float scal = 1 - Mathf.Abs(new_x - gameObject.GetComponent<RectTransform>().anchoredPosition.x) / (spacing.x * 10) * scaleRatio;
            gameObject.transform.localScale = new Vector3(scal, scal, scal);
            float pos_y = centerPos.y + spacing.y * (Mathf.Abs(new_x - gameObject.GetComponent<RectTransform>().anchoredPosition.x) / (spacing.x));
            gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(transform.GetComponent<RectTransform>().anchoredPosition.x, pos_y);
        }


       
    }

}
