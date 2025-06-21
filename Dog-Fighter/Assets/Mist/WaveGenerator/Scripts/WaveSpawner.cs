using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public GameObject wavePrefab;
    public float spawnRate = 1f; //lower the faster
    public float lifetime = 3f;

    public float speed = 10f;

    //set range waves can spawn between
    public float XSpawnRange = 200f;
    public float zSpawnRange = 200f;

    public float FadeInTime = 3f;

    //set range that the waves spawn between
    public float MinSize = 20f;
    public float MaxSize = 100f;
    private void Start()
    {
        InvokeRepeating(nameof(SpawnWave), 0, spawnRate);
    }

    void SpawnWave()
    {
        Vector3 pos = transform.position = new Vector3(
            UnityEngine.Random.Range(-XSpawnRange, XSpawnRange), transform.position.y, UnityEngine.Random.Range(-zSpawnRange, zSpawnRange));
        GameObject wave = Instantiate(wavePrefab, pos, Quaternion.Euler(0, 0, 0));
        wave.transform.localScale = Vector3.one * UnityEngine.Random.Range(MinSize, MaxSize);
        StartCoroutine(FadeIn(wave));
        
    }
    IEnumerator FadeIn(GameObject obj)
    {
        Material mat = obj.GetComponent<Renderer>().material;
        float t = 0;
        while (t < FadeInTime)
        {
            t += Time.deltaTime;
            float fade = 0 + Mathf.Clamp01(t / FadeInTime);
            Debug.Log(fade);
            mat.SetFloat("_Fade", fade);
            yield return null;
        }
        mat.SetFloat("_Fade", 1f);
        StartCoroutine(FadeAndDestroy(obj));
    }
    IEnumerator FadeAndDestroy(GameObject obj)
    {
        Material mat = obj.GetComponent<Renderer>().material;
        float t = 0;
        while (t < lifetime)
        {
            t += Time.deltaTime;
            float fade = 1 - (t / lifetime);
            mat.SetFloat("_Fade", fade);
            yield return null;
        }
        Destroy(obj);
    }
}
