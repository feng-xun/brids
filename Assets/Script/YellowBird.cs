using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowBird : Bird//继承bird类
{
/*
    重写bird类的showkill方法
 */
//飞行速度加快
    public override void Showkill()
    {
        base.Showkill();
        rg.velocity *= 1.5f;

    }
}
