using UnityEngine;

public class DoorAudioController : MonoBehaviour {
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip doorOpen;
    [SerializeField] AudioClip doorClose;
    
    public void OnDoorOpen() {
        audioSource.PlayOneShot(doorOpen);
    }
    
    public void OnDoorClosed() {
        audioSource.PlayOneShot(doorClose);
    }
}
