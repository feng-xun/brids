using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackBird : Bird
{
    private List<Pig> blocks = new List<Pig>();
    //进入触发器里面,添加脚本进入blocks集合
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            blocks.Add(collision.gameObject.GetComponent<Pig>());
        }
    }
    //离开触发器范围内，将该脚本从blocks集合中删除
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            blocks.Remove(collision.gameObject.GetComponent<Pig>());           
        }
    }
 
    public override void Showkill()
    {
        base.Showkill();
        if(blocks.Count > 0 && blocks != null)
        {
            for(int i = 0;i < blocks.Count;)
            {
                blocks[i].Dead();
      
            }
        }
        OnClear();
    }
    void OnClear()
    {
        rg.velocity = Vector3.zero;
        Instantiate(boom,transform.position,Quaternion.identity);
        render.enabled = false;
        GetComponent<CircleCollider2D>().enabled = false;
        GetComponent<TrailRenderer>().enabled = false;
    }

    protected override void Next()
    {
        GameManager._instance.birds.Remove(this);//从小鸟的数组列表中删除自身
        Destroy(gameObject);//从游戏中删除自身
        GameManager._instance.NextBird();
    }
}
