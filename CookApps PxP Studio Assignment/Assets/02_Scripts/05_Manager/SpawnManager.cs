using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monster = CookAppsPxPAssignment.Character.Monster.Monster;


namespace CookAppsPxPAssignment.Manager
{
    public class SpawnManager : MonoBehaviour
    {
        private static SpawnManager instance;
        public static SpawnManager Instance
        {
            get
            {
                return instance;
            }
        }

        public GameObject Prefab;
        private Monster[] MonsterPool;

        public int MaximumSpawnAmount = 5;
        public int curSpawnAmount = 0;

        public float SpawnTimer = 5;

        private float _curTimer;

        private void Awake()
        {
            if (instance != null)
            {
                Destroy(this);
                return;
            }
            instance = this;
            MonsterPool = new Monster[MaximumSpawnAmount];

            SetMonsterPool();
        }


        private void Start()
        {

        }

        private void Update()
        {
            _curTimer += Time.deltaTime;
            // 생성량 체크도 해야함
            if(_curTimer > SpawnTimer && curSpawnAmount < MaximumSpawnAmount)
            {
                // 스폰
                SpawnMonster();
                _curTimer = 0;
            }
        }

        private void SetMonsterPool()
        {
            for (int i = 0; i < MaximumSpawnAmount; i++)
            {
                //MonsterPools.Add(Instantiate(Prefab), false);
                MonsterPool[i] = Instantiate(Prefab.GetComponent<Monster>());
                MonsterPool[i].gameObject.SetActive(false);
            }
        }
        private void SpawnMonster()
        {
            foreach (var monster in MonsterPool)
            {
                if (!monster.gameObject.activeSelf)
                {
                    monster.SetHealthPoint();
                    monster.gameObject.SetActive(true);
                    return;
                }
            }
        }
    }
}
