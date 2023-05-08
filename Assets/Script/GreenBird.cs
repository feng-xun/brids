using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenBird : Bird
{

    //·ÉÐÐ»ØÐý
    public override void Showkill()
    {
        base.Showkill();
        Vector3 speed = rg.velocity;
        speed.x *= -0.5f;
        rg.velocity = speed;
    }
}
