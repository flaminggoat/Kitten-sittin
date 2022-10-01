using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class KittenManager : MonoBehaviour
{
    public GameObject kittenPrefab;
    public uint nKittens;
    private uint _initialNKittens;

    public float life;
    public float reduceLifeRate;

    public float minSpawnIntervalSeconds;
    public float maxSpawnIntervalSeconds;

    private float _secondsUntilNextKitten;

    // Start is called before the first frame update
    void Start()
    {
        _secondsUntilNextKitten = Random.Range(minSpawnIntervalSeconds, maxSpawnIntervalSeconds);
        _initialNKittens = nKittens;
    }

    // Update is called once per frame
    void Update()
    {
        life -= Time.deltaTime * reduceLifeRate;
        _secondsUntilNextKitten -= Time.deltaTime;

        if (_secondsUntilNextKitten <= 0 && nKittens > 0) {
            nKittens--;

            var kittenInitialDirection = Random.Range(0, 360);
            var kitten = Instantiate(kittenPrefab, transform.position, new Quaternion(0,0,0,0));
            var k = kitten.GetComponent<Kitten>();
            k.speedDirectionDegrees = kittenInitialDirection;

            _secondsUntilNextKitten = Random.Range(minSpawnIntervalSeconds, maxSpawnIntervalSeconds);
        }

        if (life <= 0) {
            Debug.Log("Game Over!");
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
            return;
        }

        var f = go.GetComponent<Food>();
        if (f != null) {
            life += f.lifeBoost;
            Destroy(go);
            return;
        }
    }
}
