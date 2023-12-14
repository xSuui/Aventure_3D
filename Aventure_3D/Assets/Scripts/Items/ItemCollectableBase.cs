using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{

    public class ItemCollectableBase : MonoBehaviour
    {
        public SFXType sFXType;
        public ItemType itemType;

        public string compareTag = "Player";
        //public ParticleSystem particleSystem;
        public new ParticleSystem particleSystem;
        public float timeToHide = 3;
        public GameObject graphicItem;

        public Collider[] colliders;

        [Header("Sounds")]
        public AudioSource audioSource;

        private void Awake()
        {
            //if (particleSystem != null) particleSystem.transform.SetParent(null);
            colliders = GetComponents<Collider>();
        }

        private void OnTriggerEnter(Collider collision)
        {
            if(collision.transform.CompareTag(compareTag))
            {
                Collect();
            }
        }

        private void PlaySFX()
        {
            SFXPool.Instance.Play(sFXType);
        }

        protected virtual void HideItens()
        {
            if (colliders != null)
            {
                foreach(Collider collider in colliders)
                {
                    collider.enabled = false;
                }
            }

            //Debug.Log("Collect");
            //if (collider != null) collider.enabled = false;

            if (graphicItem != null) graphicItem.SetActive(false);
            Invoke("HideObject", timeToHide);

        }

        protected virtual void Collect()
        {
            PlaySFX();
            HideItens();
            OnCollect();
        }

        private void HideObject()
        {
            gameObject.SetActive(false);
        }

        protected virtual void OnCollect()
        {
            if (particleSystem != null)
            {
                particleSystem.transform.SetParent(null);
                particleSystem.Play();
            }
            if (audioSource != null) audioSource.Play();

            ItemManager.Instance.AddByType(itemType);
        }
    }

}