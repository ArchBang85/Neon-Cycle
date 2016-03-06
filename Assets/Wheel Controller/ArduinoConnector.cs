/* ArduinoConnector by Alan Zucconi
 * http://www.alanzucconi.com/?p=2979
 */
using UnityEngine;
using System;
using System.Collections;
using System.IO.Ports;
using UnityEngine.Networking.NetworkSystem;

public class ArduinoConnector : MonoBehaviour {

    /* The serial port where the Arduino is connected. */
    [Tooltip("The serial port where the Arduino is connected")]
    public string port = "COM4";
    /* The baudrate of the serial port. */
    [Tooltip("The baudrate of the serial port")]
    public int baudrate = 9600;

    private SerialPort stream;

    public static ArduinoConnector instance;

    void Awake()
    {
        instance = this;
    }

    public void Open () {
        try
        {
            // Opens the serial port
            var ports = SerialPort.GetPortNames();
            stream = new SerialPort("\\\\.\\" + ports[ports.Length - 1], baudrate);
            stream.ReadTimeout = 1;
            stream.Open();
            //this.stream.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
        }
        catch
        {
            Debug.Log("Error opening Arduino connection.");
        }
    }

    public void WriteToArduino(string message)
    {
        try
        {
            // Send the request
            stream.WriteLine(message);
            stream.BaseStream.Flush();
        }
        catch
        {

        }
    }

    public string ReadFromArduino(int timeout = 0)
    {
        stream.ReadTimeout = timeout;
        try
        {
            return stream.ReadLine();
        }
        catch (TimeoutException)
        {
            return null;
        }
    }

    private void HandleWheelString(String s)
    {
        if (s.Length > 0)
        {
            var result = s.Split(' ');
            if (result.Length != 4)
            {
                Debug.LogWarning("Not three wheels detected: " + result.Length + "|" + s + "|");
                return;
            }
            for (int i = 0; i < 3; i++)
            {
                GameController.instance.UpdateWheel(i, Int32.Parse(result[i]));
            }
        }
    }

    public void Poke()
    {
        //Debug.Log(ReadFromArduino(1));
        //return;

        StartCoroutine
        (
            AsynchronousReadFromArduino
            (HandleWheelString,                 // Callback
                () => Debug.LogError("Error!"), // Error callback
                10f                             // Timeout (seconds)
            )
        );
    }
    

    public IEnumerator AsynchronousReadFromArduino(Action<string> callback, Action fail = null, float timeout = float.PositiveInfinity)
    {
        DateTime initialTime = DateTime.Now;
        DateTime nowTime;
        TimeSpan diff = default(TimeSpan);

        string dataString = null;

        do
        {
            // A single read attempt
            try
            {
                dataString = stream.ReadLine();
            }
            catch (TimeoutException)
            {
                dataString = null;
            }

            if (dataString != null)
            {

                nowTime = DateTime.Now;
                callback(dataString);
                yield return null;
            } else
                yield return new WaitForSeconds(0.05f);

            nowTime = DateTime.Now;
            diff = nowTime - initialTime;

        } while (diff.Milliseconds < timeout);

        //if (fail != null)
        //    fail();
        yield return null;
    }

    public void Close()
    {
        stream.Close();
    }

    public void SendSignal(int control, byte v1)
    {
        WriteToArduino("ECHO " + control + " " + v1);
    }
}