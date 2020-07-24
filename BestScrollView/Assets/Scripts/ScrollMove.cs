using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollMove : MonoBehaviour
{
    public ScrollRect scrollRect;
    public RectTransform gridRect;
    public RectOffset padding;
    public Vector2 spacing;
    public GameObject prefab;
    [SerializeField] private int itemCount = 10;
    [SerializeField]
    [Range(0,1)]
    private float scaleRatio = 0.5f;
    private List<GameObject> prefabsList = new List<GameObject>();
    private int instantiatCount;
    float itemHeight;
    float itemWidth;
    int upIndex = 0;
    int bottomIndex = 0;
    int oldMoveIndex = 0;
    float postionY = 0;
    float postionX = 0;
    float scrollRectHeight;
    float scrollRectWidth;
    bool isvertical;
    Vector3 centerPos;
    Vector2 vec;

    [System.Obsolete]
    private void Start()
    {
        isvertical = scrollRect.vertical;
        vec = isvertical ? new Vector2(0.5f, 1)
                    : new Vector2(0, 0.5f);

        gridRect.anchorMax = vec;
        gridRect.anchorMin = vec;
        gridRect.pivot = vec;
        gridRect.anchoredPosition = Vector2.zero;

        initScrollview();
        scrollRectHeight = scrollRect.GetComponent<RectTransform>().sizeDelta.y;//界面初始 的宽高
        scrollRectWidth = scrollRect.GetComponent<RectTransform>().sizeDelta.x;
        scrollRect.onValueChanged = new ScrollRect.ScrollRectEvent();
        scrollRect.onValueChanged.AddListener(OnScrollValueChanged);
    }

    [System.Obsolete]
    void initScrollview()
    {
        RectTransform prefab_rect = prefab.GetComponent<RectTransform>();
        itemHeight = prefab_rect.sizeDelta.y + spacing.y;//计算一个cell的宽 加上偏移
        itemWidth = prefab_rect.sizeDelta.x + spacing.x;
        if (isvertical)
        {
            gridRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, itemCount * itemHeight);//根据cell的高 设置content 的高
            instantiatCount = Mathf.CeilToInt(scrollRect.GetComponent<RectTransform>().sizeDelta.y / itemHeight) + 1;//跟scrollview的高来计算显示区域可以生成几个cell
        }
        else {
            gridRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, itemCount * itemWidth);//根据cell的宽 设置content 的宽
            instantiatCount = Mathf.CeilToInt(scrollRect.GetComponent<RectTransform>().sizeDelta.x / itemWidth) + 1;//跟scrollview的宽来计算显示区域可以生成几个cell
        }

       
        int centerIndex = Mathf.FloorToInt((instantiatCount - 1) / 2);
       // Debug.LogFormat("=======22222======={0}===", centerIndex);
        centerPos = isvertical ? new Vector2(0, -centerIndex * itemHeight)
                                 : new Vector2(centerIndex*itemWidth, 0);
        for(int i = 0; i < instantiatCount; i++)
        {
            GameObject obj = Instantiate(prefab, gridRect.transform) as GameObject;
            RectTransform obj_rect = obj.GetComponent<RectTransform>();
            obj_rect.anchorMax = vec;
            obj_rect.anchorMin = vec;
            obj_rect.pivot = vec;
            obj_rect.anchoredPosition = Vector2.zero;
            prefabsList.Add(obj);
            obj.GetComponent<RectTransform>().anchoredPosition = isvertical? new Vector2(0, -i * itemHeight)
                                                                            :new Vector2(i*itemWidth,0);
            var scrpt = obj.GetComponent<ItemMove>();
            if(scrpt == null)
            {
                scrpt = obj.AddComponent<ItemMove>();
                scrpt.scaleRatio = scaleRatio;
                scrpt.gridRect = gridRect;
                scrpt.spacing = spacing;
                scrpt.vertical = isvertical;
                scrpt.centerPos = centerPos;
            }
            
            obj.transform.FindChild("Text").GetComponent<Text>().text = i+"";
            obj.name = "item" + i;
          
        }
       
        bottomIndex = instantiatCount - 1;
        postionY = gridRect.anchoredPosition.y;
        postionX = gridRect.anchoredPosition.x;
        oldMoveIndex = isvertical? Mathf.FloorToInt(Mathf.Abs(gridRect.anchoredPosition.y / itemHeight))
                                    : Mathf.FloorToInt(Mathf.Abs(gridRect.anchoredPosition.x / itemWidth));
        //Debug.LogFormat("=========oldMoveIndex==={0}", oldMoveIndex);

    }

    [System.Obsolete]
    void OnScrollValueChanged(Vector2 vec)
    {
        float curPosY = gridRect.anchoredPosition.y;
        float curPosX = gridRect.anchoredPosition.x;
        int curMoveIndex = isvertical? Mathf.FloorToInt(Mathf.Abs(gridRect.anchoredPosition.y / itemHeight))
                                        : Mathf.FloorToInt(Mathf.Abs(gridRect.anchoredPosition.x / itemWidth));
        //Debug.LogFormat("=====curMoveIndex = {0}", curMoveIndex);
        int offsetCount = Mathf.Abs(curMoveIndex - oldMoveIndex);
        //Debug.LogFormat("=========={0}==={1}==={2}==={3}==={4}", curPosY, itemHeight, postionY, bottomIndex, itemCount);
        for(int i = 0; i < offsetCount; i++)
        {
            if (isvertical) {

                if (curPosY > itemHeight && curPosY > postionY && bottomIndex < itemCount - 1)
                {
                    upIndex++;
                    bottomIndex++;
                    updateLiatViewPos(true);
                }
                else if (curPosY > 0 && curPosY < postionY && (curPosY + scrollRectHeight) < (itemCount - 1) * itemHeight)
                {
                    upIndex--;
                    bottomIndex--;
                    updateLiatViewPos(false);
                }
            }
            else {

                if (curPosX < -itemWidth && curPosX < postionX && bottomIndex < itemCount - 1)
                {
                    upIndex++;
                    bottomIndex++;
                    updateLiatViewPos(true);
                }
                else if (curPosX < 0 && curPosX > postionX && Mathf.Abs(curPosX - scrollRectWidth) < (itemCount - 1) * itemWidth)
                {
                    upIndex--;
                    bottomIndex--;
                    updateLiatViewPos(false);
                }
            }


        }
        oldMoveIndex = curMoveIndex;
        postionY = curPosY;
        postionX = curPosX;
    }

    [System.Obsolete]
    void setGameObjectValue(int tempIndex,bool insertTop,int offetIndex)
    {
        GameObject gob = prefabsList[tempIndex];
        prefabsList.RemoveAt(tempIndex);
        if (insertTop) {
            prefabsList.Insert(0, gob);
        }
        else
        {
            prefabsList.Add(gob);
        }
        if (isvertical) {
            gob.GetComponent<RectTransform>().anchoredPosition = new Vector2(gob.GetComponent<RectTransform>().anchoredPosition.x, -offetIndex * itemHeight);
        }
        else
        {
            gob.GetComponent<RectTransform>().anchoredPosition = new Vector2(offetIndex*itemWidth, gob.GetComponent<RectTransform>().anchoredPosition.y);
        }

        
        gob.transform.FindChild("Text").GetComponent<Text>().text = "" + offetIndex;
    }

    [System.Obsolete]
    void updateLiatViewPos(bool isMoveUp)
    {
        int tempIndex = 0;
      
        if (isMoveUp)
        {
            setGameObjectValue(tempIndex, false, bottomIndex);
        }
        else
        {
            tempIndex = prefabsList.Count - 1;
            setGameObjectValue(tempIndex, true, upIndex);
        }
    }

   
}
