using UnityEngine;

public static class Vector3Extensions
{
    public static Vector3[] CardinalsAroundY { get; } = {Vector3.forward, Vector3.back, Vector3.left, Vector3.right};

    public static Vector3 SetVectorCoordinateByAxis(this Vector3 vector, Vector3 axis, float value)
    {
        axis = axis.Abs();
        float max = Mathf.Max(axis.x, axis.y, axis.z);
        if (axis.x == max) vector.x = value;
        if (axis.y == max) vector.y = value;
        if (axis.z == max) vector.z = value;
        return vector;
    }
    
    public static Vector3 SetVectorByAxis(this Vector3 vector, Vector3 axis, Vector3 value)
    {
        axis = axis.Abs();
        float max = Mathf.Max(axis.x, axis.y, axis.z);
        if (axis.x == max) vector.x = value.x;
        if (axis.y == max) vector.y = value.y;
        if (axis.z == max) vector.z = value.z;
        return vector;
    }

    public static float GetCoordinateByAxis(this Vector3 vector, Vector3 axis)
    {
        axis = axis.Abs();
        float max = Mathf.Max(axis.x, axis.y, axis.z);
        if (axis.x == max) return vector.x;
        if (axis.y == max) return vector.y;
        if (axis.z == max) return vector.z;
        return default;
    }

    public static float HorizontalDistance(Vector3 vector1, Vector3 vector2)
    {
        vector2.y = vector1.y;
        return Vector3.Distance(vector1, vector2);
    }

    public static Vector3 HorizontalDirection(Vector3 startVector, Vector3 finalVector)
    {
        startVector.y = finalVector.y;
        return finalVector - startVector;
    }

    public static bool EqualsYZ(this Vector3 firstVector, Vector3 secondVector)
    {
        return firstVector.x == secondVector.x && firstVector.z == secondVector.z;
    }

    public static Vector3 Abs(this Vector3 vector)
    {
        return new Vector3(Mathf.Abs(vector.x), Mathf.Abs(vector.y), Mathf.Abs(vector.z));
    }
}