using UnityEngine;

public static class ColliderExtensions
{
    public static bool IsWithinHorizontalBoundsBy(this BoxCollider boxCollider, Vector3 point, float radius)
    {
        if (boxCollider.bounds.Contains(point))
        {
            var closestPoint = boxCollider.ClosestHorizontalPointOnBounds(point);
            if (Vector3.Distance(point, closestPoint) >= radius)
            {
                return true;
            }
        }
        return false;
    }

    public static Vector3 ClosestHorizontalPointOnBounds(this BoxCollider boxCollider, Vector3 point, float sizeDifference = 0)
    {
        Vector3[] directions = Vector3Extensions.CardinalsAroundY;

        var bounds = boxCollider.bounds;
        var center = bounds.center;
        center.y = Mathf.Clamp(point.y, bounds.min.y, bounds.max.y);
        
        Vector3 closestPoint = boxCollider.center;
        float minDistance = Mathf.Infinity;
        
        for (int i = 0; i < directions.Length; i++)
        {
            var boundsPoint = center + directions[i] * (bounds.extents.GetCoordinateByAxis(directions[i]) + sizeDifference);
            var distance = Vector3.Distance(point, boundsPoint);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestPoint = boundsPoint;
            }
        }
        return closestPoint;
    }
}