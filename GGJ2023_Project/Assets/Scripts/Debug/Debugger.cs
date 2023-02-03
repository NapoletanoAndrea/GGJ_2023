using UnityEngine;

public static class Debugger {
    private static SphereDrawer sphereDrawer;

    private static void Init() {
        if (!sphereDrawer) 
        {
            sphereDrawer = new GameObject().AddComponent<SphereDrawer>();
        }
    }

    public static void DrawSphere(Vector3 position, float radius, float seconds = default, Color color = default) 
    {
        Init();
        
        if (color == default) 
            color = Color.white;
        
        sphereDrawer.DrawSphere(position, radius, seconds, color);
    }
}
