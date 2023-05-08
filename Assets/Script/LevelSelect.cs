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
        //如果当前关卡为0也就是第一关，则默认为isSelect为true
        //如果不是第一关则可以通过前一关是否有星星来判断
        if (transform.parent.GetChild(0).name == gameObject.name || 
            PlayerPrefs.GetInt("level" + (int.Parse(gameObject.name) - 1).ToString()) != 0)
        {
           
            isSelect = true;
        }
        if (isSelect)
        {
            image.overrideSprite = levelBG;
            transform.Find("levelNum").gameObject.SetActive(true);
            //关卡选择页面的星星显示
            int count = PlayerPrefs.GetInt("level" + gameObject.name);//通过关卡名获取之前储存的星星数量
            if(count != 0)
            {   //在星星数不等于0的情况下更改图片
                //更改的物体是本物体下的一个子物体，更改的图片为一个Sprite的数组
                transform.Find("levelStars").gameObject.GetComponent<Image>().overrideSprite = stars[count - 1];
            }
            transform.Find("levelStars").gameObject.SetActive(true);
        }
    }

    public void Selected()
    {
        if (isSelect)
        {
            PlayerPrefs.SetString("nowlevel","level" + gameObject.name);//设置关卡名level？如果nowlevel已拥有值则替换
            SceneManager.LoadScene(2);//加载到编号为2的场景，用loadLevel类来生成对应关卡名的关卡
        }
    }
}
