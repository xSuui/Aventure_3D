using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Ebac.Core.Singleton;
using Cloth;

public class Player : Singleton<Player> //, IDamageable
{
    public List<Collider> colliders;
    public Animator animator;

    public CharacterController characterController;
    public float speed = 1f;
    public float turnSpeed = 1f;
    public float gravity = -9.8f;
    public float jumpSpeed = 15f;

    private float vSpeed = 0f;

    public KeyCode jumpKeycode = KeyCode.Space;

    [Header("Run Setup")]
    public KeyCode keyRun = KeyCode.LeftShift;
    public float speedRun = 1.5f;

    [Header("Flash")]
    public List<FlashColor> flashColors;


    [Header("Life")]
    public HealthBase healthBase;
    public UIFillUpdater uiFillUpdater;

    private bool _alive = true;

    private ShakeCamera shakeCamera;

    [Space]
    [SerializeField] private ClothChanger _clothChanger;

    public void OnValidate()
    {
        if (healthBase == null) healthBase = GetComponent<HealthBase>();

        /*if (healthBase == null)
        {
            healthBase = GetComponent<HealthBase>();
            if (healthBase == null)
                Debug.LogError("HealthBase component not found on the same GameObject or parent.");
        }*/

    }

    protected override void Awake()
    {
        base.Awake();
        OnValidate();

        shakeCamera = ShakeCamera.Instance;

        healthBase.OnDamage += Damage;
        healthBase.OnKill += OnKill;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            // Desative temporariamente a gravidade
            characterController.detectCollisions = false;
            Invoke("EnableGravity", 0.5f);  // Ative a gravidade após meio segundo (ajuste conforme necessário)
        }
    }


    private void EnableGravity()
    {
        characterController.detectCollisions = true;
    }


    #region Life
    private void OnKill(HealthBase h)
    {
        if(_alive)
        {
            _alive = false;
            animator.SetTrigger("Death");
            colliders.ForEach(i => i.enabled = false);

            Invoke(nameof(Revive), 3f);
        }
    }

    private void Revive()
    {
        _alive = true;
        healthBase.ResetLife();
        animator.SetTrigger("Revive");
        Respawn();
        Invoke(nameof(TurnOnColliders), .1f);
    }

    private void TurnOnColliders()
    {
        colliders.ForEach(i => i.enabled = true);
    }

    public void Damage(HealthBase h)
    {
        /*if (healthBase == null)
        {
            Debug.LogError("healthBase is null in Damage method");
            return;
        }

        flashColors.ForEach(i => i.Flash());
        EffectsManager.Instance.ChangeVignette();

        shakeCamera.Shake();*/


        flashColors.ForEach(i => i.Flash());
        EffectsManager.Instance.ChangeVignette();

        //shakeCamera.Shake();
    }

    public void Damage(float damage, Vector3 dir)
    {
        //Damage(damage);
    }
    #endregion

    #region teste
    void Update()
    {
        transform.Rotate(0, Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime, 0);

        var inputAxisVertical = Input.GetAxis("Vertical");
        var speedVector = transform.forward * inputAxisVertical * speed;


        if (characterController.isGrounded)
        {
            vSpeed = 0;
            if (Input.GetKeyDown(jumpKeycode))
            {
                vSpeed = jumpSpeed;
            }
        }


        vSpeed -= gravity * Time.deltaTime;
        speedVector.y = vSpeed;

        var isWalking = inputAxisVertical != 0;
        if (isWalking)
        {
            if (Input.GetKey(keyRun))
            {
                speedVector *= speedRun;
                animator.speed = speedRun;
            }
            else
            {
                animator.speed = 1;
            }
        }

        characterController.Move(speedVector * Time.deltaTime);
         
        animator.SetBool("Run", inputAxisVertical != 0);

    }
    #endregion 

    [NaughtyAttributes.Button]
    public void Respawn()
    {
        if(CheckpointManager.Instance.HasCheckpoint())
        {
            transform.position = CheckpointManager.Instance.GetPositionFromLastCheckpoint();
        }
    }

    public void ChangeSpeed(float speed, float duration)
    {
        StartCoroutine(ChangeSpeedCoroutine(speed, duration));
    }

    IEnumerator ChangeSpeedCoroutine(float localSpeed, float duration)
    {
        var defaultSpeed = speed;
        speed = localSpeed;
        yield return new WaitForSeconds(duration);
        speed = defaultSpeed;
    }

    public void ChangeTexture(ClothSetup setup, float duration)
    {
        StartCoroutine(ChangeTextureCoroutine(setup, duration));
    }

    IEnumerator ChangeTextureCoroutine(ClothSetup setup, float duration)
    {
        _clothChanger.ChangeTexture(setup);
        yield return new WaitForSeconds(duration);
        _clothChanger.ResetTexture();
        
    }

}


#region code aula
/*if (inputAxisVertical != 0)
{
    animator.SetBool("Run", true);
}
else
{
    animator.SetBool("Run", false);
}*/
#endregion