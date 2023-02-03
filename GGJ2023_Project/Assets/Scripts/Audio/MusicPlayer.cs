using UnityEngine;

public enum FadeType{ Cross, Full }

public class MusicPlayer : MonoBehaviour {
    [SerializeField] private AudioCueSO playOnStart;
    [SerializeField] private FadeType fadeType;
    [SerializeField] private float fadeSeconds;

    private void Start() {
        AudioManager.Instance?.PlayMusic(playOnStart, fadeSeconds, fadeType);
    }
}