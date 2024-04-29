using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace Sparta_TextRpg
{
    internal class DataManager
    {
        public static DataManager Instance = new DataManager();
        public static List<Item> Items { get; private set; }

        static DataManager()
        {
            Items = new List<Item>
            {
            };
        }
    }
}


