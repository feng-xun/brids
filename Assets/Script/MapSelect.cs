using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapSelect : MonoBehaviour
{
    private bool isSelect = false;

    public int starsNum = 0;//�����ؿ�����Ҫ����������
    public GameObject locks;
    public GameObject stars;

    public GameObject panel;//���Ӧ�Ĺؿ�����ҳ��
    public GameObject map;
    public Text starsText;

    public int starsnum = 1;
    public int endnum = 10;
    private void Start()
    {
        //PlayerPrefs.DeleteAll();//ɾ������PlayerPrefs����ļ�ֵ��
        //PlayerPrefs��Unity���õ�һ����̬�࣬�����Լ�ֵ�Եķ�ʽ�洢һЩ�򵥵���������
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
            map.SetActive(false);//��ֻ��ҳ��رգ��������Ӧ�Ĺؿ�����ҳ��
        }
    }
    public void panelSelect()
    {
        panel.SetActive(false);
        map.SetActive(true);
    }
}
