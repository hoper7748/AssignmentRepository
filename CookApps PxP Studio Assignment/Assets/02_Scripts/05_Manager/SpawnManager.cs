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
                // 생성량 체크도 해야함
                if (_curTimer > SpawnTimer && curSpawnAmount < MaximumSpawnAmount)
                {
                    // 스폰
                    SpawnMonster();
                    _curTimer = 0;
                }
            }
        }

        public void MonsterDie(Transform monster)
        {
            curSpawnAmount--;
            // 몬스터가 죽을 때마다 카운터가 증가하여 목표치 달성 시, 스테이지 이동.
            if(deadCount >= StageChangeTargetScore && !IsBossFight)
            {
                // 보스 스폰
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
            // Status 증가
            GameManager.Instance.NextStage();
            StageChangeTargetScore += 10;

            HealthPoint *= 1f + GameManager.Instance.GetStageCount * 0.1f;
            AttackDamage *= 1f + GameManager.Instance.GetStageCount * 0.1f;
        }

        private void SpawnMonster()
        {
            int rand = Random.Range(0, GameManager.Instance.Playables.Count);
            // 몬스터를 스폰해 주면서 스테이터스를 세팅해주는 방법도 고려해보자.

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
