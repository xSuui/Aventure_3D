using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ebac.Core.Singleton;

namespace Itens
{
    public enum ItemType
    {
        COIN,
        LIFE_PACK
    }

    public class IitemManager : Singleton<IitemManager>
    {
        public List<ItemSetup> itemSetups;


        private void Start()
        {
            Reset();
        }

        private void Reset()
        {
            foreach(var i in itemSetups)
            {
                i.soInt.value = 0;
            }
        }

        public void AddType(ItemType itemType, int amount = 1)
        {
            if (amount < 0) return;

            itemSetups.Find(i => i.itemType == itemType).soInt.value += amount;
        }

        public void RemoveByType(ItemType itemType, int amount = -1)
        {
            if (amount > 0) return;

            var item = itemSetups.Find(i => i.itemType == itemType);
            item.soInt.value -= amount;

            if (item.soInt.value < 0) item.soInt.value = 0;
        }

        [NaughtyAttributes.Button]
        private void AddCoin()
        {
            AddType(ItemType.COIN);
        }
        
        [NaughtyAttributes.Button]
        private void AddLifePack()
        {
            AddType(ItemType.LIFE_PACK);
        }

        

        
    }

    [System.Serializable]
    public class ItemSetup
    {
        public ItemType itemType;
        public SOInt soInt;
    }

}