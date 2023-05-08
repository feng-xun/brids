using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LevelSelect : MonoBehaviour
{
    public bool isSelect = false;
    public Sprite levelBG;
    public Sprite[] stars;
    
    private Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
    }
    void Start()
    {
        //�����ǰ�ؿ�Ϊ0Ҳ���ǵ�һ�أ���Ĭ��ΪisSelectΪtrue
        //������ǵ�һ�������ͨ��ǰһ���Ƿ����������ж�
        if (transform.parent.GetChild(0).name == gameObject.name || 
            PlayerPrefs.GetInt("level" + (int.Parse(gameObject.name) - 1).ToString()) != 0)
        {
           
            isSelect = true;
        }
        if (isSelect)
        {
            image.overrideSprite = levelBG;
            transform.Find("levelNum").gameObject.SetActive(true);
            //�ؿ�ѡ��ҳ���������ʾ
            int count = PlayerPrefs.GetInt("level" + gameObject.name);//ͨ���ؿ�����ȡ֮ǰ�������������
            if(count != 0)
            {   //��������������0������¸���ͼƬ
                //���ĵ������Ǳ������µ�һ�������壬���ĵ�ͼƬΪһ��Sprite������
                transform.Find("levelStars").gameObject.GetComponent<Image>().overrideSprite = stars[count - 1];
            }
            transform.Find("levelStars").gameObject.SetActive(true);
        }
    }

    public void Selected()
    {
        if (isSelect)
        {
            PlayerPrefs.SetString("nowlevel","level" + gameObject.name);//���ùؿ���level�����nowlevel��ӵ��ֵ���滻
            SceneManager.LoadScene(2);//���ص����Ϊ2�ĳ�������loadLevel�������ɶ�Ӧ�ؿ����Ĺؿ�
        }
    }
}
