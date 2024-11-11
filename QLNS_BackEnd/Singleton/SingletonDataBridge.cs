using QLNS_BackEnd.Data;

namespace QLNS_BackEnd.Singleton
{
    public class SingletonDataBridge
    {
        private static DataContext uniqueInstance;
        private SingletonDataBridge() {}
        public static DataContext GetInstance()
        {        
            if (uniqueInstance == null)
            {
                uniqueInstance = new DataContext();
            }
            return uniqueInstance;
        }
    }
}
