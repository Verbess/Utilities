using UnityEngine;

namespace Verbess.Utils.Singleton
{
    public abstract class MonoSingleton<T> : MonoBehaviour, ISingletonInit where T : MonoSingleton<T>
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
                        instance = CreateMonoSingletonInstance();
                    }
                }
                return instance;
            }
            protected set
            {
                instance = value;
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

        private static T CreateMonoSingletonInstance()
        {
            T instance = null;

            if (!Application.isPlaying)
            {
                return instance;
            }
            else
            {
                instance = UnityEngine.Object.FindObjectOfType<T>();
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