using UnityEngine;
using UnityEngine.Assertions;
using UnityStandardAssets.Characters.FirstPerson;

public class InteriorDetector : MonoBehaviour {
    [SerializeField] FirstPersonController player;
    [SerializeField] AudioSource ambienceAudioSource;
    [SerializeField] AudioReverbZone reverbZone;
    const string ROOM_LAYER_NAME = "Room";

    bool previousProcessedState;
    int activeEnteredRoomColliders;

    int roomLayer;
    public bool IsInside { private set; get; } = false;

    private void Start() {
        roomLayer = LayerMask.NameToLayer(ROOM_LAYER_NAME);
        Assert.IsTrue(roomLayer >= 0);

        previousProcessedState = IsInside;
    }

    public void Update() {
        if (previousProcessedState != IsInside) {
            previousProcessedState = IsInside;
            UpdateSoundSettings();
        }
    }

    void UpdateSoundSettings() {
        if (IsInside) {
            reverbZone.gameObject.SetActive(true);
            ambienceAudioSource.spatialBlend = 1f;
            player.UpdateCurrentFootstepsPlacementState(EntityPlacementState.Interior);
        } else {
            reverbZone.gameObject.SetActive(false);
            ambienceAudioSource.spatialBlend = 0f;
            player.UpdateCurrentFootstepsPlacementState(EntityPlacementState.Outside);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.layer == roomLayer) {
            activeEnteredRoomColliders++;
            UpdateInsideState();
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.layer == roomLayer) {
            activeEnteredRoomColliders--;
            UpdateInsideState();
        }
    }

    void UpdateInsideState() {
        IsInside = activeEnteredRoomColliders > 0;
    }
}