using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using SKCell;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public static bool end1 = false;
    public static bool end2 = false;
    public static bool end3 = false;
    [SerializeField]private Player player;
    [SerializeField]private float _lowSanValue;
    public Camera mainCamera;  // TODO:修改主相机!!
    [Header("EscapeTime")]
    public float escapeTime = 240f;
    
    private Coroutine _escapeCoroutine;
    private bool _isEscaping;
    [Header("Parry")] 
    [SerializeField] private float parryStopDuration = 0.2f;
    private Coroutine _timeStopCoroutine;

    private void OnEnable()
    {
        player.OnUseMaskChanged += HandelUseMaskChanged;//订阅玩家使用面具事件
        player.OnParrySuccess += OnPlayerParrySuccess;
    }
    private void OnDisable()
    {
        player.OnUseMaskChanged -= HandelUseMaskChanged;//取消玩家使用面具事件
        player.OnParrySuccess -= OnPlayerParrySuccess;
    }

    private void Awake()
    {
        player = FindObjectOfType<Player>();
        _lowSanValue = player.maxSanValue * 0.2f;
    }
    
    /// <summary>
    /// 低san状态检测
    /// </summary>
    public void OnPlayerLowSan() 
    { 
        if (player.currentSanValue <= _lowSanValue) 
        { 
            //画面模糊/残影
            ////镜头晃动
        }
    }

    /// <summary>
    /// 通关检测
    /// </summary>
    public void LevelCompletedOrNot(bool end)
    {
        if (end)
        {
            //弹出结局
            //解锁结局
        }
        else
        {
            //弹出结局
            //解锁结局
        }
    }
    
    private void HandelUseMaskChanged(bool usemask)
    {
        if (usemask)
        {
          //视野遮挡
          //变红
          //面具ui变化  
        }
        
    }
    
    #region Escape
    //开始逃离倒计时
    public void StartEscapeCountdown()
    {
        if (_isEscaping) return;

        _isEscaping = true;
        _escapeCoroutine = StartCoroutine(EscapeCountdown());
    }

    // 取消逃离（比如成功逃出）
    public void StopEscapeCountdown()
    {
        if (!_isEscaping) return;

        _isEscaping = false;
        StopCoroutine(_escapeCoroutine);
        _escapeCoroutine = null;
    }

    private IEnumerator EscapeCountdown()
    {
        float remainingTime = escapeTime;

        while (remainingTime > 0f)
        {
            // 发事件 / 更新 UI

            yield return null;
            remainingTime -= Time.deltaTime;
        }

        OnEscapeFailed();
    }

    private void OnEscapeFailed()
    {
        Debug.Log("逃离失败");
        // Game Over / Reload / 结算
    }
    #endregion 
    
    #region Parry
    private void OnPlayerParrySuccess()
    {
        StartTimeStop(parryStopDuration);
    }

    public void StartTimeStop(float duration)
    {
        if (_timeStopCoroutine != null)
            StopCoroutine(_timeStopCoroutine);

        _timeStopCoroutine = StartCoroutine(TimeStopCoroutine(duration));
    }

    private IEnumerator TimeStopCoroutine(float duration)
    {
        float originalTimeScale = Time.timeScale;
        Time.timeScale = 0f;

        yield return new WaitForSecondsRealtime(duration);

        Time.timeScale = originalTimeScale;
        _timeStopCoroutine = null;
    }
    #endregion
    
    public void ScreenBlurred()
    {
        
    }
    
}
