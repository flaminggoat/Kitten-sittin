using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public Button startButton;
    public Button fsButton;


    // Start is called before the first frame update
    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        startButton = root.Q<Button>("start-button");
        fsButton = root.Q<Button>("fs-button");

        startButton.clicked += StartButtonPressed;
        startButton.clicked += startButton.Blur;

        fsButton.clicked += fsButtonPressed;
        fsButton.clicked += fsButton.Blur;
    }

    void fsButtonPressed() {
        Screen.fullScreen = !Screen.fullScreen;
    }

    void StartButtonPressed() {
        SceneManager.LoadScene("SampleScene");
    }
}
