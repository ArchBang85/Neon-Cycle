using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class BowelMovementController : MonoBehaviour {

    public List<GameObject> pistons = new List<GameObject>();
    public float[] pistonSpeeds;
    public float maxPistonReach = 0.4f;
    public GameObject[] sliderPegs = new GameObject[3];
    private float[] pegStartPos;
    public List<Vector3> initialPistonPos = new List<Vector3>();
    private float maxSpeed = 0.75f;

	// Use this for initialization
	void Start () {

        pistonSpeeds = new float[50];
        for (int i = 0; i <pistonSpeeds.Length; i++)
        {
            pistonSpeeds[i] = 0.0f;
        }

        pegStartPos = new float[3];
        try {
            pegStartPos[0] = sliderPegs[0].transform.position.y;
            pegStartPos[1] = sliderPegs[1].transform.position.y;
            pegStartPos[2] = sliderPegs[2].transform.position.y;
        }
        catch
        {

        }
	    foreach(GameObject piston in GameObject.FindGameObjectsWithTag("Piston"))
        {
            pistons.Add(piston);
            initialPistonPos.Add(piston.transform.position);
        }

	}
	
	// Update is called once per frame
	void Update () {

        // Move pistons
        for (int i = 0; i < pistons.Count; i++)
        {
            GameObject Piston = pistons[i];

            PistonScript p = Piston.GetComponent<PistonScript>();
            float t = p.trajectory;
            // Reverse dir
            /*if (p.forward && t > maxPistonReach)
            {
                p.forward = false;
            }

            if (!p.forward && t < -maxPistonReach)
            {
                p.forward = true;
            }*/

            //float moveAmount = pistonSpeeds[p.group - 1] * Time.deltaTime;
            try
            {
                float moveAmount = -(float)Math.Cos(GameController.instance.wheels[p.group - 1].getWheelTurns() * 2 * Math.PI) * maxPistonReach * (p.forward ? 1 : -1);
                Debug.Log("Piston " + p.group + " has value " + moveAmount);
                p.transform.position = initialPistonPos[i] + Vector3.up * moveAmount;

            }
            catch
            {
                Debug.Log("unable to move pistons with wheels");
                p.transform.position = initialPistonPos[i] + Vector3.up * pistonSpeeds[p.group - 1] * p.orientation;
            }

            /*
            if(p.forward)
            {
                // move forward
                p.trajectory += moveAmount;
                p.transform.Translate(Vector3.up * moveAmount);

            } else if(!p.forward)
            {
                // move backwards by speed
                p.trajectory -= moveAmount;
                p.transform.Translate(Vector3.up * -moveAmount);
            }*/
        }

        // Keyboard controls
        if(Input.GetKey(KeyCode.Q) && pistonSpeeds[0] < maxSpeed)
        {
          //  sliderPegs[0].transform.position = new Vector2(sliderPegs[0].transform.position.x, pegStartPos[0] + pistonSpeeds[0]);
            pistonSpeeds[0] += Time.deltaTime;
                    }
        if (Input.GetKey(KeyCode.A) && pistonSpeeds[0] > -maxSpeed)
        {
            pistonSpeeds[0] -= Time.deltaTime;
        //    sliderPegs[0].transform.position = new Vector2(sliderPegs[0].transform.position.x, pegStartPos[0] + pistonSpeeds[0]);

        }

        if (Input.GetKey(KeyCode.W) && pistonSpeeds[1] < maxSpeed)
        {
            pistonSpeeds[1] += Time.deltaTime;
      //      sliderPegs[1].transform.position = new Vector2(sliderPegs[1].transform.position.x, pegStartPos[1] + pistonSpeeds[1]);

        }
        if (Input.GetKey(KeyCode.S) && pistonSpeeds[1] > -maxSpeed)
        {
            pistonSpeeds[1] -= Time.deltaTime;
    //        sliderPegs[1].transform.position = new Vector2(sliderPegs[1].transform.position.x, pegStartPos[1] + pistonSpeeds[1]);

        }
        if (Input.GetKey(KeyCode.E) && pistonSpeeds[2] < maxSpeed)
        {
            pistonSpeeds[2] += Time.deltaTime;
  //          sliderPegs[2].transform.position = new Vector2(sliderPegs[2].transform.position.x, pegStartPos[2] + pistonSpeeds[2]);

        }
        if (Input.GetKey(KeyCode.D) && pistonSpeeds[2] > -maxSpeed)
        {
            pistonSpeeds[2] -= Time.deltaTime;
//            sliderPegs[2].transform.position = new Vector2(sliderPegs[2].transform.position.x, pegStartPos[2] + pistonSpeeds[2]);

        }

        for (int i = 0; i < 3; i++)
	    {
            try {
                pistonSpeeds[i] = GameController.instance.wheels[i].position / 100f;
            }
            catch
            {

            }
	    }

        Mathf.Clamp(pistonSpeeds[0], -maxSpeed, maxSpeed);
        Mathf.Clamp(pistonSpeeds[1], -maxSpeed, maxSpeed);
        Mathf.Clamp(pistonSpeeds[2], -maxSpeed, maxSpeed);
	}
}
