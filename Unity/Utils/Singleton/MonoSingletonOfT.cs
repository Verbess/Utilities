using UnityEngine;

namespace Utils.Singleton
{
    public abstract class MonoSingleton<T> : MonoBehaviour, ISingleton where T : MonoSingleton<T>
    {
        private static T instance;
        private static bool isApplicationQuit = false;
        private static readonly object synclock = new object();

        public static T Instance
        {
            get
            {
                lock (synclock)
                {
                    if ((instance == null) && !isApplicationQuit)
                    {
                        instance = MonoSingletonCreator.CreateMonoSingleton<T>();
                    }
                }
                return instance;
            }
        }

        public static bool IsApplicationQuit
        {
            get
            {
                return isApplicationQuit;
            }
            protected set
            {
                isApplicationQuit = value;
            }
        }

        public virtual void Initialize() { }

        public virtual void Dispose()
        {
            Destroy(gameObject);
        }

        protected virtual void OnApplicationQuit()
        {
            isApplicationQuit = true;

            if (instance != null)
            {
                Destroy(instance.gameObject);
                instance = null;
            }
        }

        protected virtual void OnDestroy()
        {
            instance = null;
        }
    }
}