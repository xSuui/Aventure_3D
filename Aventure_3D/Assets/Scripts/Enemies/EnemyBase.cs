using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Enemy
{ 
    public class EnemyBase : MonoBehaviour
    {
        public float startLife = 10f;

        [SerializeField] private float _currentLife;

        [Header("Start Animation")]
        public float startAnimationDuration = .2f;
        public Ease starAnimationEase = Ease.OutBack;
        public bool startWithBornAnimation = true;

        private void Awake()
        {
            Init();
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
            OnKill();
        }

        protected virtual void OnKill() 
        {
            Destroy(gameObject);
        }

        public void OnDamage(float f)
        {
            _currentLife -= f;
            
            if(_currentLife <= 0)
            {
                Kill();
            }
        }

        #region ANIMATIONS
        private void BornAnimation()
        {
            transform.DOScale(0, startAnimationDuration).SetEase(starAnimationEase).From();
        }

        #endregion

        //Debug
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                OnDamage(5f);
            }
        }

    }
}