using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    public GameObject cheesePrefab;
    public float spawnInterval = 10;
    float _secondsUntilNextKitten = 0;

    public float spawnRadius = 20f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _secondsUntilNextKitten -= Time.deltaTime;

        if (_secondsUntilNextKitten < 0 ) {
            var pos = new Vector3(Random.Range(-spawnRadius,spawnRadius),Random.Range(-spawnRadius,spawnRadius),Random.Range(-spawnRadius,spawnRadius));
            var kitten = Instantiate(cheesePrefab, pos, new Quaternion(0,0,0,0));
            _secondsUntilNextKitten = spawnInterval;
        }
        
    }
}
