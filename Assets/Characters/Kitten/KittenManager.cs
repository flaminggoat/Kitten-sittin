using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class KittenManager : MonoBehaviour
{
    public GameObject kittenPrefab;
    public uint nKittens;
    [HideInInspector]
    public uint initialNKittens;
    [HideInInspector]
    public uint deadKittens = 0;

    public GameOver gameOverScreen;

    public float hp;
    private float _maxHp;
    public float reduceLifeRate;

    public float minSpawnIntervalSeconds;
    public float maxSpawnIntervalSeconds;

    private float _secondsUntilNextKitten;

    private List<string> _hungrySentences = new List<string>()
    {
        "You couldn't keep your kittens fed! :(",
        "Your kittens needed food...",
    };

    private List<string> _diedSentences = new List<string>()
    {
        "Your kittens walked into the world before they were ready :(",
        "You have no kittens left alive :(",
    };

    // Start is called before the first frame update
    void Start()
    {
        _secondsUntilNextKitten = Random.Range(minSpawnIntervalSeconds, maxSpawnIntervalSeconds);
        initialNKittens = nKittens;
        _maxHp = hp;
    }

    // Update is called once per frame
    void Update()
    {
        hp -= Time.deltaTime * reduceLifeRate;
        _secondsUntilNextKitten -= Time.deltaTime;

        if (_secondsUntilNextKitten <= 0 && nKittens > 0) {
            nKittens--;

            var kittenInitialDirection = Random.Range(0, 360);
            var kitten = Instantiate(kittenPrefab, transform.position, new Quaternion(0,0,0,0));
            var k = kitten.GetComponent<Kitten>();
            k.speedDirectionDegrees = kittenInitialDirection;
            k.manager = this;

            _secondsUntilNextKitten = Random.Range(minSpawnIntervalSeconds, maxSpawnIntervalSeconds);
        }

        if (hp <= 0) {
            var sentenceIndex =  Random.Range(0, _hungrySentences.Count);
            gameOverScreen.TriggerGameOver(_hungrySentences[sentenceIndex]);
        } else if (initialNKittens == deadKittens) {
            var sentenceIndex =  Random.Range(0, _diedSentences.Count);
            gameOverScreen.TriggerGameOver(_diedSentences[sentenceIndex]);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
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
            hp += f.lifeBoost;

            if (hp > _maxHp) {
                hp = _maxHp;
            }

            Destroy(go);
            return;
        }
    }
}
