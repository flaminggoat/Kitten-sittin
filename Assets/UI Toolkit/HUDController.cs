using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HUDController : MonoBehaviour
{
    public KittenManager kittenManager;
    private ProgressBar _hpBar;

    // Start is called before the first frame update
    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        _hpBar = root.Q<ProgressBar>("hp-bar");
    }

    // Update is called once per frame
    void Update()
    {
        _hpBar.value = kittenManager.hp;
    }
}
