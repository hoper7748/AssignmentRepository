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


        [Space(10), Header("Monster Start Info"), Space(5)]
        public float HealthPoint;
        public float AttackDamage;

        [Space(10), Header("Spawn Values"), Space (5)]
        public int MaximumSpawnAmount = 5;
        public int curSpawnAmount = 0;

        public float SpawnTimer = 5;

        public bool IsBossFight = false;

        public int StageChangeTargetScore = 0;

        Transform BossMonster;

        protected int deadCount = 0;
        private float _curTimer;

        private void Awake()
        {
            if (instance != null)
            {
                Destroy(this);
                return;
            }
            instance = this;


            //SetMonsterPool();
        }


        private void Start()
        {
            deadCount = 0;
            if(StageChangeTargetScore <= 0)
            {
                StageChangeTargetScore = 10;
            }
        }

        private void Update()
        {
            if(!IsBossFight)
            {
                _curTimer += Time.deltaTime;
                // ������ üũ�� �ؾ���
                if (_curTimer > SpawnTimer && curSpawnAmount < MaximumSpawnAmount)
                {
                    // ����
                    SpawnMonster();
                    _curTimer = 0;
                }
            }
        }

        public void MonsterDie(Transform monster)
        {
            curSpawnAmount--;
            // ���Ͱ� ���� ������ ī���Ͱ� �����Ͽ� ��ǥġ �޼� ��, �������� �̵�.
            if(deadCount >= StageChangeTargetScore && !IsBossFight)
            {
                // ���� ����
                IsBossFight = true;
                SpawnMonster();
            }
            if(BossMonster != null && monster == BossMonster)
            {
                NextStage();
                IsBossFight = false;
            }
            else
            {
                deadCount++;
            }

            GameManager.Instance.GetResult();
        }
        
        public void NextStage()
        {
            // Status ����
            GameManager.Instance.NextStage();
            StageChangeTargetScore += 10;

            HealthPoint *= 1f + GameManager.Instance.GetStageCount * 0.1f;
            AttackDamage *= 1f + GameManager.Instance.GetStageCount * 0.1f;
        }

        private void SpawnMonster()
        {
            int rand = Random.Range(0, GameManager.Instance.Playables.Count);
            // ���͸� ������ �ָ鼭 �������ͽ��� �������ִ� ����� ����غ���.

            Vector3 spawnPos = Random.insideUnitSphere * 5f + GameManager.Instance.Playables[rand].transform.position ;
            spawnPos.z = 0;
            Transform temp = Instantiate(Prefab).transform;
            temp.position = spawnPos;
            if(IsBossFight)
            {
                temp.localScale *= 3f;
                temp.GetComponent<Character.Character>().Initialize(HealthPoint * 1.5f, AttackDamage * 1.5f, 2f);
                BossMonster = temp;
            }
            else
            {
                temp.GetComponent<Character.Character>().Initialize(HealthPoint, AttackDamage);
            }
            curSpawnAmount++;
        }

    }
}
