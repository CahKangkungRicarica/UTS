using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PemancarEvent : MonoBehaviour
{

    public static event Action TekanTombol;
  
    void Update()
    {
        if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Debug.Log("Tombol ditekan, memanggil event TekanTombol");
            TekanTombol?.Invoke();
        }
    }
}
