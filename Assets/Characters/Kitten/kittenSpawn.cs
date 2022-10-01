using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class kittenSpawn : MonoBehaviour
{
    public GameObject kittenPrefab;
    public uint nKittens;

    public float minSpawnIntervalSeconds;
    public float maxSpawnIntervalSeconds;

    private float secondsUntilNextKitten;

    // Start is called before the first frame update
    void Start()
    {
        secondsUntilNextKitten = Random.Range(minSpawnIntervalSeconds, maxSpawnIntervalSeconds);
    }

    // Update is called once per frame
    void Update()
    {
        secondsUntilNextKitten -= Time.deltaTime;

        if (secondsUntilNextKitten <= 0 && nKittens > 0) {
            nKittens--;

            var kittenInitialDirection = Random.Range(0, 360);
            Instantiate(kittenPrefab, transform.position, Quaternion.Euler(new Vector3(0, 0, kittenInitialDirection)));

            secondsUntilNextKitten = Random.Range(minSpawnIntervalSeconds, maxSpawnIntervalSeconds);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var go = other.gameObject;
        var k = go.GetComponent<IsKitten>();
        if (k != null) {
            if (!k.born) {
                k.born = true;
                return;
            }
            Destroy(go);
            nKittens++;
        }
    }
}
