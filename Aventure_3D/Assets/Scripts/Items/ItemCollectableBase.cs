using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{

    public class ItemCollectableBase : MonoBehaviour
    {
        public ItemType itemType;

        public string compareTag = "Player";
        //public ParticleSystem particleSystem;
        public new ParticleSystem particleSystem;
        public float timeToHide = 3;
        public GameObject graphicItem;

        public new Collider collider;

        [Header("Sounds")]
        public AudioSource audioSource;

        private void Awake()
        {
            //if (particleSystem != null) particleSystem.transform.SetParent(null);
        }

        private void OnTriggerEnter(Collider collision)
        {
            if(collision.transform.CompareTag(compareTag))
            {
                Collect();
            }
        }

        protected virtual void HideItens()
        {
            //Debug.Log("Collect");
            if (collider != null) collider.enabled = false;

            if (graphicItem != null) graphicItem.SetActive(false);
            Invoke("HideObject", timeToHide);

        }

        protected virtual void Collect()
        {
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