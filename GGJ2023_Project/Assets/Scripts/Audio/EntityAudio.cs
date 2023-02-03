using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityAudio : MonoBehaviour {
    protected void PlayAudio(AudioCueSO audioCue, Vector3 positionInSpace) {
        if (AudioManager.Instance) {
            if (audioCue) {
                AudioManager.Instance.PlaySFX(audioCue, positionInSpace);    
            }
        }
    }
}
