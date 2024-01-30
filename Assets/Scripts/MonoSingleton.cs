using UnityEngine;

public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    public static T Instance
    {
        get
        {
            if (!instance)
                FindInstance();
            return instance;
        }
    }

    private static T instance;

    private static void Create()
    {
        if (instance)
            return;

        var go = new GameObject(typeof(T).Name);
        instance = go.AddComponent<T>();
    }

    private static void FindInstance()
    {
        if (instance)
            return;

        instance = FindObjectOfType<T>();
    }
}