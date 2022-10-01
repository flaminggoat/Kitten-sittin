using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public Button startButton;
    public Button optionsButton;


    // Start is called before the first frame update
    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        startButton = root.Q<Button>("start-button");
        optionsButton = root.Q<Button>("options-button");

        startButton.clicked += StartButtonPressed;
        
    }

    void StartButtonPressed() {
        SceneManager.LoadScene("SampleScene");
    }
}
