using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausePanel : MonoBehaviour
{
    private Animator animator;//获取动画状态机

    public GameObject pauseButton;//暂停按钮

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
        点击pause按钮会弹出暂停界面
     */

    public void Pause()
    {
        //第一步，播放弹出动画
        animator.SetBool("isPause", true);//设置动画状态机中isPause参数的bool值，使得弹出动画播放
        //第二步，暂停按钮隐藏
        pauseButton.SetActive(false);
        
    }
    public void Resume()
    {
        //第一步，恢复正常播放速度
        Time.timeScale = 1;
        //第二步，播放隐藏动画
        animator.SetBool("isPause", false);
        
    }

    public void pauseAnimEnd()
    {
        Time.timeScale = 0;//时间流逝速度的缩放比例,1为正常速度，0为暂停
    }
    public void ResumeAnimEnd()
    {
        
        //暂停按钮显示
        pauseButton.SetActive(true);
    }
}
