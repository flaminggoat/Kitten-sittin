using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameOver : MonoBehaviour
{
    public float delaySeconds;
    private UIDocument _screen;
    private bool _isGameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        _screen = GetComponent<UIDocument>();
        _screen.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isGameOver) {
            delaySeconds -= Time.deltaTime;
        }

        if (delaySeconds<0) {
            Time.timeScale = 0;
        }
    }

    public void TriggerGameOver() {
        _screen.enabled = true;
        _isGameOver = true;
    }
}
