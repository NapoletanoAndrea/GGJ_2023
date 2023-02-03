using UnityEngine;

public class GenericEntityAudio : EntityAudio 
{
    [SerializeField] private UnitySerializedDictionary<string, AudioCueSO> audioCueDictionary;
    [SerializeField] private Transform audioEmitter;

    private void Awake() 
    {
        if (!audioEmitter) 
        {
            audioEmitter = transform;
        }
    }

    public void PlayAudio(string audioCueKey) 
    {
        if (!audioCueDictionary.TryGetValue(audioCueKey, out var audioCue))
        {
            Debug.LogWarning($"{audioCueKey} is not present in the dictionary");
            return;
        }
        if (audioCue == null) {
            Debug.LogWarning($"{audioCueKey} has no value");
            return;
        }
        PlayAudio(audioCue, audioEmitter.position);
    }
}
