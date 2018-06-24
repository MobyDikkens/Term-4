using System;
using System.IO;

namespace Thailand.Helpers
{
    public static class TownResolver
    {
        private const string NamesPath = "Resourses/names.csv";

        private static string _file;

        static TownResolver()
        {
            _file = File.ReadAllText(NamesPath);
        }


        public static string GetName(int id)
        {
            string strId = Convert.ToString(id);
            int start = _file.IndexOf(strId) + strId.Length + 1;
            int end = _file.IndexOf("\r\n", start);

            string name = _file.Substring(start, end - start);

            return name;
        }
    }
}
