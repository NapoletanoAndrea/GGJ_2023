using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class TransformExtensions
{
    public static void SetStatic(this Transform transform, bool value) {
        transform.gameObject.isStatic = value;
        foreach (Transform child in transform) {
            child.SetStatic(value);
        }
    }

    public static T[] GetChildrenOfType<T>(this Transform transform) where T : Component
    {
        List<T> children = new();
        if (transform.childCount > 0)
        {
            foreach (Transform child in transform)
            {
                var comp = child.GetComponent<T>();
                if (comp != null)
                {
                    children.Add(comp);
                }
                children.AddRange(child.GetChildrenOfType<T>());
            }
        }
        return children.ToArray();
    }

    public static Transform GetClosest(this IEnumerable<Transform> transforms, Vector3 toCompare)
    {
        transforms = transforms.OrderBy(transform => (transform.position - toCompare).sqrMagnitude);
        return transforms.First();
    }

    public static Vector3 HorizontalForward(this Transform transform)
    {
        var forward = transform.forward;
        forward.y = 0;
        return forward.normalized;
    }

    public static bool IsPartOf(this Transform part, Transform whole)
    {
        return part == whole || part.IsChildOf(whole);
    }
}