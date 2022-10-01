using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kittenSpawn : MonoBehaviour
{
    public GameObject kitten;
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
            Instantiate(kitten, transform.position, Quaternion.Euler(new Vector3(0, 0, kittenInitialDirection)));

            secondsUntilNextKitten = Random.Range(minSpawnIntervalSeconds, maxSpawnIntervalSeconds);
        }
        
    }
}
