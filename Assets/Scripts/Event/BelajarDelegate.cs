using UnityEngine;
using System;

public class BelajarDelegate : MonoBehaviour
{
    public delegate void Kalah();
    public delegate void KoinDiambil();

    public Kalah onKalah;
    public KoinDiambil onKoinDiambil;

    void Start()
    {
        onKalah += PesanKalah;
        onKoinDiambil += PesanKoin;

        onKalah?.Invoke();
        onKoinDiambil?.Invoke();
    }

    void PesanKalah()
    {
        Debug.Log("Game Over!");
    }

    void PesanKoin()
    {
        Debug.Log("Koin diambil!");
    }
    
}

