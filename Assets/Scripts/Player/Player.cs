using System;
using System.Collections;
using SKCell;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : SKMonoSingleton<Player>
{
    [SerializeField]
    PlayerInput input;
    new  Rigidbody2D rigidbody2D;
    [SerializeField] private float moveSpeed = 10f;

    [Header("SanValue")] 
    public float maxSanValue = 100f;

    public float currentSanValue;
    [SerializeField] private float sanCost = 60f;//san值消耗
    [SerializeField] private float recoverSan = 10f;//san值恢复/弹反
    
    [Header("Parry")] 
    [SerializeField] private GameObject parryArea;
    [SerializeField] private float parryTime = 1f;
    [SerializeField] private float parryDamage;
    private WaitForSeconds parryWait;
    public event Action OnParrySuccess;
    

    [Header("UseMask")] 
    private bool _useMask = false;
    public event Action<bool> OnUseMaskChanged;

    // [SerializeField] private GameObject UIRoot;
    private Animator anim;
    public bool blUseMask
    {
        get => _useMask;
        set
        {
            if(_useMask == value) return;
            
            _useMask = value;
            OnUseMaskChanged?.Invoke(_useMask);
        }
    }
    
    [SerializeField] private float invincibleTime = 1f;
    [SerializeField] private GameObject slashArea;
    [SerializeField] private float velocityIncreaseValue;
    
    private WaitForSeconds invincibleWait;
    
    private void OnEnable()
    {
        input.onMove += Move;
        input.onStopMove += StopMove;
        input.onParry += Parry;
        input.onUseMask += UseMask;
    }

    private void OnDisable()
    {
        input.onMove -= Move;
        input.onStopMove -= StopMove;
        input.onParry -= Parry;
        input.onUseMask -= UseMask;
    }

    void Move(Vector2 moveInput)
    {
        if (moveInput.x < 0)
        {
            anim.SetBool("isLeft", true);
            anim.SetBool("isRight", false);
        }
        else
        {
            anim.SetBool("isRight", true);
            anim.SetBool("isLeft", false);
        }
        rigidbody2D.velocity = moveInput * moveSpeed;
    }

    void StopMove()
    {
        anim.SetBool("isLeft", false);
        anim.SetBool("isRight", false);
        rigidbody2D.velocity = Vector2.zero;
    }

    void Parry()
    {
        StartCoroutine(parryCoroutine());
    }

    IEnumerator parryCoroutine()
    {
        parryArea.SetActive(true);
        yield return parryWait;
        parryArea.SetActive(false);
    }

    public void HandelParrySuccess()//TODO:弹反成功，此函数未被调用
    {
        OnParrySuccess?.Invoke();
    }
    
    void UseMask()
    {
        StartCoroutine(UseMaskCoroutine());
    }

    IEnumerator UseMaskCoroutine()
    {
        _useMask = true;
        slashArea.SetActive(true);
        yield return invincibleWait;
        slashArea.SetActive(false);
        _useMask = false;
    }

    protected override void Awake()
    {
        base.Awake();
        currentSanValue = maxSanValue;//初始化san值
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D.gravityScale = 0f;
        anim = GetComponent<Animator>();
        StartCoroutine(GameStartCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator GameStartCoroutine()
    {
        while (transform.position.x < 0)
        {
            Move(Vector2.right);
            yield return null;
        }
        StopMove();
        Init();
    }
    void Init()
    {
        parryWait = new WaitForSeconds(parryTime);
        invincibleWait = new WaitForSeconds(invincibleTime);
        Camera.main.gameObject.transform.SetParent(transform);
        input.EnableGameplayInput();
        // UIRoot.SetActive(true);
    }
    
    public float GetCurrentSanValue() => currentSanValue;
    public void changeUIMap() => input.EnableUIInput();

    public void ChangeGamePlayMap() => input.EnableGameplayInput();
    public void DisableAllInput() => input.DisableAllInputs();
}
