using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace PandaBear
{
    public class Program
    {
        public static string[] infoParser()
        {
            JObject reader = JObject.Parse(File.ReadAllText("info.json"));

            string version = (string)reader["Version"];
            string creator = (string)reader["Creator"];
            string dependencies = (string)reader["Dependencies"];

            List<string> toArray = new List<string>();
            toArray.Add(version);
            toArray.Add(creator);
            toArray.Add(dependencies);

            return toArray.ToArray();
        }
        static void Main(string[] args)
        {
            Console.WriteLine("");
        }
    }
}
