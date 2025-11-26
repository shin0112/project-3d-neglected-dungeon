using UnityEngine;

/// <summary>
/// 기본 타입을 확장하는 클래스 모음
/// </summary>

#region Transform
public static class TransformExtension
{
    public static T FindChild<T>(this Transform t, string name) where T : Component
    {
        T[] children = t.gameObject.GetComponentsInChildren<T>(true);
        foreach (T child in children)
        {
            if (child.name == name)
            {
                return child;
            }
        }
        return null;
    }
}
#endregion

public static class ArrayExtension
{
    private static readonly System.Random random = new System.Random();

    public static T Random<T>(this T[] array)
    {
        if (array == null || array.Length == 0)
            return default;

        return array[random.Next(array.Length)];
    }
}