using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private Vector3 _moveDirection;
    private Vector2 _lookVec;
    private Vector2 _facingDirection;
    public float runningSpeed = 5f;
    [SerializeField] private Transform aim;
    [SerializeField] private new Camera camera;
    [SerializeField] private Transform bulletPrefab;
    [SerializeField] private float fireRate = 0.4f;
    [SerializeField] private float InvulnerabilityTime = 3f;
    [SerializeField] private Animator Anim;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float blinkRate = 0.01f;
    [SerializeField] private AudioClip impactClip;
    [SerializeField] private AudioClip itemClip;
    private CameraController camController;
    bool takeDamageCooldown;
    private bool gunLoaded = true;
    [SerializeField] private int health = 10;
    private bool powerShotEnabled;

    public int Health
    {
        get => health;
        set
        {
            health = value;
            GUIManager.sharedInstance.updateUIHealth(health);
        }
    }

    private void Awake()
    {
    }

    void Start()
    {
        GUIManager.sharedInstance.updateUIHealth(health);
        camController = FindObjectOfType<CameraController>();
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        //Movement Character
        transform.position += _moveDirection * (Time.deltaTime * runningSpeed);
        //Movement Aim
        aim.position = (Vector2)transform.position + _facingDirection.normalized;
        
        Anim.SetFloat("Speed", _moveDirection.magnitude);
        if (aim.position.x > transform.position.x)
        {
            spriteRenderer.flipX = true;
        }else if (aim.position.x < transform.position.x)
        {
            spriteRenderer.flipX = false;
        }
    }
    
    private void Animation(Vector2 moveDirection, InputAction.CallbackContext context){
            Anim.SetFloat("Speed", moveDirection.magnitude);
            if (aim.position.x > transform.position.x)
            {
                spriteRenderer.flipX = true;
            }else if (aim.position.x < transform.position.x)
            {
                spriteRenderer.flipX = false;
            }
    }
    
    public void Move(InputAction.CallbackContext context){
        _moveDirection = context.ReadValue<Vector2>();
        //Animation(_moveDirection, context);
    }

    public void LookStick(InputAction.CallbackContext context)
    {
        _lookVec = new Vector2(context.ReadValue<Vector2>().x, context.ReadValue<Vector2>().y);
        _facingDirection = _lookVec;
    }

    public void LookMouse(InputAction.CallbackContext context)
    {
        _lookVec = new Vector2(context.ReadValue<Vector2>().x, context.ReadValue<Vector2>().y);
        _lookVec = camera.ScreenToWorldPoint(_lookVec);
        _facingDirection = _lookVec - (Vector2)transform.position;
    }

    public void Fire(InputAction.CallbackContext context)
    {
        if (context.performed && gunLoaded)
        {
            gunLoaded = false;
            float angle = Mathf.Atan2(_facingDirection.y, _facingDirection.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
            Transform bulletClone = Instantiate(bulletPrefab, transform.position, targetRotation);
            if (powerShotEnabled)
            {
                bulletClone.GetComponent<Bullet>().powerShot = true;
            }
            StartCoroutine(ReloadGun());
        }
    }

    IEnumerator ReloadGun()
    {
        yield return new WaitForSeconds(1/fireRate);
        gunLoaded = true;
    }

    public void TakeDamage()
    {
        if (takeDamageCooldown)
            Health--;
        takeDamageCooldown = false;
        camController.Shake();
        AudioSource.PlayClipAtPoint(impactClip, transform.position);
        StartCoroutine(TakeDamageCooldown());

        if (Health <= 0)
        {
            GUIManager.sharedInstance.GameOver();
            //GameOver
        }
    }

    IEnumerator TakeDamageCooldown()
    {
        StartCoroutine(BlinkRoutine());
        yield return new WaitForSeconds(InvulnerabilityTime);
        takeDamageCooldown = true;
    }

    IEnumerator BlinkRoutine()
    {
        int t = 10;
        while (t > 0)
        {
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(t * blinkRate);
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(t * blinkRate);
            t--;
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PowerUp"))
        {
            AudioSource.PlayClipAtPoint(itemClip, transform.position);
            switch (other.GetComponent<PowerUp>().powerUpType)
            {
                case PowerUp.PowerUpType.FireRateIncrease:
                    //incrementar cadencia de disparo
                    fireRate++;
                    break;
                case PowerUp.PowerUpType.PowerShot:
                    //activar el power shot
                    powerShotEnabled = true;
                    break;
            }
            Destroy(other.gameObject, 0.1f);
        }
    }
}
