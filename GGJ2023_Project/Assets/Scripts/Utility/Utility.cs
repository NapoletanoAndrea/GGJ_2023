using System;
using System.Collections;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public static class Utility
{
    public static IEnumerator TransformPositionLerpCoroutine(Transform targetTransform, Vector3 finalVector,
        float lerpSeconds, AnimationCurve curve = null, Action action = null, Action<Transform> action2 = null)
    {
        float count = 0;
        float t = 0;

        float curveTime = 0;
        if (curve != null)
        {
            curveTime = curve.keys[curve.length - 1].time;
        }

        Vector3 startVector = targetTransform.position;

        while (count <= lerpSeconds)
        {
            count += Time.deltaTime;
            t += Time.deltaTime / lerpSeconds;
            if (curve != null)
            {
                float curveCount = count * curveTime / lerpSeconds;
                t = curve.Evaluate(curveCount);
            }
            targetTransform.position = Vector3.Lerp(startVector, finalVector, t);
            yield return null;
        }

        targetTransform.position = finalVector;
        action?.Invoke();
        action2?.Invoke(targetTransform);
    }

    public static IEnumerator TransformPositionLerpCoroutine(Transform targetTransform, Vector3 startVector, Vector3 finalVector, AnimationCurve curve, float startSeconds = 0f, bool inverse = false)
    {
        float count = startSeconds;
        float lerpSeconds = curve.keys[curve.length - 1].time;

        if (!inverse)
        {
            while (count <= lerpSeconds)
            {
                count += Time.deltaTime;
                targetTransform.position = Vector3.Lerp(startVector, finalVector, curve.Evaluate(count));
                yield return null;
            }

            targetTransform.position = finalVector;
        }
        else
        {
            while (count >= 0)
            {
                count -= Time.deltaTime;
                targetTransform.position = Vector3.Lerp(startVector, finalVector, curve.Evaluate(count));
                yield return null;
            }

            targetTransform.position = startVector;
        }
    }

    public static IEnumerator TransformScaleLerpCoroutine(Transform targetTransform, Vector3 finalVector, float lerpSeconds, Action action = null)
    {
        float count = 0;
        float t = 0;

        Vector3 startVector = targetTransform.localScale;

        while (count <= lerpSeconds)
        {
            count += Time.deltaTime;
            t += Time.deltaTime / lerpSeconds;
            targetTransform.localScale = Vector3.Lerp(startVector, finalVector, t);
            yield return null;
        }

        targetTransform.localScale = finalVector;
        action?.Invoke();
    }

    public static IEnumerator TransformRotationLerpCoroutine(Transform targetTransform, Quaternion finalRotation, float lerpSeconds, Action action = null)
    {
        float count = 0;
        float t = 0;

        Quaternion startRotation = targetTransform.rotation;

        while (count <= lerpSeconds)
        {
            count += Time.deltaTime;
            t += Time.deltaTime / lerpSeconds;
            targetTransform.rotation = Quaternion.Lerp(startRotation, finalRotation, t);
            yield return null;
        }

        targetTransform.rotation = finalRotation;
        action?.Invoke();
    }

    public static IEnumerator ImageColorAlphaLerpCoroutine(Image image, float targetAlpha, float lerpSeconds)
    {
        float count = 0;
        float t = 0;

        float startAlpha = image.color.a;

        Color color = image.color;

        while (count <= lerpSeconds)
        {
            count += Time.deltaTime;
            t += Time.deltaTime / lerpSeconds;
            color.a = Mathf.Lerp(startAlpha, targetAlpha, t);
            image.color = color;
            yield return null;
        }

        color.a = targetAlpha;
        image.color = color;
    }

    public static IEnumerator ImageColorAlphaLerpCoroutine(RawImage image, float targetAlpha, float lerpSeconds)
    {
        float count = 0;
        float t = 0;

        float startAlpha = image.color.a;

        Color color = image.color;

        while (count <= lerpSeconds)
        {
            count += Time.deltaTime;
            t += Time.deltaTime / lerpSeconds;
            color.a = Mathf.Lerp(startAlpha, targetAlpha, t);
            image.color = color;
            yield return null;
        }

        color.a = targetAlpha;
        image.color = color;
    }

    public static float HorizontalDistance(Vector3 v1, Vector3 v2)
    {
        v1.y = v2.y;
        return Vector3.Distance(v1, v2);
    }

    public static Vector3 GetClosestPointOnFiniteLine(Vector3 point, Vector3 start, Vector3 end)
    {
        Vector3 direction = end - start;
        float magnitude = direction.magnitude;
        direction.Normalize();
        float projectLength = Mathf.Clamp(Vector3.Dot(point - start, direction), 0f, magnitude);
        return start + direction * projectLength;
    }

    public static IEnumerator InvokeWithFrameDelay(Action method, int frameDelay = 1)
    {
        for (int i = 0; i < frameDelay; i++)
        {
            yield return new WaitForEndOfFrame();
        }
        method?.Invoke();
    }

#if UNITY_EDITOR
    private static MethodInfo clearConsoleMethod;
    private static MethodInfo ClearConsoleMethod
    {
        get
        {
            if (clearConsoleMethod == null)
            {
                Assembly assembly = Assembly.GetAssembly(typeof(SceneView));
                Type logEntries = assembly.GetType("UnityEditor.LogEntries");
                clearConsoleMethod = logEntries.GetMethod("Clear");
            }
            return clearConsoleMethod;
        }
    }

    public static void ClearLogConsole()
    {
        ClearConsoleMethod.Invoke(new object(), null);
    }
#endif

    // public static T[] GetAllInstances<T>() where T : ScriptableObject
    // {
    //     string[] guids = AssetDatabase.FindAssets("t:" + typeof(T).Name);
    //     var a = new T[guids.Length];
    //     for (int i = 0; i < guids.Length; i++)
    //     {
    //         string path = AssetDatabase.GUIDToAssetPath(guids[i]);
    //         a[i] = AssetDatabase.LoadAssetAtPath<T>(path);
    //     }
    //     return a;
    // }

}