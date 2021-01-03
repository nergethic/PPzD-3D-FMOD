using System;
using UnityEngine;
using FMODUnity;

public class DoorAudioController : MonoBehaviour {
    [SerializeField] StudioBankLoader bankLoader;
    [SerializeField, BankRef] private string doorBankName;
    [SerializeField, EventRef] string doorOpenEventNameRef; 
    [SerializeField, EventRef] string doorCloseEventNameRef;

    private void Awake() {
        bankLoader.Load();
    }

    private void OnDestroy() {
        bankLoader.Unload();
    }

    public void OnDoorOpen() {
        RuntimeManager.PlayOneShot(doorOpenEventNameRef);
    }
    
    public void OnDoorClosed() {
        RuntimeManager.PlayOneShot(doorCloseEventNameRef);
    }
}
