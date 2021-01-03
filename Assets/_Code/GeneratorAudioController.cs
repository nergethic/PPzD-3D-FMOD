using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using UnityEngine.Assertions;

public class GeneratorAudioController : MonoBehaviour {
    [SerializeField, BankRef] string bankName;
    [SerializeField, EventRef] string generatorEventNameRef;
    [SerializeField] InteriorDetector interiorDetector;
    [SerializeField] bool shouldPlayOutside = true;

    EventInstance generatorEventInstance;

    private void Awake() {
        if (interiorDetector == null) {
            interiorDetector = FindObjectOfType<InteriorDetector>();
        }
        Assert.IsTrue(interiorDetector != null);
        
        RuntimeManager.LoadBank(bankName, true);
        generatorEventInstance = RuntimeManager.CreateInstance(generatorEventNameRef);
        generatorEventInstance.set3DAttributes(RuntimeUtils.To3DAttributes(gameObject));
        generatorEventInstance.start();
    }

    private void OnDestroy() {
        RuntimeManager.UnloadBank(bankName);
    }

    void Update() {
        float volume;
        if (interiorDetector.IsInside)
            volume = shouldPlayOutside ? 0f : .7f;
        else
            volume = shouldPlayOutside ? .7f : 0f;

        generatorEventInstance.setVolume(volume);
    }
}
