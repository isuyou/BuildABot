using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximityTester{

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public static bool proximityTest(Vector2 proximity, Vector3 position1, Vector3 position2)
    {
        if ((Mathf.Abs(position1.x - position2.x) < proximity.x) && (Mathf.Abs(position1.x - position2.x) < proximity.y))
            return true;
        return false;
    }

    public static bool proximityMagnitudeTest(float proximityMagnitude, Vector3 position1, Vector3 position2)
    {
        if(new Vector2(position1.x - position2.x, position1.y - position2.y).magnitude < proximityMagnitude)
        {
            return true;
        }
        return false;
    }
}
