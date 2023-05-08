using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{

    private bool isClick = false;//���ڼ�����̧����°�
    [HideInInspector]
    public SpringJoint2D sp;//���ɹ������
    protected Rigidbody2D rg;//�������
    private TrailRenderer trail;//׷����Ⱦ��ʹ������������β��Ч
    [HideInInspector]
    public bool canMove = false;//�ж�С���Ƿ��ܱ��ƶ�
    private float posX;
    private bool isFly = false;
    protected SpriteRenderer render;
    
    public Transform rightPos;//����֦�ϵĵ�
    public Transform leftPos;//����֦�ϵĵ�
    public float maxDis = 1;//������ק�ľ���
    public LineRenderer left; //���ڻ��ߵ����
    public LineRenderer right;
    public GameObject boom;
    public Sprite hurtSprite;//���˸�������ͼƬ

    public AudioClip select;
    public AudioClip fly;

    private void Awake()
    {
        sp = GetComponent<SpringJoint2D>();
        rg = GetComponent<Rigidbody2D>();
        trail = GetComponent<TrailRenderer>();
        trail.enabled = false;
        render = GetComponent<SpriteRenderer>();
    }

    //�ڱ���������갴��ʱ����
    private void OnMouseDown()
    {
        if (canMove)
        {
            AudioPlay(select);
            isClick = true;
            rg.isKinematic = true;
        }
    }
    //�ڱ����������̧��ʱ����
    private void OnMouseUp()
    {
        if (canMove)
        {
            isClick = false;
            rg.isKinematic = false;
            Invoke("Fly", 0.1f);
            //���û������
            right.enabled = false;
            left.enabled = false;
            canMove = false;
        }
        
    }

    private void Update()
    {
        if (isClick)//����갴��ʱ
        {
            //��ȡ��������λ�ã�������תΪ����������������꣬Ȼ���ֵ���豾����
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //transform.position += new Vector3(0, 0, 10); ���ַ������������z����Ϊ0
            transform.position += new Vector3(0, 0, -Camera.main.transform.position.z);
            if(Vector3.Distance(transform.position,rightPos.position) > maxDis)//����λ���޶�
            {
                //���������������һ����������������λ����ʹ����������ͬ�ķ��򣬵��䳤��Ϊ1.0��
                Vector3 pos = (transform.position - rightPos.position).normalized;
                pos *= maxDis;//��ȷ���õ���������������ק�ľ��룬��������������
                transform.position = pos + rightPos.position;//�����������Ϊ������ק�ı����������������
            }
            Line();
        }


        /*
            �������С������ƶ�
             Vector3.Lerp����ʼֵ������ֵ����ֵ�������������Ĳ�ֵ��ʵ��ƽ���ƶ�
            Mathf.Clamp(ֵ����Сֵ�����ֵ)���������ֵС����Сֵȡ��Сֵ���������ֵȡ���ֵ������ȡ�����ֵ
        */
        posX = transform.position.x;
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position,
            new Vector3(Mathf.Clamp(posX,0,15), Camera.main.transform.position.y, Camera.main.transform.position.z),
                2.5f * Time.deltaTime);


        if (isFly)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Showkill();
            }
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isFly = false;
        if(collision.gameObject.tag == "Enemy")
        {
            render.sprite = hurtSprite;
        }
        

    }

    protected virtual void Fly()
    {
        isFly = true;
        AudioPlay(fly);
        sp.enabled = false; //�������
        trail.enabled = true;
        Invoke("Next", 3);//3s֮�����Next����
    }
    /*���߲���
     * �������ֵ�����Ƥ��
     *  ����֦�ϵĵ��С�����������һ����
     *  ����֦�ϵĵ��С�����������һ����
     */
    void Line()
    {
        right.enabled = true;
        left.enabled = true;
        //SetPosition(0, rightPos.position);����ȷ��һ���ߣ�0��ʾ��һ���㣬Ȼ������õ������
        right.SetPosition(0, rightPos.position);
        right.SetPosition(1, transform.position);

        left.SetPosition(0, leftPos.position);
        left.SetPosition(1, transform.position);
    }

    protected virtual void Next()
    {
        GameManager._instance.birds.Remove(this);//��С��������б���ɾ������
        Destroy(gameObject);//����Ϸ��ɾ������
        Instantiate(boom, transform.position, Quaternion.identity);//�ͷű�ը����
        GameManager._instance.NextBird();
    }

    public void AudioPlay(AudioClip clip)
    {
        AudioSource.PlayClipAtPoint(clip,transform.position);
    }

    /*
        С�����������
        �ڷ��й��������ͷ�һ�Σ��ͷ��꽫ifFly��Ϊfalse��
     */
    public virtual void Showkill()
    {
        isFly = false;
    }

}
