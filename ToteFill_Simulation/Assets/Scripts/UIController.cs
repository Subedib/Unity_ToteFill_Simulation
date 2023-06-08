using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;



public class UIController : MonoBehaviour
{
    public Button Filler1Tote, Filler2Tote;
    public RadioButton Filler1Sel, Filler2Sel;

    // Start is called before the first frame update
    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        Filler1Sel = root.Q<RadioButton>("filler1Sel");
        Filler2Sel = root.Q<RadioButton>("filler2Sel");
        Filler1Tote = root.Q<Button>("filler1Tote");
        Filler2Tote = root.Q<Button>("filler2Tote");

        Filler1Tote.clicked += () => GameObject.Find("Spawner1").GetComponent<Spawner>().SpawnTote();
    }

}
