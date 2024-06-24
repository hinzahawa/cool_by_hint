using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace cool_cal_by_hint.masterData
{
    internal static partial class SpecProducts
    {
        public static List<Product> specProductList = new List<Product>();
        public static void initData()
        {
            try
            {
                // อ่านไฟล์ JSON
                string jsonFilePath = "./masterData/spec_products.json";
                string jsonData = File.ReadAllText(jsonFilePath);
                // แปลง JSON เป็น List ของ objects
                specProductList = JsonConvert.DeserializeObject<List<Product>>(jsonData);

            }
            catch (IOException ex)
            {
                // จัดการเมื่อเกิดข้อผิดพลาดในการอ่านไฟล์
                Console.WriteLine("Error reading the file: " + ex.Message);
            }
        }
    }
}
