using System.IO.Ports;
using UnityEngine;

public class EndCube : MonoBehaviour
{
    public string portName = "COM4"; // Replace with your Arduino's COM port
    public int baudRate = 115200;     // Match the baud rate in your Arduino code
    private SerialPort serialPort;

    private void Start()
    {
        // Initialize and open the serial port
        serialPort = new SerialPort(portName, baudRate);
        try
        {
            if (!serialPort.IsOpen)
            {
                serialPort.Open();
                Debug.Log("Serial port opened successfully.");
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Failed to open serial port: {e.Message}");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Ensure the trigger is activated by the player
        {
            Debug.Log("Player collided with the end cube. Sending signal to Arduino.");
            if (serialPort != null && serialPort.IsOpen)
            {
                serialPort.WriteLine("play_song"); // Send a signal to the Arduino
            }
        }
    }

    private void OnApplicationQuit()
    {
        // Close the serial port when the application ends
        if (serialPort != null && serialPort.IsOpen)
        {
            serialPort.Close();
        }
    }
}
