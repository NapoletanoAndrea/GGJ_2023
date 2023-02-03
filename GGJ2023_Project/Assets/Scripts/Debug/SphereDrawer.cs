using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereDrawer : MonoBehaviour
{
    private class SphereData {
        public Vector3 position;
        public float radius;
        public Color color;
    }

    private readonly List<SphereData> sphereDataList = new();
    
    private void OnDrawGizmos() {
        foreach (var sphereData in sphereDataList) {
            Gizmos.color = sphereData.color;
            Gizmos.DrawWireSphere(sphereData.position, sphereData.radius);
        }
    }

    public void DrawSphere(Vector3 position, float radius, float seconds, Color color) {
        var sphereData = new SphereData
        {
            position = position,
            radius = radius,
            color = color
        };
        StartCoroutine(DrawSphereCoroutine(sphereData, seconds));
    }

    private IEnumerator DrawSphereCoroutine(SphereData sphereData, float seconds) {
        sphereDataList.Add(sphereData);
        yield return new WaitForSeconds(seconds);
        sphereDataList.Remove(sphereData);
    }
}
