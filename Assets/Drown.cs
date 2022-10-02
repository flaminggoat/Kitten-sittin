using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Drown : MonoBehaviour
{
    float _scale = 1;
    bool _isDrowning = false;
    public GameOver gameOverScreen;
    public bool isLeaping = false;

    private List<string> _drownSentences = new List<string>()
    {
        "Not even mother cats like water!",
        "Remember, cats don't like water",
        "That blue thing you stepped on, yeah, that was water :/",
    };

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_isDrowning)
        {
            if (_scale <= 0) {
                Destroy(gameObject);
            }

            _scale -= Time.deltaTime * 0.3f;
            
            transform.localScale = transform.localScale * _scale;
        }
    }

    private void OnCollisionStay2D(Collision2D other) {
        if (other.gameObject.GetComponent<IsWater>() != null && !isLeaping && !_isDrowning)
        {
            // Prevent any movement
            var m = GetComponent<mother>();
            if (m != null) {
                Destroy(GetComponent<mother>());

                // Trigger game over
                var sentenceIndex =  Random.Range(0, _drownSentences.Count);
                gameOverScreen.TriggerGameOver(_drownSentences[sentenceIndex]);
            }

            // Kill the kitten
            var k = GetComponent<Kitten>();
            if (k != null) {
                k.manager.deadKittens++;
            }

            var a = GetComponent<AudioSource>();
            if (a != null) {
                a.Play();
            } else {
                Debug.Log("No audio source to play");
            }
            _isDrowning = true;
        }
    }

}
