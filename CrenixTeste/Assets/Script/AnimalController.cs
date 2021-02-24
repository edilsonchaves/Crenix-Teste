using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AnimalController : MonoBehaviour
{

    [TextArea(1,3)][SerializeField]string[] texts;
    [SerializeField]Text textAnimal;
    // Start is called before the first frame update
    void Start()
    {
        textAnimal.text = texts[0];
    }

    // Update is called once per frame
    public void ChangeTextAnimals(bool statusGame)
    {
        if (statusGame)
        {
            textAnimal.text = texts[1];
        }
        else
        {
            textAnimal.text = texts[0];
        }
    }
}
