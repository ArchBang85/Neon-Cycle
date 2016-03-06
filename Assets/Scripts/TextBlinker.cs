using UnityEngine;
using System.Collections;

public class TextBlinker : MonoBehaviour {

    public bool startingText = false;
    public GameObject nextText;
    public float initialDelay = 1.2f;
    int[] fibo =  {2, 3, 5, 8, 13, 21, 34, 55, 89};
	// Use this for initialization
	void Start () {
        if(startingText)
        {
            FireText();
        }
	}
	
	// Update is called once per frame
	void Update () {
    }

    public void FireText()
    {
        StartCoroutine("Blink", initialDelay);
    }

    // Rather hand built method for making the instructional texts blink
    IEnumerator Blink(float delay)
    {
        this.transform.GetComponent<MeshRenderer>().enabled = false;

        yield return new WaitForSeconds(delay);
        RendererToggle();
        for (int i = 0; i < fibo.Length; i++)
        {
            yield return new WaitForSeconds(delay / fibo[i]);
            RendererToggle();
        }
        RendererToggle();
        yield return new WaitForSeconds(delay);
        RendererToggle();

        for (int i = 0; i < 6; i++)
        {
            yield return new WaitForSeconds(delay / 10);
            RendererToggle();
        }

        yield return new WaitForSeconds(delay / 2);
        if(nextText != null)
        {
            nextText.GetComponent<TextBlinker>().FireText();
        }
    }

    // Toggle text visibility
    void RendererToggle()
    {
        if (this.transform.GetComponent<MeshRenderer>().enabled == true)
        {
            this.transform.GetComponent<MeshRenderer>().enabled = false;
        }
        else
        {
            this.transform.GetComponent<MeshRenderer>().enabled = true;
        }
    }
}
