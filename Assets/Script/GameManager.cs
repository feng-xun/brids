using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public List<Bird> birds;  //��ȡ�ű�bird�༯��
    public List<Pig> pigs;//��ȡ�ű�pig�༯��
    public GameObject[] stars; //��ȡʤ��������������
    public static GameManager _instance;//��̬����������ģʽ
    public GameObject lose;
    public GameObject win;

    private Vector3 originPos;//��һֻС���λ��

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
     * ��С���ʼ��
     */
    private void Initiailized()
    {
        for(int i = 0; i < birds.Count; i++)
        {
            if (i == 0)
            {
                //��һֻС��Ľű��������ã������䵯�ɹؽ����Ҳ����
                //��ԭ�ȵ�һֻС���λ�ø�ֵ�����ڵĵ�һֻС�񣬱������õ��ɹؽ�ʱ����Ч����ǿ
                birds[i].transform.position = originPos;
                birds[i].enabled = true;
                birds[i].sp.enabled = true;
                birds[i].canMove = true;
            }
            else
            {
                //�����С��Ľű���ʱ�����ã����ɹؽ�Ҳ������
                birds[i].enabled = false;
                birds[i].sp.enabled = false;
                birds[i].canMove = false;
            }
        }
    }


    //�ж���Ϸ�߼�
    public void NextBird()
    {
        if(pigs.Count > 0)
        {
            if(birds.Count > 0)
            {
                //���С�񶼻��ţ���Ϸ����
                Initiailized();
            }
            else
            {
                //����ţ�С��û�ˣ���Ϸ����
                lose.SetActive(true);
            }
        }
        else
        {

            //���������ţ���Ϸʤ��
            win.SetActive(true);
        }
    }
/*
 * ��Ϸʤ��ʱ�����ǵ���ʾ
 */
    public void ShowStars()
    {
        StartCoroutine("show");//����һ��Э�̷���
    }
    IEnumerator show()//Э�̣�unityЭ����һ������ִͣ�У���ͣ���������أ�ֱ���ж�ָ����ɺ����ִ�еĺ�����
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
        //���ص�ǰ�ؿ�
        SceneManager.LoadScene(2);
    }
    public void Home()
    {
        SaveData();
        //�������˵�
        SceneManager.LoadScene(1);
    }


    public void SaveData()
    {
        if (starsNum > PlayerPrefs.GetInt(PlayerPrefs.GetString("nowlevel"), 0))
        {
            PlayerPrefs.SetInt("totalStarsNum", PlayerPrefs.GetInt("totalStarsNum", 0) + starsNum - PlayerPrefs.GetInt(PlayerPrefs.GetString("nowlevel")));
            PlayerPrefs.SetInt(PlayerPrefs.GetString("nowlevel"), starsNum);//��ȡ�ؿ������ڴ��浱ǰ��Ӧ����������
            
            print(PlayerPrefs.GetInt("totalStarsNum"));
        }
    }
}
