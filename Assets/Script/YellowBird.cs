using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowBird : Bird//�̳�bird��
{
/*
    ��дbird���showkill����
 */
//�����ٶȼӿ�
    public override void Showkill()
    {
        base.Showkill();
        rg.velocity *= 1.5f;

    }
}
