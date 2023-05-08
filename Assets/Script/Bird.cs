using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{

    private bool isClick = false;//用于检测鼠标抬起或按下啊
    [HideInInspector]
    public SpringJoint2D sp;//弹簧过节组件
    protected Rigidbody2D rg;//刚体组件
    private TrailRenderer trail;//追踪渲染，使用他来制作拖尾特效
    [HideInInspector]
    public bool canMove = false;//判断小鸟是否能被移动
    private float posX;
    private bool isFly = false;
    protected SpriteRenderer render;
    
    public Transform rightPos;//右树枝上的点
    public Transform leftPos;//左树枝上的点
    public float maxDis = 1;//限制拖拽的距离
    public LineRenderer left; //用于画线的组件
    public LineRenderer right;
    public GameObject boom;
    public Sprite hurtSprite;//受伤更换精灵图片

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

    //在本物体有鼠标按下时运行
    private void OnMouseDown()
    {
        if (canMove)
        {
            AudioPlay(select);
            isClick = true;
            rg.isKinematic = true;
        }
    }
    //在本物体有鼠标抬起时运行
    private void OnMouseUp()
    {
        if (canMove)
        {
            isClick = false;
            rg.isKinematic = false;
            Invoke("Fly", 0.1f);
            //禁用画线组件
            right.enabled = false;
            left.enabled = false;
            canMove = false;
        }
        
    }

    private void Update()
    {
        if (isClick)//当鼠标按下时
        {
            //获取鼠标的坐标位置，并将其转为主摄像机的世界坐标，然后把值赋予本物体
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //transform.position += new Vector3(0, 0, 10); 两种方法将本物体的z轴设为0
            transform.position += new Vector3(0, 0, -Camera.main.transform.position.z);
            if(Vector3.Distance(transform.position,rightPos.position) > maxDis)//进行位置限定
            {
                //两个坐标运算求出一个向量，将向量单位化，使向量保持相同的方向，但其长度为1.0。
                Vector3 pos = (transform.position - rightPos.position).normalized;
                pos *= maxDis;//用确定好的向量乘以限制拖拽的距离，求出最大距离的向量
                transform.position = pos + rightPos.position;//把自身坐标改为限制拖拽的标点加上最大距离的向量
            }
            Line();
        }


        /*
            摄像机随小鸟飞行移动
             Vector3.Lerp（起始值，结束值，插值）计算两向量的插值，实现平滑移动
            Mathf.Clamp(值，最小值，最大值)，但输入的值小于最小值取最小值，大于最大值取最大值，否则取本身的值
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
        sp.enabled = false; //组件禁用
        trail.enabled = true;
        Invoke("Next", 3);//3s之后调用Next方法
    }
    /*画线操作
     * 用来表现弹弓的皮筋
     *  右树枝上的点和小鸟的坐标连成一条线
     *  左树枝上的点和小鸟的坐标连成一条线
     */
    void Line()
    {
        right.enabled = true;
        left.enabled = true;
        //SetPosition(0, rightPos.position);两点确立一条线，0表示第一个点，然后输入该点的坐标
        right.SetPosition(0, rightPos.position);
        right.SetPosition(1, transform.position);

        left.SetPosition(0, leftPos.position);
        left.SetPosition(1, transform.position);
    }

    protected virtual void Next()
    {
        GameManager._instance.birds.Remove(this);//从小鸟的数组列表中删除自身
        Destroy(gameObject);//从游戏中删除自身
        Instantiate(boom, transform.position, Quaternion.identity);//释放爆炸动画
        GameManager._instance.NextBird();
    }

    public void AudioPlay(AudioClip clip)
    {
        AudioSource.PlayClipAtPoint(clip,transform.position);
    }

    /*
        小鸟的特殊能力
        在飞行过程中能释放一次，释放完将ifFly改为false；
     */
    public virtual void Showkill()
    {
        isFly = false;
    }

}
