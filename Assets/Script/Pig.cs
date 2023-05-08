using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pig : MonoBehaviour
{
    public float maxSpeed = 10;
    public float minSpeed = 5;
    public Sprite hurt;//����ͼƬ
    public GameObject boom;
    public GameObject score;
    public bool isPig;
    public AudioClip hurtClip;
    public AudioClip dead;
    public AudioClip birdCollision;


    private SpriteRenderer render;//������Ⱦ���
    

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
        //collision.relativeVelocity ������ײ�������������ٶȣ�magnitude ���ظ������ĳ��ȡ�
        //���������ײ���������ٶȴ�����õ�����ٶȣ���ֱ������
        //�������С�ٶ�������ٶ�֮��������
        if (collision.relativeVelocity.magnitude > maxSpeed)
        {
            Dead();
        }
        else if (collision.relativeVelocity.magnitude > minSpeed)
        {
            //����ʱ��������ͼƬ
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
        //��������ʱ,��Ԥ������ʹ��boom����������Ч
        Instantiate(boom, transform.position, Quaternion.identity);
        //��������ʱ,��Ԥ���������ɽ�������Ч����1.5s��ɾ��
        GameObject go = Instantiate(score, transform.position + new Vector3(0,0.8f,0), Quaternion.identity);
        Destroy(go, 1.5f);
    }

    public void AudioPlay(AudioClip clip)
    {
        AudioSource.PlayClipAtPoint(clip, transform.position);
    }
}
