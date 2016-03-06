using UnityEngine;
using System.Collections;

public class WormWallBehaviour : MonoBehaviour {
    float initX;
    float initTime;
    float t = 0;
    public bool leftWall = true;
	// Use this for initialization
	void Start () {
        initX = transform.position.x;
        initTime = Time.time;
        t = Random.Range(0.2f, 0.6f);
    }

    // Update is called once per frame
    void Update () {
        if(leftWall)
        {
            transform.position = new Vector3(initX + Mathf.PingPong(Time.time / 4, t), transform.position.y, transform.position.z);
        } else
        {
            transform.position = new Vector3(initX - Mathf.PingPong(Time.time / 4, t), transform.position.y, transform.position.z);
        }
    }
}
