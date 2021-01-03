using System;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using UnityEngine.Assertions;
using UnityStandardAssets.Characters.FirstPerson;

public class InteriorDetector : MonoBehaviour {
    [SerializeField] FirstPersonController player;
    [SerializeField, BankRef] string ambienceBankName;
    [SerializeField, EventRef] string ambienceEventName;
    const string ROOM_LAYER_NAME = "Room";

    EventInstance generatorEventInstance;
    bool previousProcessedState;
    int activeEnteredRoomColliders;

    int roomLayer;
    public bool IsInside { private set; get; } = false;

    private void Start() {
        roomLayer = LayerMask.NameToLayer(ROOM_LAYER_NAME);
        Assert.IsTrue(roomLayer >= 0);
        IsInside = false;
        previousProcessedState = IsInside;
        
        RuntimeManager.LoadBank(ambienceBankName, true);
        generatorEventInstance = RuntimeManager.CreateInstance(ambienceEventName);
        generatorEventInstance.set3DAttributes(RuntimeUtils.To3DAttributes(gameObject));
        generatorEventInstance.start();

        UpdateSoundSettings();
    }

    private void OnDestroy() {
        RuntimeManager.UnloadBank(ambienceBankName);
    }

    public void Update() {
        if (previousProcessedState != IsInside) {
            previousProcessedState = IsInside;
            UpdateSoundSettings();
        }
    }

    void UpdateSoundSettings() {
        if (IsInside) {
            generatorEventInstance.setVolume(0.7f);
            player.UpdateCurrentFootstepsPlacementState(EntityPlacementState.Interior);
        } else {
            generatorEventInstance.setVolume(0.0f);
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