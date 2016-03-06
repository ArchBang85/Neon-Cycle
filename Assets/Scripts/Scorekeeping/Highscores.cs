using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public class Highscores : MonoBehaviour {
    //
    /*
    Logic
    Retrieve total score from current cycle of play
    Retrieve high score list
    Figure out where in the list the score goes
    If in the top ten, then place accordingly and display all 10
    Otherwise place at bottom
    Activate loop where the three letters of the name can be set
    Once time has passed, save down score at that position in the master list
    */
    private static FileInfo sourceFile = null;
    private static StreamReader reader = null;
    private static string text = " ";

    public float TimeToEnterName = 20.0f;
    public List<GameObject> names = new List<GameObject>();
    public List<GameObject> scores = new List<GameObject>();
    public GameObject textTemplate;
    public GameObject playerNameText;

    public string[,] griddo;

    string[] playerNameLetters = new string[3];
    // Use this for initialization
    void Start ()
    {
        playerNameLetters[0] = "A";
        playerNameLetters[1] = "A";
        playerNameLetters[2] = "A";

        // retrieve score from current cycle of play
        griddo = readFile("C:\\Users\\Vaio\\Copy\\RotatorGame2\\RotatorGame\\Assets\\Standard Assets\\scores.csv");
        griddo[0,0] = "XXX,";
        // flatten griddo

        // writing back into score file
        FileWriter("C:\\Users\\Vaio\\Copy\\RotatorGame2\\RotatorGame\\Assets\\Standard Assets\\scores2.csv", griddo[0,0]);

        // FileReader.getLine(Application.dataPath + "/StreamingAssets/levels/index.idx", currentLevel);
        // retrieve high score list

        StartCoroutine("CreateTexts",10);
	}


    // compare current score in total list and rank
    int rankScore(int score, int[] allScores)
    {
        int rank = 0;
        for (int i = 0; i<allScores.Length; i++)
        {
            if(score >= allScores[i])
            {
                rank = i;
                return rank;                
            }
                // insert at this position, bump everything else down
        }
        return rank;
    }

	// Update is called once per frame
	void Update () {
        // countdown 	
        TimeToEnterName -= Time.deltaTime;
        if(TimeToEnterName > 0)
        {
            // allow changing of letters
            // Wheel controls

            // need to allow changing of the letter after a small amount of turning, so need to track

            // can there be a minor force feedback element each time a letter actually changes?

            // then send a little back and forth buzz, or move contrary to the direction the rotation is happening 

            // keyboard controls
            if (Input.GetKeyDown(KeyCode.Q))
            {
                playerNameLetters[0] = changeLetter(playerNameLetters[0], false);
            }
            if(Input.GetKeyDown(KeyCode.W))
            {
                playerNameLetters[0] = changeLetter(playerNameLetters[0], true);
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                playerNameLetters[1] = changeLetter(playerNameLetters[1], false);
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                playerNameLetters[1] = changeLetter(playerNameLetters[1], true);
            }

            if (Input.GetKeyDown(KeyCode.Z))
            {
                playerNameLetters[2] = changeLetter(playerNameLetters[2], false);
            }
            if (Input.GetKeyDown(KeyCode.X))
            {
                playerNameLetters[2] = changeLetter(playerNameLetters[2], true);
            }

            playerNameText.GetComponent<TextMesh>().text = playerNameLetters[0] + playerNameLetters[1] + playerNameLetters[2];

        }
    }

    string changeLetter(string inputLetter, bool clockwise)
    {
        string letter = " ";
        string letterList = "ABCDEFGHIJKLMNOPQRSTUVWXYZÅÄÖ! ";
        for (int i = 0; i < letterList.Length; i++)
        {
            if (letterList[i].ToString() == inputLetter)
            {
                if (clockwise)
                {
                    // loop around if necessary
                    if (i == letterList.Length - 1)
                    {
                        letter = letterList[0].ToString();
                    } else
                    {
                        letter = letterList[i + 1].ToString();

                    }
                } else
                {
                    if (i == 0)
                    {
                        letter = letterList[letterList.Length - 1].ToString();
                    }
                    else
                    {
                        letter = letterList[i - 1].ToString();
                    }
                }
            }
        }

        return letter;
    }

    IEnumerator CreateTexts(int t)
    {
        for (int i = 0; i < t; i++)
        {
            yield return new WaitForSeconds(0.03f);
            GameObject nameText = (GameObject)Instantiate(textTemplate, new Vector3(transform.position.x, transform.position.y - i,1), Quaternion.identity);
            names.Add(nameText);
            nameText.GetComponent<TextMesh>().text = "ABC";
            yield return new WaitForSeconds(0.02f);
            GameObject scoreText = (GameObject)Instantiate(textTemplate, new Vector3(transform.position.x + 4, transform.position.y - i,1), Quaternion.identity);
            scores.Add(scoreText);
            scoreText.GetComponent<TextMesh>().text = "9,999";

        }
    }

    IEnumerator UpdateTexts()
    {
        yield return new WaitForSeconds(0.05f);

    }



    public static string[,] readFile(string s)
    {
        int length = 0;
        int width = 0;
        sourceFile = new FileInfo(s);
        reader = sourceFile.OpenText();

        while (true)
        {
            text = reader.ReadLine();
            if (text != null)
            {
                int ticker = 0;

                for (int i = 1; i <= text.Length; i++)
                {

                    if (text.Substring(i - 1, 1) == ",")
                    {
                        ticker++;
                    }
                    else if (text.Substring(i - 1, 1) == ";")
                    {
                        length++;
                        ticker++;
                    }
                }
                if (ticker > width)
                {

                    width = ticker;

                }

            }
            else {
                break;
            }
        }
        string[,] grid = new string[length, width];

        reader.Close();

        sourceFile = new FileInfo(s);

        reader = sourceFile.OpenText();

        for (int i = 0; i < length; i++)
        {

            text = reader.ReadLine();

            if (text != null)
            {

                int ticker = 0;
                int prevStop = 0;

                for (int k = 1; k <= text.Length; k++)
                {

                    if (ticker == width)
                    {
                        continue;
                    }

                    if (text.Substring(k - 1, 1) == "," || text.Substring(k - 1, 1) == ";")
                    {
                        if (k - prevStop > 0)
                        {
                            grid[i, ticker] = text.Substring(prevStop, k - prevStop);
                        }
                        else {
                            grid[i, ticker] = "0";
                        }

                        prevStop = k;
                        ticker++;
                    }
                }
            }

        }

        reader.Close();

        return grid;
    }

    public static void FileWriter(string file, string input)
    {
        File.AppendAllText(file, input);
    }
}
