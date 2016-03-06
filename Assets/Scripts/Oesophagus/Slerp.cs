using UnityEngine;
using System.Collections;

public class Slerp : MonoBehaviour {
    float initY;
    float initX;
    public bool moveWithLevel = false;
    float levelSpeed = 0;
    float levelAdjustment = 0;
    float t;
    public float speed = 0.25f;
    public float[] slerpBounds = new float[2];
    public bool regular = false;
    public bool forward = false;
	// Use this for initialization
	void Start () {
        initX = transform.position.x;
        if(moveWithLevel)
        {
            levelSpeed = GameObject.Find("Main Camera").GetComponent<O_GameController>().levelSpeed - 0.05f;
        }
        initY = transform.position.y;
        if(slerpBounds[0] == 0.0f)
        {
            slerpBounds[0] = 0.3f;
        } 
        if(slerpBounds[1] == 0.0f)
        {
            slerpBounds[1] = 0.5f;
        }
             
        t = Random.Range(slerpBounds[0], slerpBounds[1]);
        if(regular)
        {
            t = slerpBounds[1];
        }
	}
	
	// Update is called once per frame
	void Update () {

        if(moveWithLevel)
        {
            levelAdjustment += levelSpeed * Time.deltaTime;
        }
        if (!forward)
        {
            transform.position = new Vector3(transform.position.x, initY + levelAdjustment + Mathf.PingPong(Time.time * speed, t), transform.position.z);
        }       
        else
        {
            transform.position = new Vector3(initX + levelAdjustment + Mathf.PingPong(Time.time * speed, t), initY + levelAdjustment + Mathf.PingPong(Time.time * speed, t), transform.position.z);
        }
    }
}
