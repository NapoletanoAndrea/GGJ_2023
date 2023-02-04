using UnityEngine;

public static class LayerMaskExtensions
{
    public static bool Contains(this LayerMask layermask, int layer) {
        return layermask == (layermask | (1 << layer));
    }
}