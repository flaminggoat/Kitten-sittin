using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameOver : MonoBehaviour
{
    public float delaySeconds;
    private UIDocument _screen;
    private bool _isGameOver = false;
    private Button _restartBtn;

    // Start is called before the first frame update
    void Start()
    {
        _screen = GetComponent<UIDocument>();
        _screen.enabled = false;
        
        _restartBtn = _screen.rootVisualElement.Q<Button>("restart-btn");
        _restartBtn.clicked += RestartButtonPressed;
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

    void RestartButtonPressed() {
        Debug.Log("Press restart button");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;

        // If not, it remains in focus and pressing space restarts the scene again
        _restartBtn.Blur();
    }

    public void TriggerGameSuccess(uint kittens) {
        _screen.rootVisualElement.Q<VisualElement>("bg").style.backgroundColor = new StyleColor(new Color(0,0.5f,0,0.4f));
        _screen.rootVisualElement.Q<Label>("result").text = "Congratulations!";
        TriggerGameOver("You nurtured " + kittens.ToString() + " kittens to adulthood.");
    }
 
    public void TriggerGameOver(string reason) {
        if (!_isGameOver) {
            var label = _screen.rootVisualElement.Q<Label>("game-over-reason");
            label.text = reason;

            _screen.enabled = true;
            _isGameOver = true;
        }

    }
}
