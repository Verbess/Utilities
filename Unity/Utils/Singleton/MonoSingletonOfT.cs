using UnityEngine;

namespace Verbess.Utils.Singleton
{
    public abstract class MonoSingleton<T> : MonoBehaviour, ISingleton where T : MonoSingleton<T>
    {
        private static T instance;
        private static bool isShuttingDown = false;
        private static readonly object synclock = new object();

        public static T Instance
        {
            get
            {
                lock (synclock)
                {
                    if ((instance == null) && !isShuttingDown)
                    {
                        instance = MonoSingletonCreator.CreateMonoSingleton<T>();
                    }
                }
                return instance;
            }
        }

        public static bool IsShuttingDown
        {
            get
            {
                return isShuttingDown;
            }
            protected set
            {
                isShuttingDown = value;
            }
        }

        public virtual void Initialize() { }

        public virtual void Dispose()
        {
            Destroy(gameObject);
        }

        protected virtual void OnApplicationQuit()
        {
            isShuttingDown = true;
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