using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapSelect : MonoBehaviour
{
    private bool isSelect = false;

    public int starsNum = 0;//解锁关卡所需要的星星数量
    public GameObject locks;
    public GameObject stars;

    public GameObject panel;//相对应的关卡现在页面
    public GameObject map;
    public Text starsText;

    public int starsnum = 1;
    public int endnum = 10;
    private void Start()
    {
        //PlayerPrefs.DeleteAll();//删除所有PlayerPrefs储存的键值对
        //PlayerPrefs是Unity内置的一个静态类，可以以键值对的方式存储一些简单的数据类型
        if (PlayerPrefs.GetInt("totalStarsNum", 0) >= starsNum)
        {
            isSelect = true;
        }

        if (isSelect)
        {
            locks.SetActive(false);
            stars.SetActive(true);
        }


        int count = 0;        
        for(int i = starsnum; i <= endnum; i++)
        {
            count += PlayerPrefs.GetInt("level" + i.ToString(), 0);
        }
        starsText.text = count.ToString()+"/30";

    }

    public void Selected()
    {
        if (isSelect)
        {
            panel.SetActive(true);
            map.SetActive(false);//将只身页面关闭，启动相对应的关卡现在页面
        }
    }
    public void panelSelect()
    {
        panel.SetActive(false);
        map.SetActive(true);
    }
}
