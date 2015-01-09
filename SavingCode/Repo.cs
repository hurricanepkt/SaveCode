namespace SavingCode
{
    public class Repo
    {
        private static ClassForPersistance _classForPersistance;
        public static void Save(ClassForPersistance useclass)
        {
            _classForPersistance = useclass;
        }

        public static ClassForPersistance First()
        {
            return _classForPersistance;
        }

    }
}