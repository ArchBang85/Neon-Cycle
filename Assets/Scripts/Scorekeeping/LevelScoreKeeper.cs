using UnityEngine;
using System.Collections;

public class LevelScoreKeeper : MonoBehaviour {
    public int score;
    public GameObject scoreText;
	// Use this for initialization
	void Start () {
        if (scoreText == null)
        {
            scoreText = GameObject.Find("ScoreText");
        }
        StartCoroutine("UpdateScore");
	}

    IEnumerator UpdateScore()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            scoreText.GetComponent<TextMesh>().text = score.ToString();
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
