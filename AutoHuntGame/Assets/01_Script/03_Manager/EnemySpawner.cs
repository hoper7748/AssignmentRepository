using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // 몬스터 생성 주기
    public float SpawnTimer = 5;
    public int curSpawn = 0;
    public int MaxSpawn = 5;
    [Space(10)]
    [Header("Status")]
    public int EHP;
    public int EATK;
    public int EAttackRate;
    public int EAttackRange;

    // 플레이어 중심 생성 범위
    public float CraeteRange = 10f;

    public GameObject EnemyPrefab;

    private float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
    }

    private Vector3 SpawnPoint()
    {
        Vector3 PlayerPoint = Player.Instance.transform.position;
        Vector3 getPoint = Random.onUnitSphere;
        getPoint.y = PlayerPoint.y; 

        float r = Random.Range(5f, CraeteRange);

        return getPoint * r + PlayerPoint;
    }

    private void Spawn()
    {
        curSpawn++;
        GameObject obj = Instantiate(EnemyPrefab, SpawnPoint(), Quaternion.identity);
        obj.GetComponent<Enemy>().Initialize(this, EHP, EATK, EAttackRate, EAttackRange);
    }

    public void DeadEnemy()
    {
        curSpawn--;
    }

    // Update is called once per frame
    void Update()
    {
        if (curSpawn < MaxSpawn)
        {
            timer += Time.deltaTime;
            if (timer > SpawnTimer)
            {
                Spawn();
                timer = 0;
            }
        }
    }
}
