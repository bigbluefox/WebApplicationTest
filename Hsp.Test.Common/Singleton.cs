namespace Hsp.Test.Common
{
    #region 多线程下的单例模式

    /// <summary>
    ///     多线程下的单例模式
    ///     上述代码使用了双重锁方式较好地解决了多线程下的单例模式实现。
    ///     先看内层的if语句块，使用这个语句块时，先进行加锁操作，
    ///     保证只有一个线程可以访问该语句块，进而保证只创建了一个实例。
    ///     再看外层的if语句块，这使得每个线程欲获取实例时不必每次都得加锁，
    ///     因为只有实例为空时（即需要创建一个实例），才需加锁创建，若果已存在一个实例，就直接返回该实例，节省了性能开销
    /// </summary>
    /// <summary>
    ///     单例模式
    /// </summary>
    public class Singleton
    {
        // 定义一个静态变量来保存类的实例
        private static Singleton _singleton;

        // 定义一个标识确保线程同步
        private static readonly object locker = new object();

        // 定义私有构造函数，使外界不能创建该类实例
        private Singleton()
        {
        }

        //定义公有方法提供一个全局访问点。
        public static Singleton GetInstance()
        {
            //这里的lock其实使用的原理可以用一个词语来概括“互斥”这个概念也是操作系统的精髓
            //其实就是当一个进程进来访问的时候，其他进程便先挂起状态
            if (_singleton == null) //区别就在这里
            {
                lock (locker)
                {
                    // 如果类的实例不存在则创建，否则直接返回
                    if (_singleton == null)
                    {
                        _singleton = new Singleton();
                    }
                }
            }
            return _singleton;
        }
    }

    #endregion

    internal sealed class SingletonCSharp
    {
        public static readonly SingletonCSharp Instance = new SingletonCSharp();

        private SingletonCSharp()
        {
        }
    }

    //public sealed class SingletonCSharp
    //{
    //    SingletonCSharp()
    //    {
    //    }

    //    public static SingletonCSharp GetInstance()
    //    {
    //        return Nested.instance;
    //    }

    //    class Nested
    //    {
    //        // Explicit static constructor to tell C# compiler
    //        // not to mark type as beforefieldinit
    //        static Nested()
    //        {
    //        }

    //        internal static readonly SingletonCSharp instance = new SingletonCSharp();
    //    }
    //}
}