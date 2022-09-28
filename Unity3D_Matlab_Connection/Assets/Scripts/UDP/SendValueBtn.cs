using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendValueBtn : MonoBehaviour
{
    [SerializeField]
    UDPTransmitter transmitter;
    
    public void Send_values()
    {
        transmitter.Send(80f);
    }
}
