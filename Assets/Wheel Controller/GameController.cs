using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController: MonoBehaviour
{
    public static GameController instance;
    public List<Wheel> wheels = new List<Wheel>(3);
    public int wheelCount = 0;
    private float timer = 0;

	void Awake ()
	{
        instance = this;
        
	}

    void Start()
    {
        ArduinoConnector.instance.Open();
    }

    public void RegisterWheel(Wheel w)
    {
        wheels.Add(w);
        Debug.Log("New Wheel added.");
        wheelCount++;
    }

    public void UpdateWheel(int id, int delta)
    {
        for (int i = 0; i < wheels.Count; i++)
        {
            if (wheels[i].id == id)
            {
                wheels[i].UpdatePosition(delta);
                return;
            }
        }
    }

    public void Slider1(float pos)
    {
        Debug.Log("Slider: " + pos);
        SendMotorSpeed(0, (int) pos);
    }

    public void Slider2(float pos)
    {
        Debug.Log("Slider: " + pos);
        SendMotorSpeed(1, (int)pos);
    }


    public void SendMotorSpeed(int id, int speed)
    {
        if (speed > 1023) speed = 1023;
        if (speed < -1023) speed = -1023;
        if (Mathf.Abs(speed) < 80) speed = 0;
        speed += 1023;
        byte v1 = (byte)(speed % 256);
        byte v2 = (byte)(speed >> 8);
        //Debug.Log("splitting number " + speed + " into " + v1 + " " + v2);
        ArduinoConnector.instance.SendSignal(id * 2, v2);
        ArduinoConnector.instance.SendSignal(id * 2 + 1, v1);
    }

	void Update ()
	{
	    timer += Time.deltaTime;
	    if (timer > 0)
	    {
	        timer = 0;
            ArduinoConnector.instance.WriteToArduino("PING");
            ArduinoConnector.instance.Poke();
        }

	    if (Input.GetKeyDown(KeyCode.Alpha1))
	    {
            SendMotorSpeed(0, 123);
	    }
	}
}
