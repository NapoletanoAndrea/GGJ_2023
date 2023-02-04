using UnityEngine;

public static class ColorExtensions
{
    public static Color GetAlphaColor(this Color color, float alpha)
    {
        Color tmp = color;
        tmp.a = Mathf.Clamp01(alpha);
        return tmp;
    }
}