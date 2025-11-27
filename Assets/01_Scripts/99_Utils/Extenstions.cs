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
    public static T Random<T>(this T[] array)
    {
        if (array == null || array.Length == 0)
            return default;

        return array[UnityEngine.Random.Range(0, array.Length)];
    }

    public static T[] Shuffle<T>(this T[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            int r = UnityEngine.Random.Range(i, array.Length);
            (array[i], array[r]) = (array[r], array[i]);
        }
        return array;
    }
}