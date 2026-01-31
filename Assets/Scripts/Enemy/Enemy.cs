using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

public abstract class Enemy : MonoBehaviour
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
    protected float moveSpeed = 3f;
    protected float stopDistance = 1.5f;
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

    [Header("Spawn")]
    [SerializeField] private float spawnMinRadius = 5f;
    [SerializeField] private float spawnMaxRadius = 10f;
    
    [Header("Collider")]
    public float minDistance = 8f;      // 最小间距
    public float deadZone = 0.05f;        // 死区（防抖）
    public float separationStrength = 6f; // 分离强度
    
    protected bool isActive;
    protected virtual void Awake()
    {
        Init();
    }

    /// <summary>
    /// 初始化
    /// </summary>
    public virtual void Init()
    {
        curruntHealth = maxHealth;
    }

    
    public virtual void OnSpawn()
    {
        isActive = true;
        gameObject.SetActive(true);
    }
    
    public virtual void OnDespawn()
    {
        isActive = false;
        gameObject.SetActive(false);
    }
    
    protected virtual void Update()
    {
        PlayerDetection();
    }

    private void LateUpdate()
    {
        EnemyCollider();
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
        if (!isActive || Player.instance == null) return;

        float distance = Vector2.Distance(transform.position, Player.instance.transform.position);

        if (distance <= stopDistance)
            return; // 距离够近，停止移动

        Vector2 dir = (Player.instance.transform.position - transform.position).normalized;
        transform.position += (Vector3)(dir * moveSpeed * Time.deltaTime);
    }

    public void EnemyCollider()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(
            transform.position,
            minDistance
        );

        Vector2 separation = Vector2.zero;
        int count = 0;

        foreach (var hit in hits)
        {
            if (!hit.CompareTag("Enemy")) continue;
            if (hit.gameObject == gameObject) continue;

            Vector2 diff = (Vector2)transform.position - (Vector2)hit.transform.position;
            float dist = diff.magnitude;

            if (dist <= 0.0001f) continue;

            // 死区判断（防止来回抖）
            if (dist > minDistance - deadZone) continue;

            separation += diff.normalized * (minDistance - dist);
            count++;
        }

        if (count > 0)
        {
            Vector2 move = separation / count;
            transform.position += (Vector3)(move * separationStrength * Time.deltaTime);
        }
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
