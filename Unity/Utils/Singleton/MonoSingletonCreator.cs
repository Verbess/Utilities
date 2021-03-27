using UnityEngine;

namespace Utils.Singleton
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

                if (instance == null)
                {
                    GameObject obj = new GameObject(typeof(T).Name);
                    instance = obj.AddComponent<T>();
                }

                return instance;
            }
        }
    }
}