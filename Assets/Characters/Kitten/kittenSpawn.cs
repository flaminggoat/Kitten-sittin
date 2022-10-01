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

    private float _secondsUntilNextKitten;

    // Start is called before the first frame update
    void Start()
    {
        _secondsUntilNextKitten = Random.Range(minSpawnIntervalSeconds, maxSpawnIntervalSeconds);
    }

    // Update is called once per frame
    void Update()
    {
        _secondsUntilNextKitten -= Time.deltaTime;

        if (_secondsUntilNextKitten <= 0 && nKittens > 0) {
            nKittens--;

            var kittenInitialDirection = Random.Range(0, 360);
            var kitten = Instantiate(kittenPrefab, transform.position, new Quaternion(0,0,0,0));
            var k = kitten.GetComponent<Kitten>();
            k.speedDirectionDegrees = kittenInitialDirection;

            _secondsUntilNextKitten = Random.Range(minSpawnIntervalSeconds, maxSpawnIntervalSeconds);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var go = other.gameObject;
        var k = go.GetComponent<Kitten>();
        if (k != null) {
            if (!k.isBorn) {
                k.isBorn = true;
                return;
            }
            Destroy(go);
            nKittens++;
        }
    }
}
