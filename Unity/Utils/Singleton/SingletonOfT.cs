namespace Verbess.Utils.Singleton
{
    public abstract class Singleton<T> : ISingleton where T : Singleton<T>
    {
        private static T instance;
        private static readonly object synclock;

        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (synclock)
                    {
                        if (instance == null)
                        {
                            instance = SingletonCreator.CreateSingleton<T>();
                        }
                    }
                }
                return instance;
            }
        }

        static Singleton()
        {
            instance = null;
            synclock = new object();
        }

        protected Singleton() { }

        public virtual void Initialize() { }

        public virtual void Dispose()
        {
            instance = null;
        }
    }
}