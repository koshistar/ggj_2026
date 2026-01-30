using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    /// <summary>
    /// 最大生命值
    /// </summary>
    protected int maxHealth = 100;
    /// <summary>
    /// 当前生命值
    /// </summary>
    protected int curruntHealth;
    /// <summary>
    /// 仇恨距离
    /// </summary>
    protected float hateDistance;
    /// <summary>
    /// 移动速度
    /// </summary>
    protected float moveSpeed = 6f;
    /// <summary>
    /// 攻击力
    /// </summary>
    private float attackValue = 25f;//攻击值
    /// <summary>
    /// 攻击距离
    /// </summary>
    protected float meleeAttackDistance = 1f;//近战
    protected float rangedAttackDistance = 5f;//远程
    protected float spikeAttackDistance = 2f; //突刺
    /// <summary>
    /// 攻击间隔
    /// </summary>
    protected float attackInterval = 1f;
    /// <summary>
    /// enemy动画
    /// </summary>
    protected Animator enemyAnim;

    private void Awake()
    {
        Init();
    }

    /// <summary>
    /// 初始化
    /// </summary>
    public void Init()
    {
        curruntHealth = maxHealth;
    }

    /// <summary>
    /// 游走
    /// </summary>
    protected void Roam()
    {
        
    }

    /// <summary>
    /// 索敌
    /// </summary>
    protected void PlayerDetection()
    {
        
    } 
    /// <summary>
    /// 攻击  
    /// </summary>
    protected void Attack()
    {
        
    }
    /// <summary>
    /// 改变生命值
    /// </summary>
    /// <param name="damage"></param>
    public void ApplyHealthChange(int damage)
    {
        curruntHealth -= damage;
        if (curruntHealth <= 0)
        {
            Die();
        }
    }

    /// <summary>
    /// 改变速度
    /// </summary>
    public void ApplySpeedChange()
    {
        
    }
    
    /// <summary>
    /// 死亡
    /// </summary>
    public void Die()
    {
        Destroy(gameObject);
    }
}
