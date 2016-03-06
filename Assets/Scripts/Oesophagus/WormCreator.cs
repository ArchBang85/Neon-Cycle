using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class WormCreator : MonoBehaviour {

    public GameObject wormWall;
    public float wallOffset;
    public List<GameObject> wormWalls = new List<GameObject>();
    public GameObject wormWallParent;
    // Use this for initialization
    IEnumerator CreateWallsWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay); 
        for (int i = 0; i < 40; i++)
        {
            GameObject leftWall = (GameObject)Instantiate(wormWall, new Vector3(-wallOffset, 6.0f - i * 1.5f, -1), Quaternion.identity * Quaternion.Euler(90, 0, 0));
            wormWalls.Add(leftWall);
            leftWall.transform.SetParent(wormWallParent.transform);
            GameObject rightWall = (GameObject)Instantiate(wormWall, new Vector3(wallOffset, 6.0f - i * 1.5f, -1), Quaternion.identity * Quaternion.Euler(90, 0, 0));
            rightWall.GetComponent<WormWallBehaviour>().leftWall = false;
            wormWalls.Add(rightWall);
            rightWall.transform.SetParent(wormWallParent.transform);

        }
    }

	void Start () {
        wormWallParent = GameObject.Find("WormWalls");
        StartCoroutine("CreateWallsWithDelay", 0.1f);
    }

// Update is called once per frame
void Update () {
	
	}
}
