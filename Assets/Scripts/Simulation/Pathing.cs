using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Pathing : MonoBehaviour {

    public GameObject[] pathways;
    public float[] turning;
    int startIndex = 0;
    private List<Transform> path = new List<Transform>();
    public float[] waitTimes;
    public float speed = 2;
    public float turnRate = 5;
    public bool player = false;

    
    public float proximityRange = 1f;

    private int index;

    //timer control
    private int waitIndex;
    private bool waiting = false;
    private float totalWait = 0f;

    //turning control
    private float totalTurn = 0f;

    //stop scripting for stoplights
    public GameObject stopLight;
    public Vector2 stoplightProximityRange = new Vector2(1f, 1f);

    //stop scripting for other cars and collisions
    public GameObject[] otherCars;
    public float carCollisionProximityRange = 1f;


    // Use this for initialization
    void Start () {
        index = startIndex;
        waitIndex = startIndex;
        if (pathways == null)
            pathways = new GameObject[0];
        if (waitTimes == null)
            waitTimes = new float[0];
        if (turning == null)
            turning = new float[0];
        speed = speed / 10;
        proximityRange = proximityRange / 10;
	}

    private bool hasLogged = false;
	
	// Update is called once per frame
	void Update () {
        float time = Time.deltaTime;
        if (!waiting)
        {
            checkLight(time);
            
        }
        else
            waitCheck(time);
    }

    private void checkLight(float time)
    {
        if(stopLight != null)
        {
            Stoplight stopScript = (Stoplight)stopLight.GetComponent("Stoplight");
            if (stopScript.stop)
            {
                if (ProximityTester.proximityTest(stoplightProximityRange, this.transform.position, stopLight.transform.position))
                {
                    //car stops in place until light is go
                    if(!hasLogged)
                        Debug.Log("stop");
                    hasLogged = true;
                }
                else
                    checkCarCollision(time);
            }
            else
            {
                checkCarCollision(time);
            }
        }
        else
            checkCarCollision(time);
    }

    private void checkCarCollision(float time)
    {
        if(otherCars != null)
        {
            foreach(GameObject otherCar in otherCars)
            {
                if (ProximityTester.proximityMagnitudeTest(carCollisionProximityRange, this.transform.position, otherCar.transform.position))
                    return;
            }
        }
        moveCheck(time);
    }

    private void moveCheck(float time)
    {
        if(index < turning.Count())
        {
            if (totalTurn < turning.ElementAt(index))
            {
                float currentTurn = turnRate * speed * time * 10;
                totalTurn += currentTurn;
                this.transform.Rotate(Vector3.forward * currentTurn);
            }
        }
        
        if (index < pathways.Length)
        {
            Vector3 positionDifference = this.transform.localPosition - pathways.ElementAt(index).transform.localPosition;
            if (Mathf.Abs(positionDifference.x) > 1 && Mathf.Abs(positionDifference.y) > 1)
            {
                turnCar(positionDifference, time);
            }

            if (Mathf.Abs(positionDifference.x) < proximityRange)
            {
                moveLeftRight(speed, time);
            }
            else if (Mathf.Abs(positionDifference.x) < proximityRange)
            {
                moveLeftRight(speed, time);
            }
            else if(Mathf.Abs(positionDifference.y) < proximityRange)
            {
                moveLeftRight(speed, time);
            }
            else if(Mathf.Abs(positionDifference.y) < proximityRange)
            {
                moveLeftRight(speed, time);
            }

            if (new Vector2(positionDifference.x, positionDifference.y).magnitude < proximityRange)
            {
                waiting = true;
                waitCheck(time);
            }
        }
        
    }

    private void moveUpDown(float adjustedSpeed, float time)
    {
        
        this.transform.Translate(new Vector3(0f, adjustedSpeed * time, 0f));
        
    }

    private void moveLeftRight(float adjustedSpeed, float time)
    {
        this.transform.Translate(new Vector3(adjustedSpeed * time, 0f, 0f));
    }

    private void turnCar(Vector3 positionDifference, float time)
    {

    }

    private void waitCheck(float time)
    {
        totalWait += time;
        if (index < waitTimes.Count()) {
            if (totalWait > waitTimes.ElementAt(index))
            {
                resetCounters();
            }
        }
        else
        {
            resetCounters();
        }

    }

    private void resetCounters()
    {
        waiting = false;
        index++;
        totalWait = 0f;
        totalTurn = 0f;
    }
}
