using UnityEngine;
using System.Runtime.InteropServices;
using System;
using UnityEngine.SceneManagement;

public class GiftInteraction : MonoBehaviour
{
    [DllImport("user32.dll")]
    static extern void keybd_event(byte virtualKey, byte scanCode, uint flags, IntPtr extraInfo);
    
    const byte VK_NUMLOCK = 0x90;
    const uint KEYEVENTF_EXTENDEDKEY = 0x1;
    const uint KEYEVENTF_KEYUP = 0x2;
    
    [SerializeField]
    private string endSceneName = "End";
    
    private bool hasTriggered = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasTriggered)
        {
            hasTriggered = true;
            
            // Send HID signal for happy birthday song
            keybd_event(VK_NUMLOCK, 0x45, KEYEVENTF_EXTENDEDKEY, IntPtr.Zero);
            keybd_event(VK_NUMLOCK, 0x45, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP, IntPtr.Zero);
            
            // Immediately load end scene
            SceneManager.LoadScene(endSceneName);
        }
    }
}