using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee : Enemy
{
    protected override void Update()
    {
        base.Update();

        // 距离够近时执行近战攻击
        // Attack();
    }
}
