using UnityEngine;
using UnityEngine.Assertions;

public class GeneratorAudioController : MonoBehaviour {
    [SerializeField] AudioSource generatorAudioSource;
    [SerializeField] InteriorDetector interiorDetector;
    [SerializeField] bool shouldPlayOutside = true;

    private void Awake() {
        if (interiorDetector == null) {
            interiorDetector = FindObjectOfType<InteriorDetector>();
        }
        Assert.IsTrue(interiorDetector != null);
    }

    void Update() {
        if (interiorDetector.IsInside) {
            generatorAudioSource.volume = shouldPlayOutside ? 0f : 1f;
        } else {
            generatorAudioSource.volume = shouldPlayOutside ? 1f : 0f;
        }
    }
}
