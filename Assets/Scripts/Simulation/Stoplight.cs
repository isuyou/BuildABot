using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stoplight : MonoBehaviour {

    public bool stop;
    SpriteRenderer sprite;
    public GameObject playerCar;
    public bool changeLight;
    public bool wait;
    public Vector2 proximityRange = new Vector2(1f,1f);

    private int numChanges = 0;
    public int maxNumChanges = 2;
    private bool willChange = false;
    private float totalTime = 0f;
    //timeToChangeLight size must be equal to maxNumChanges
    public float[] timeToChangeLight = { 1f, 3f };

    public Sprite redLight;
    public Sprite greenLight;

	// Use this for initialization
	void Start () {
        sprite = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        float time = Time.deltaTime;
        if(!willChange && playerCar != null)
        {
            if(ProximityTester.proximityTest(proximityRange, this.transform.position, playerCar.transform.position))
            {
                willChange = true;
            }
        }
        if (willChange && numChanges < maxNumChanges)
        {
            if (totalTime > timeToChangeLight[numChanges])
            {
                ChangeLight();
                numChanges++;
            }
            totalTime += time;
            
        }

        
    }
    
    void ChangeLight()
    {
        if (stop)
            sprite.sprite = greenLight;
        else
            sprite.sprite = redLight;
        stop = !stop;
        totalTime = 0f;

    }
}
