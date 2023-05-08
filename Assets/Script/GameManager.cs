using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public List<Bird> birds;  //获取脚本bird类集合
    public List<Pig> pigs;//获取脚本pig类集合
    public GameObject[] stars; //获取胜利界面星星物体
    public static GameManager _instance;//静态变量，单例模式
    public GameObject lose;
    public GameObject win;

    private Vector3 originPos;//第一只小鸟的位置

    private int starsNum = 0;

    private void Awake()
    {
        _instance = this;
        originPos = birds[0].transform.position;
    }
    private void Start()
    {
        Initiailized();
    }
    /*
     * 将小鸟初始化
     */
    private void Initiailized()
    {
        for(int i = 0; i < birds.Count; i++)
        {
            if (i == 0)
            {
                //第一只小鸟的脚本将其启用，并将其弹簧关节组件也启用
                //将原先第一只小鸟的位置赋值给现在的第一只小鸟，避免启用弹簧关节时物理效果过强
                birds[i].transform.position = originPos;
                birds[i].enabled = true;
                birds[i].sp.enabled = true;
                birds[i].canMove = true;
            }
            else
            {
                //其余的小鸟的脚本暂时不启用，弹簧关节也不启用
                birds[i].enabled = false;
                birds[i].sp.enabled = false;
                birds[i].canMove = false;
            }
        }
    }


    //判断游戏逻辑
    public void NextBird()
    {
        if(pigs.Count > 0)
        {
            if(birds.Count > 0)
            {
                //猪和小鸟都活着，游戏继续
                Initiailized();
            }
            else
            {
                //猪活着，小鸟没了，游戏结束
                lose.SetActive(true);
            }
        }
        else
        {

            //猪死完了着，游戏胜利
            win.SetActive(true);
        }
    }
/*
 * 游戏胜利时，星星的显示
 */
    public void ShowStars()
    {
        StartCoroutine("show");//开启一个协程方法
    }
    IEnumerator show()//协程，unity协程是一个能暂停执行，暂停后立即返回，直到中断指令完成后继续执行的函数。
    {
        for (; starsNum <= birds.Count; starsNum++)
        {
            if (starsNum >= stars.Length)
            {
                break;
            }
            yield return new WaitForSeconds(0.2f);
            stars[starsNum].SetActive(true);
        }
    }

    public void Replay()
    {
        SaveData();
        //返回当前关卡
        SceneManager.LoadScene(2);
    }
    public void Home()
    {
        SaveData();
        //返回主菜单
        SceneManager.LoadScene(1);
    }


    public void SaveData()
    {
        if (starsNum > PlayerPrefs.GetInt(PlayerPrefs.GetString("nowlevel"), 0))
        {
            PlayerPrefs.SetInt("totalStarsNum", PlayerPrefs.GetInt("totalStarsNum", 0) + starsNum - PlayerPrefs.GetInt(PlayerPrefs.GetString("nowlevel")));
            PlayerPrefs.SetInt(PlayerPrefs.GetString("nowlevel"), starsNum);//获取关卡名用于储存当前对应的星星数量
            
            print(PlayerPrefs.GetInt("totalStarsNum"));
        }
    }
}
