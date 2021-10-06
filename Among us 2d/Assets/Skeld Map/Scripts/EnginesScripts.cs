using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnginesScripts : MonoBehaviour
{
    public GameObject[] Steams;

    public GameObject[] Sparks;
    public Sprite[] SparkImg;

    void Start()
    {
        foreach(var steam in Steams)
        {
            StartCoroutine(RandomSteam(steam));
        }

        foreach (var spark in Sparks)
        {
            StartCoroutine(RandomSpark(spark));
        }
    }

    private IEnumerator RandomSteam(GameObject steam)
    {
        while (true)
        {
            float RandTime = Random.Range(0.5f, 1.5f);
            yield return new WaitForSeconds(RandTime);
            steam.SetActive(true);
            yield return new WaitForSeconds(0.6f);
            steam.SetActive(false);
        }
    }

    private IEnumerator RandomSpark(GameObject spark)
    {
        while (true)
        {
            float RandTime = Random.Range(0.5f, 1.5f);
            yield return new WaitForSeconds(RandTime);
            spark.SetActive(true);

            for (int i = 0; i < 7; i++)
            {
                yield return null;
                spark.GetComponent<SpriteRenderer>().sprite = SparkImg[Random.Range(0, 14)];
            }

            yield return new WaitForSeconds(0.15f);
            spark.SetActive(false);
        }
    }
}
