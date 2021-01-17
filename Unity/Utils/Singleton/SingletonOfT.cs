using System;
using System.Reflection;

namespace Verbess.Utils.Singleton
{
    public abstract class Singleton<T> : ISingletonInit where T : Singleton<T>
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
                            instance = CreateSingletonInstance();
                        }
                    }
                }
                return instance;
            }
            protected set => instance = value;
        }

        static Singleton()
        {
            instance = default(T);
            synclock = new object();
        }

        protected Singleton() { }

        public virtual void Initialize() { }

        public virtual void Dispose()
        {
            instance = null;
        }

        private static T CreateSingletonInstance()
        {
            Type type = typeof(T);
            ConstructorInfo[] constructorInfos = type.GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic);
            ConstructorInfo constructorInfo = Array.Find<ConstructorInfo>(constructorInfos, a => a.GetParameters().Length == 0);
            if (constructorInfo == null)
            {
                throw new Exception($"There is no Non-Public Non-Parameters Constructor in {type}");
            }
            else
            {
                object instance = constructorInfo.Invoke(null);
                T t = instance as T;
                return t;
            }
        }
    }
}