namespace DesignPatternsLearning.Creational
{

    //Use Cases

    //Singleton pattern as a LoadBalancing object. Only a single instance (the singleton) of the class can be created because servers may dynamically come on- or off-line 
    //and every request must go throught the one object that has knowledge about the state of the (web) farm.

    //Can be used for Thread management as ThreadPool and Logger to write logs to files

    public class Singleton
    {
        private static Singleton instance;
        private static object syncObj = new object();

        private Singleton() { }
        public static Singleton Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncObj)
                    {
                        if (instance == null)
                            instance = new Singleton();
                    }
                }
                return instance;
            }
        }
    }

    public class SingletionWithStaticInstance
    {
        private static SingletionWithStaticInstance instance = new SingletionWithStaticInstance();
        private SingletionWithStaticInstance() { }

        public static SingletionWithStaticInstance Instance
        {
            get
            {
                return instance;
            }
        }
    }

    public class SingletionWithStaticConstructor
    {
        private static SingletionWithStaticConstructor instance;
        private SingletionWithStaticConstructor() { }

        static SingletionWithStaticConstructor()
        {
            instance = new SingletionWithStaticConstructor();
        }
        public static SingletionWithStaticConstructor Instance
        {
            get
            {
                return instance;
            }
        }
    }
}
