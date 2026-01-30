using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [SerializeField]
    PlayerInput input;
    new  Rigidbody2D rigidbody2D;
    [SerializeField] private float moveSpeed = 10f;
    
    [Header("Parry")]
    [SerializeField] private GameObject parryArea;
    [SerializeField] private float parryTime = 1f;
    [SerializeField] private float parryDamage;
    private WaitForSeconds parryWait;
    

    [Header("UseMask")] 
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
        rigidbody2D.velocity = moveInput * moveSpeed;
    }

    void StopMove()
    {
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
    void UseMask()
    {
        StartCoroutine(UseMaskCoroutine());
    }

    IEnumerator UseMaskCoroutine()
    {
        slashArea.SetActive(true);
        yield return invincibleWait;
        slashArea.SetActive(false);
    }

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D.gravityScale = 0f;
        parryWait = new WaitForSeconds(parryTime);
        invincibleWait = new WaitForSeconds(invincibleTime);
        input.EnableGameplayInput();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
