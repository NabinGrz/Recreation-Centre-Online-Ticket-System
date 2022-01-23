using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.ComponentModel;
using Newtonsoft.Json;

namespace Coursework
{
public class Utility
    {
        //declaring a path for data to be stored
        private static string _filePath = "Visitors.txt";
        private static string _filePathPrice = "Price.txt";



        //methods to write and read the datas of visitorsd detail and price
        public static string WriteToText(string data)
        {
            if (!File.Exists(_filePath))
            {
                File.Create(_filePath);
            }

            using (StreamWriter outputFile = new StreamWriter(_filePath))
            {
                outputFile.WriteLine(data);
            }


            return "victory";
            Console.WriteLine("Nabin");
        }

        public static string WriteToTextPrice(string data)
        {
            if (!File.Exists(_filePathPrice))
            {
                File.Create(_filePathPrice);
            }

            using (StreamWriter outputFile = new StreamWriter(_filePathPrice))
            {
                outputFile.WriteLine(data);
            }


            return "victory";
            Console.WriteLine("Nabin");
        }

        public static string ReadToText()
        {
            if (File.Exists(_filePath))
            {
                string data1 = File.ReadAllText(_filePath);
                return data1;
            }
            return null;
        }

        public static string ReadToTextPrice()
        {
            if (File.Exists(_filePathPrice))
            {
                string data1 = File.ReadAllText(_filePathPrice);
                return data1;
            }
            return null;
        }

    }
}
