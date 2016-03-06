using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class PieceBehaviour : MonoBehaviour {

    public float speed = 0.5f;
    public int pieceColour;
    public Material[] pieceMaterials = new Material[3];
    LevelScoreKeeper scoreScript;
    // Use this for initialization
    void Start () {
        try
        {
            this.GetComponent<Rigidbody>().AddForce((new Vector3(0, 0, 0) - new Vector3(transform.position.x, transform.position.y, 0.0f)) * speed);

        }
        catch
        {
            Debug.Log("no rigidbody found");
        }
        scoreScript = GameObject.Find("Main Camera").GetComponent<LevelScoreKeeper>();
    }
	
	// Update is called once per frame
	void Update () {
        //transform.Translate((new Vector3(0, 0, 0) - new Vector3(transform.position.x, transform.position.y, 0.0f) ) * Time.deltaTime * speed);
      
	}

    public void setColour(int c)
    {
        this.GetComponent<Renderer>().material = pieceMaterials[c];
        pieceColour = c;
        
    }

    void OnTriggerEnter(Collider col)
    {
        Debug.Log(col);
        if (col.transform.tag == "Segment")
        {
        
            if (pieceColour == col.transform.GetComponent<Segment>().segmentColour)
            {
                Debug.Log("Destroying");
                Destroy(this.gameObject, 0.2f);
                GameObject.Find("Main Camera").GetComponent<S_GameController>().piecesEaten += 1;
                scoreScript.score += 1;

                /// override! just making the level change soon for testing
                if (scoreScript.score > 50)
                {
                    int levelCount = SceneManager.sceneCountInBuildSettings;
                    int currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
                    if (currentLevelIndex == levelCount - 1)
                    {
                        currentLevelIndex = -1;
                    }

                    SceneManager.LoadScene(currentLevelIndex + 1);
                }
            }
          /*  else
            {
                this.GetComponent<Rigidbody>().AddForce((new Vector3(transform.position.x, transform.position.y, 0.0f) - new Vector3(0, 0, 0)) * speed);
            }*/
        }

    }
}
