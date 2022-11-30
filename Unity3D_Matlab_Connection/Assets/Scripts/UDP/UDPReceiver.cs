using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

public class UDPReceiver : MonoBehaviour
{
    public int Port;
    private UdpClient _ReceiveClient;

    private UdpClient _tok1;
    private UdpClient _tok2;
    private UdpClient _tok3;
    private UdpClient _rotor_speed;
    private UdpClient _torque;

    private Thread _ReceiveThread;
    private IReceiverObserver _Observer;

    void Start()
    {
        Initialize();
    }

    /// <summary>
    /// Initialize objects.
    /// </summary>
    public void Initialize()
    {
        // Receive
        _ReceiveThread = new Thread(
            new ThreadStart(ReceiveData));
        _ReceiveThread.IsBackground = true;
        _ReceiveThread.Start();
    }

    public void SetObserver(IReceiverObserver observer)
    {
        _Observer = observer;
    }

    /// <summary>
    /// Receive data with pooling.
    /// </summary>
    private void ReceiveData()
    {
        _ReceiveClient = new UdpClient(Port);
        //_tok1 = new UdpClient(25000);
        _tok2 = new UdpClient(25001);
        _tok3 = new UdpClient(25002);
        _rotor_speed = new UdpClient(25003);
        _torque = new UdpClient(25004);

        while (true)
        {
            try
            {
                //getValues(_tok1);
                getValues(_tok2, "tok2");
                getValues(_tok3, "tok3");
                getValues(_rotor_speed, "rotor_speed");
                getValues(_torque, "torque");
                //IPEndPoint anyIP = new IPEndPoint(IPAddress.Any, 0);
                //byte[] data = _ReceiveClient.Receive(ref anyIP);

                //double[] values = new double[data.Length / 8];
                //Buffer.BlockCopy(data, 0, values, 0, values.Length * 8);

                //if (_Observer != null)
                //    _Observer.OnDataReceived(values);

                //Debug.Log(">>>>");

                //for (int i=0; i <= values.Length-1; i++)
                //{
                //    Debug.Log(values[i]);
                //}
            }
            catch (Exception err)
            {
                Debug.Log("<color=red>" + err.Message + "</color>");
            }
        }
    }

    /// <summary>
    /// Deinitialize everything on quiting the application.Or you might get error in restart.
    /// </summary>
    private void OnApplicationQuit()
    {
        try
        {
            _ReceiveThread.Abort();
            _ReceiveThread = null;
            _ReceiveClient.Close();
        }
        catch (Exception err)
        {
            Debug.Log("<color=red>" + err.Message + "</color>");
        }
    }

    private void getValues(UdpClient client, string name)
    {
        IPEndPoint anyIP = new IPEndPoint(IPAddress.Any, 0);
        byte[] data = client.Receive(ref anyIP);

        double[] values = new double[data.Length / 8];
        Buffer.BlockCopy(data, 0, values, 0, values.Length * 8);

        if (_Observer != null)
            _Observer.OnDataReceived(values);

        Debug.Log(name + ">>>>");

        for (int i = 0; i <= values.Length - 1; i++)
        {
            Debug.Log(values[i]);
        }
    }
}