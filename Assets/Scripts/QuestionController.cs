using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionController : MonoBehaviour {
    public Text question;
    public int numQuestion = 0;
    // Use this for initialization
    void Start () {
        
    }
    
    // Update is called once per frame
    void Update () {
        
        if (numQuestion == 0){
            question.text="What happens when a car approaches a traffic light?";
        }
        if (numQuestion == 1)
        {
            question.text ="What happens when a car approaches a “STOP” sign at a railway crossing?";
        }
        if (numQuestion == 2)
        {
            question.text ="What happens when a car approaches a pedestrian crossing the road?";
        }
        if (numQuestion == 3)
        {
            question.text ="What happens when a car is on one-way road and speed limit is 40 miles/hour?";
        }
        if (numQuestion == 4)
        {
            question.text ="What happens when a car has to take a U-turn?";
        }
        if (numQuestion == 5)
        {
            question.text = "What to do when you enter a highway with speed limit 65 miles/hour?";
        }
        if (numQuestion == 6)
        {
            question.text = "What happens if a car has to park itself?";
        }

        
    }
}
