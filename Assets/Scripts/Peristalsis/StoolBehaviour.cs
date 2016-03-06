using UnityEngine;
using System.Collections;

public class StoolBehaviour : MonoBehaviour {
    LevelScoreKeeper scoreScript;
	// Use this for initialization
	void Start () {
        scoreScript = GameObject.Find("Main Camera").GetComponent<LevelScoreKeeper>();
	}
	
	// Update is called once per frame
	void Update () {

        // we could shrink the stool over time? or we could meld multiple ones together? or there could be a mesh over multiple 
	
        // can there be some kind of pressure counter which triggers force feedback when a lot of stools are squished tight?

	}

    void OnTriggerEnter(Collider col)
    {
        if(col.transform.tag == "end")
        {
            // reached goal, add points
            scoreScript.score += 1;
            Destroy(this.gameObject, Random.Range(0.1f, 0.4f));
 

        } 
        if(col.transform.tag == "obstacle")
        {
            // wrong exit, destroy stool and no points
            Destroy(this.gameObject, Random.Range(0.1f, 0.4f));
        }
    }
}
