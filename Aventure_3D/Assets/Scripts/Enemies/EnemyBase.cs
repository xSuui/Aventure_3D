using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using Animation;

namespace Enemy
{ 
    public class EnemyBase : MonoBehaviour, IDamageable
    {
        public SFXType sFXType;
        public SFXType sFXType2;

        public Collider _collider;
        public FlashColor flashColor;
        public ParticleSystem _particleSystem;
        public float startLife = 10f;
        public bool lookAtPlayer = false;

        public AudioSource audioSource;

        [SerializeField] private float _currentLife;

        [Header("Animation")]
        [SerializeField] private AnimationBase _animationBase;

        [Header("Start Animation")]
        public float startAnimationDuration = .2f;
        public Ease starAnimationEase = Ease.OutBack;
        public bool startWithBornAnimation = true;

        [Header("Events")]
        public UnityEvent OnKillEvent;


        private Player _player;

        private void Awake()
        {
            Init();
        }

        private void Start()
        {
            _player = GameObject.FindObjectOfType<Player>();
        }

        private void PlaySFX()
        {
            SFXPool.Instance.Play(sFXType);
        }
        
        private void PlaySFX02()
        {
            SFXPool.Instance.Play(sFXType2);
        }

        protected void ResetLife()
        {
            _currentLife = startLife;
        }

        protected virtual void Init()
        {
            ResetLife();

            if(startWithBornAnimation)
                BornAnimation();
        }

        protected virtual void Kill()
        {
            PlaySFX();
            OnKill();
        }

        protected virtual void OnKill() 
        {
            if (_collider != null) _collider.enabled = false;
            Destroy(gameObject, 3f);
            PlayAnimationByTrigger(AnimationType.DEATH);
            OnKillEvent?.Invoke();
        }

        public void OnDamage(float f)
        {
            
            if (flashColor != null) flashColor.Flash();
            if (_particleSystem != null) _particleSystem.Emit(15);

            transform.position -= transform.forward;

            _currentLife -= f;
     
            if (_currentLife <= 0)
            {
                Kill();
            }
        }

        #region ANIMATIONS
        private void BornAnimation()
        {
            transform.DOScale(0, startAnimationDuration).SetEase(starAnimationEase).From();
        }

        public void PlayAnimationByTrigger(AnimationType animationType)
        {
            _animationBase.PlayAnimationByTrigger(animationType);
        }

        #endregion


        public void Damage(float damage)
        {
            Debug.Log("Damage");
            OnDamage(damage);
        }
        public void Damage(float damage, Vector3 dir)
        {
            OnDamage(damage);
            transform.DOMove(transform.position - dir, .1f);
        }

        private void OnCollisionEnter(Collision collision)
        {
            Player p = collision.transform.GetComponent<Player>();

            if(p != null)
            {
                p.healthBase.Damage(1);
            }
        }

        public virtual void Update()
        {
            if(lookAtPlayer)
            {
                transform.LookAt(_player.transform.position);
            }
        }

    }
}


/*//Debug
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                OnDamage(5f);
            }
        }*/