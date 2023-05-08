using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pig : MonoBehaviour
{
    public float maxSpeed = 10;
    public float minSpeed = 5;
    public Sprite hurt;//精灵图片
    public GameObject boom;
    public GameObject score;
    public bool isPig;
    public AudioClip hurtClip;
    public AudioClip dead;
    public AudioClip birdCollision;


    private SpriteRenderer render;//精灵渲染组件
    

    private void Awake()
    {
        render = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if (isPig)
            { 
            }
                AudioPlay(birdCollision);
        }
        //collision.relativeVelocity 两个碰撞对象的相对线性速度；magnitude 返回该向量的长度。
        //如果两个碰撞物体的相对速度大于设好的最大速度，则直接死亡
        //如果在最小速度与最大速度之间则受伤
        if (collision.relativeVelocity.magnitude > maxSpeed)
        {
            Dead();
        }
        else if (collision.relativeVelocity.magnitude > minSpeed)
        {
            //受伤时更换精灵图片
            render.sprite = hurt;
            if(isPig)
                AudioPlay(hurtClip);
        }
    }

    public void Dead()
    {
        if (isPig)
        {
            GameManager._instance.pigs.Remove(this);
            AudioPlay(dead);
        }
        Destroy(gameObject);
        //物体销毁时,在预制体中使用boom动画播放特效
        Instantiate(boom, transform.position, Quaternion.identity);
        //物体销毁时,在预制体中生成奖励分数效果，1.5s后删除
        GameObject go = Instantiate(score, transform.position + new Vector3(0,0.8f,0), Quaternion.identity);
        Destroy(go, 1.5f);
    }

    public void AudioPlay(AudioClip clip)
    {
        AudioSource.PlayClipAtPoint(clip, transform.position);
    }
}
