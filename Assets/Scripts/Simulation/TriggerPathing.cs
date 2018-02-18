using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPathing : MonoBehaviour {

    public float proximityRange = 1f;
    public GameObject playerCar;

    public bool willChange;

    public float speed = 2f;

    //timing trackers
    private float totalTime = 0f;
    public float moveTime = 5f;

	// Use this for initialization
	void Start () {

        speed = speed / 10;
    }
	
	// Update is called once per frame
	void Update () {
        float time = Time.deltaTime;
        if (!willChange && playerCar != null)
        {
            if (ProximityTester.proximityMagnitudeTest(proximityRange, this.transform.position, playerCar.transform.position))
            {
                willChange = true;
            }
        }
        if (willChange && totalTime < moveTime)
        {
            Move(time);
            totalTime += time;

        }
    }

    void Move(float time)
    {
        this.transform.Translate(new Vector3(speed * time, 0f, 0f));
    }
}
