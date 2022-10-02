using System.Collections;
using System.Collections.Generic;
using MyUILibrary;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class KittenCounterController : MonoBehaviour
{
    public KittenManager kittenManager;
    private TextMeshProUGUI _text;

    // Start is called before the first frame update
    void Start()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        _text.text = kittenManager.nKittens.ToString();
    }
}
