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
        [SerializeField] private Transform[] _playableTransform;

        public GameObject Prefab;

        public int MaximumSpawnAmount = 5;
        public int curSpawnAmount = 0;

        public float SpawnTimer = 5;

        private Monster[] _monsterPool;
        private float _curTimer;

        private void Awake()
        {
            if (instance != null)
            {
                Destroy(this);
                return;
            }
            instance = this;
            _monsterPool = new Monster[MaximumSpawnAmount];

            //SetMonsterPool();
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
        private void SpawnMonster()
        {
            int rand = Random.Range(0, _playableTransform.Length);




            Vector3 spawnPos = Random.insideUnitSphere * 5f + _playableTransform[rand].position ;
            spawnPos.z = 0;
            Transform temp = Instantiate(Prefab).transform;
            temp.position = spawnPos;
            curSpawnAmount++;
        }

    }
}
