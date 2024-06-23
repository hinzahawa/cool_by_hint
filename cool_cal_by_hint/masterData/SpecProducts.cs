﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace cool_cal_by_hint.masterData
{
    internal class SpecProducts
    {
        public Products[] specProductList = [];
        public class Products
        {
            public string model { get; set; }
            public string refrigerant { get; set; }
            public double capacity { get; set; }
            public double room_temp { get; set; }
            public double evaporation_temp { get; set; }
            public double superheating { get; set; }
            public double condensation_temp { get; set; }
            public double suction_gas_temp { get; set; }
            public double subcooling { get; set; }
            public double surface { get; set; }
            public double tube_volume { get; set; }
            public double fin_pitch { get; set; }
            public double test_pressure { get; set; }
            public double air_flow { get; set; }
            public double air_throw { get; set; }
            public double fan_diameter { get; set; }
            public int number_of_fan { get; set; }
            public string voltage { get; set; }
            public double fan_speed { get; set; }
            public double poer { get; set; }
            public double noise_level_in_3m { get; set; }
            public string tube_material { get; set; }
            public string fin_material { get; set; }
            public string casing_material { get; set; }
            public string inlet_connection { get; set; }
            public string outlet_connection { get; set; }
            public double dry_weight { get; set; }
            public double L { get; set; }
            public double E1 { get; set; }
            public double H { get; set; }
            public double W { get; set; }
            public double B { get; set; }
            public string electric_defrost_for_coil { get; set; }
            public string electric_defrost_for_tray { get; set; }
            public string drainage { get; set; }
            public double price { get; set; }
            public double total { get; set; }

        }
        public void initData()
        {
            try
            {
                // อ่านไฟล์ JSON
                string jsonFilePath = "./spec_products.json";
                string jsonData = File.ReadAllText(jsonFilePath);
                // แปลง JSON เป็น List ของ objects
                List<Products> specProductList = JsonConvert.DeserializeObject<List<Products>>(jsonData);

            }
            catch (IOException ex)
            {
                // จัดการเมื่อเกิดข้อผิดพลาดในการอ่านไฟล์
                Console.WriteLine("Error reading the file: " + ex.Message);
            }
        }
    }
}