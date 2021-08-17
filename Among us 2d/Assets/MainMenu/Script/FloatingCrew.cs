using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingCrew : MonoBehaviour
{
    [SerializeField]
    private GameObject prefab;
    [SerializeField]
    private List<Sprite> sprites;

    private bool[] crewStates = new bool[12];
    private float timer = 0.5f;
    private float distance = 10f;

    // Start is called before the first frame update
    void Start()
    {
        for(int i=0; i<12; i++)
        {
            SpawnFloatingCrew((EPlayerColor)i, Random.Range(0f,distance));
        }

        SpawnFloatingCrew(0, 10);
    }

    // Update is called once per frame
    void Update()
    {
        
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            SpawnFloatingCrew((EPlayerColor)Random.Range(0, 12), distance);
            timer = 1f;
        }
    }

    // FloatingCrew들을 만드는 함수 (0~12번의 색깔, 생성 거리)
    public void SpawnFloatingCrew(EPlayerColor playerColor, float dist)
    {
        if(!crewStates[(int)playerColor])
        {
            crewStates[(int)playerColor] = true;

            // 랜덤한 각도 생성
            float angle = Random.Range(0f, 360f) * Mathf.PI / 180;

            // 스폰위치
            Vector3 spawnPos = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0f) * dist;

            // 방향
            Vector3 direction = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f);
            float floatingSpeed = Random.Range(0.5f, 1f);
            float rotateSpeed = Random.Range(-0.5f, 0.5f);

            // Instantiate = 프리팹생성함수
            // Quaternion.identity = roatation(0,0,0) (회전 없음)
            var crew = Instantiate(prefab, spawnPos, Quaternion.identity).GetComponent<Crew>();

            // 프리팹 crew의 설정
            crew.SetFloatingCrew(sprites[Random.Range(0, sprites.Count)], playerColor, direction,
                floatingSpeed, rotateSpeed, Random.Range(0.5f, 1f));

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var crew = collision.GetComponent<Crew>();
        if (crew != null)
        {
            crewStates[(int)crew.playerColor] = false;
            Destroy(crew.gameObject);
        }
    }
}
