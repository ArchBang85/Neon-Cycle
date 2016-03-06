using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("Loading next level");
            LoadNextLevel();
        }
	}

    void LoadNextLevel()
    {
        int levelCount = SceneManager.sceneCountInBuildSettings;
        int currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        if(currentLevelIndex == levelCount -1)
        {
            currentLevelIndex = -1;
        }

        SceneManager.LoadScene(currentLevelIndex + 1);
    }
}
