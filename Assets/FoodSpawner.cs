using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    public GameObject cheesePrefab;
    public GameObject ratPrefab;
    public float spawnInterval = 10;
    public float minSpawnRadius = 5f;
    public float maxSpawnRadius = 20f;

    float _secondsUntilNextFood = 0;
    float _difficulty = 0;
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
        _difficulty += Time.deltaTime;
        _secondsUntilNextFood -= Time.deltaTime;

        if (_secondsUntilNextFood < 0) {
            Vector3 pos;
            do {
                pos = new Vector3(
                    Random.Range(-minSpawnRadius-_difficulty,maxSpawnRadius+_difficulty),
                    Random.Range(-minSpawnRadius-_difficulty,maxSpawnRadius+_difficulty),
                    0);
                // retry until a point is found that is not in water
            }while(_waterCollider.OverlapPoint(new Vector2(pos.x, pos.y)) && _waterCollider != null);
            
            switch (Random.Range(0,2)) {
                case 0:
                    Instantiate(cheesePrefab, pos, new Quaternion(0,0,0,0));
                    break;
                case 1:
                    var r = Instantiate(ratPrefab, pos, new Quaternion(0,0,0,0));
                    var rat = r.GetComponent<Rat>();
                    rat.speedDirectionDegrees = Random.Range(0,360f);
                    break;
            }
            
            _secondsUntilNextFood = spawnInterval;
        }
        
    }
}
