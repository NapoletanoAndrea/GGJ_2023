using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MonoBehaviourExtensions
{
    private static void SafeStopCoroutine(this MonoBehaviour mono, IEnumerator coroutine)
    {
        if (coroutine != null)
        {
            mono.StopCoroutine(coroutine);
        }
    }
    
    public static void StopCoroutines(this MonoBehaviour mono, IEnumerable<IEnumerator> coroutines)
    {
        foreach (var cor in coroutines)
        {
            mono.SafeStopCoroutine(cor);
        }
    }

    public static void StartCoroutine(this MonoBehaviour mono, IEnumerator coroutine, out IEnumerator result)
    {
        result = coroutine;
        mono.StartCoroutine(coroutine);
    }
}