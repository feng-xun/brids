using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausePanel : MonoBehaviour
{
    private Animator animator;//��ȡ����״̬��

    public GameObject pauseButton;//��ͣ��ť

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public void Retry()
    {
        Time.timeScale = 1;
        GameManager._instance.Replay();
    }
    public void Home()
    {
        Time.timeScale = 1;
        GameManager._instance.Home();
    }
    /*
        ���pause��ť�ᵯ����ͣ����
     */

    public void Pause()
    {
        //��һ�������ŵ�������
        animator.SetBool("isPause", true);//���ö���״̬����isPause������boolֵ��ʹ�õ�����������
        //�ڶ�������ͣ��ť����
        pauseButton.SetActive(false);
        
    }
    public void Resume()
    {
        //��һ�����ָ����������ٶ�
        Time.timeScale = 1;
        //�ڶ������������ض���
        animator.SetBool("isPause", false);
        
    }

    public void pauseAnimEnd()
    {
        Time.timeScale = 0;//ʱ�������ٶȵ����ű���,1Ϊ�����ٶȣ�0Ϊ��ͣ
    }
    public void ResumeAnimEnd()
    {
        
        //��ͣ��ť��ʾ
        pauseButton.SetActive(true);
    }
}
