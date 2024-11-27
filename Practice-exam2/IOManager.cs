using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace Practice_exam2
{
    public class IOManager
    {
        private string extension = ".json";

        public void WriteJson(string path, string fileName, Object obj)
        {
            string fullpath = path + @"\" + fileName + extension;
            StreamWriter stream = new StreamWriter(fullpath);
            string content = JsonConvert.SerializeObject(obj);
            stream.Write(content);
            stream.Close();
        }

        public T ReadJson<T>(string path, string fileName)
        {
            string fullpath = path + @"\" + fileName + extension;
            StreamReader stream = new StreamReader(fullpath);
            string content = stream.ReadToEnd();
            stream.Close();
            return JsonConvert.DeserializeObject<T>(content);
        }

        //public void CreateFile(string path, string fileName)
        //{

        //}

        public List<FileInfo> LoadFiles(string path)
        {
            DirectoryInfo folder = new DirectoryInfo(path);
            FileInfo[] files = folder.GetFiles();
            return files.ToList();
        }

        public bool isPathExist(string path)
        {
            return File.Exists(path);
        }
    }
}