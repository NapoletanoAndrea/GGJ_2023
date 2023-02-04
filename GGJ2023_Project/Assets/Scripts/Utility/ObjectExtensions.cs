using UnityEngine;

public static class ObjectExtensions
{
    public static Component GetOrAddComponent<T>(this Object obj) where T : Component {
        var gameObject = obj as GameObject;
        if(!gameObject) {
            gameObject = (obj as Component)?.gameObject;
        }
        if (gameObject) {
            return !gameObject.TryGetComponent<T>(out var comp) ? gameObject.AddComponent<T>() : comp;
        }
        return null;
    }
}