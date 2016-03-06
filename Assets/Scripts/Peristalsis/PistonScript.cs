using UnityEngine;
using System.Collections;

public class PistonScript : MonoBehaviour {

    public int group = 1;
    public float trajectory = 0f;
    public bool forward = true;
    public int orientation = 1;


	// Use this for initialization
	void Start () {
        SetWheelIcons();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void SetWheelIcons()
    {
        if (group == 1)
        {
            // large wheel

            foreach (Transform t in transform)
            {
                Debug.Log(t.name);
                if (t.name == "WheelIconLarge")
                {
                    t.gameObject.SetActive(true);
                }
                if (t.name == "WheelIconMedium")
                {
                    t.gameObject.SetActive(false);
                }
                if (t.name == "WheelIconSmall")
                {
                    t.gameObject.SetActive(false);
                }
            }
        }
        else if (group == 2)
        {
            // medium wheel
            foreach (Transform t in transform)
            {
                Debug.Log(t.name);
                if (t.name == "WheelIconLarge")
                {
                    t.gameObject.SetActive(false);
                }
                if (t.name == "WheelIconMedium")
                {
                    t.gameObject.SetActive(true);
                }
                if (t.name == "WheelIconSmall")
                {
                    t.gameObject.SetActive(false);
                }
            }
        }
        else
        {
            // small wheel
            foreach (Transform t in transform)
            {
                Debug.Log(t.name);
                if (t.name == "WheelIconLarge")
                {
                    t.gameObject.SetActive(false);
                }
                if (t.name == "WheelIconMedium")
                {
                    t.gameObject.SetActive(false);
                }
                if (t.name == "WheelIconSmall")
                {
                    t.gameObject.SetActive(true);
                }
            }
        }
    }



}
