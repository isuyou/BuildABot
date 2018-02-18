using UnityEngine;
using UnityEngine.SceneManagement;

public class startNewScene : MonoBehaviour { 
    public void OnMouseButton() {
        SceneManager.LoadScene("Simulation");
    } }