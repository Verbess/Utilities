using UnityEngine;

namespace Verbess.Utils.Singleton
{
    internal static class MonoSingletonCreator
    {
        public static T CreateMonoSingleton<T>() where T : MonoBehaviour, ISingleton
        {
            T instance = null;

            if (!Application.isPlaying)
            {
                return instance;
            }
            else
            {
                instance = Object.FindObjectOfType<T>();
                if (instance != null)
                {
                    return instance;
                }
                else
                {
                    GameObject gameObject = new GameObject(typeof(T).Name);
                    instance = gameObject.AddComponent<T>();

                    return instance;
                }
            }
        }
    }
}