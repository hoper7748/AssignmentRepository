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

            SetMonsterPool();
        }


        private void Start()
        {

        }

        private void Update()
        {
            _curTimer += Time.deltaTime;
            // ������ üũ�� �ؾ���
            if(_curTimer > SpawnTimer && curSpawnAmount < MaximumSpawnAmount)
            {
                // ����
                SpawnMonster();
                _curTimer = 0;
            }
        }

        private void SetMonsterPool()
        {
            for (int i = 0; i < MaximumSpawnAmount; i++)
            {
                //MonsterPools.Add(Instantiate(Prefab), false);
                _monsterPool[i] = Instantiate(Prefab.GetComponent<Monster>());
                _monsterPool[i].gameObject.SetActive(false);
                _monsterPool[i].transform.name = $"Goblin ({i})";
            }
        }
        private void SpawnMonster()
        {
            // ������ �����ǿ� ����
            int rand = Random.Range(0, _playableTransform.Length);
            Vector3 spawnPos = Random.insideUnitSphere * 5f;
            spawnPos.z = 0;
            foreach (var monster in _monsterPool)
            {
                if (!monster.gameObject.activeSelf)
                {
                    monster.SetHealthPoint();
                    monster.gameObject.SetActive(true);
                    // ���� ��ǥ�� �����ؾ���.
                    monster.transform.position = _playableTransform[rand].transform.position + spawnPos;
                    return;
                }
            }
        }
    }
}
