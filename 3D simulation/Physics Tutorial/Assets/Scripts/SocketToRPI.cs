using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Net.Sockets;

public class SocketToRPI : MonoBehaviour
{

    public string messageString;
    public bool messageSent = false;
    public Socket client;
    TcpClient mySocket;
    public NetworkStream theStream;
    StreamWriter theWriter;
    StreamReader theReader;
    public string hostIP;
    public int hostPort;
    private bool socketReady = false;


    void Start()
    {
        setUpSocket();
    }

    void Update()
    {

        //Debug.Log("Socket Running");
        while (theStream.DataAvailable)
        {                  
            string recievedData = readSocket();
        }

        if (!messageSent)
        {
            messageString = "Unity Connected";
            writeSocket(messageString);
            Debug.Log("RPI connected");
            messageSent = true;
        }
    }


   public void setUpSocket()
    {
        try
        {
            Debug.Log("Setting up On Address:" + hostIP + ", " + hostPort);
            mySocket = new TcpClient(hostIP, hostPort);
            theStream = mySocket.GetStream();
            theWriter = new StreamWriter(theStream);
            theReader = new StreamReader(theStream);
            socketReady = true;
            Debug.Log("Socket set up on IP:" + hostIP);
        }
        catch (Exception e)
        {
            Debug.Log("Socket Error:" + e);
        }
    }

    public void writeSocket(string theLine)
    {            // function to write data out
        if (!socketReady)
            return;
        String tmpString = theLine;
        theWriter.Write(tmpString);
        theWriter.Flush();

    }

    public String readSocket()
    {                        // function to read data in
        if (!socketReady)
            return "";
        if (theStream.DataAvailable)
            return theReader.ReadLine();
        return "NoData";
    }

    public void closeSocket()
    {                            // function to close the socket
        if (!socketReady)
            return;
        theWriter.Close();
        theReader.Close();
        mySocket.Close();
        socketReady = false;
    }

    public void maintainConnection()
    {                    // function to maintain the connection
        if (!theStream.CanRead)
        {
            setUpSocket();
        }
    }

    public void sendAngle(int pin, float angle)
    {
        string pinAngle = pin.ToString() + "," + Mathf.Round(angle).ToString() + "," + pin.ToString() + "_"; // we use the pin as a bit check to make sure the message is correct
        Debug.Log("message sent: " + pinAngle);
        writeSocket(pinAngle);
     
    }

}


