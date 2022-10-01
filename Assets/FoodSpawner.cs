using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    public GameObject cheesePrefab;
    public float spawnInterval = 10;
    public float spawnRadius = 20f;

    float _secondsUntilNextKitten = 0;
    CompositeCollider2D _waterCollider;

    // Start is called before the first frame update
    void Start()
    {
        _waterCollider = GameObject.FindGameObjectWithTag("water").GetComponent<CompositeCollider2D>();
        if (_waterCollider == null) {
            Debug.Log("Failed to get water collider");
        }
    }
        
    // Update is called once per frame
    void Update()
    {
        _secondsUntilNextKitten -= Time.deltaTime;

        if (_secondsUntilNextKitten < 0) {
            Vector3 pos;
            do {
                pos = new Vector3(Random.Range(-spawnRadius,spawnRadius),Random.Range(-spawnRadius,spawnRadius),Random.Range(-spawnRadius,spawnRadius));
                // retry until a point is found that is not in water
            }while(_waterCollider.OverlapPoint(new Vector2(pos.x, pos.y)) && _waterCollider != null);
            
            var kitten = Instantiate(cheesePrefab, pos, new Quaternion(0,0,0,0));
            _secondsUntilNextKitten = spawnInterval;
        }
        
    }
}
