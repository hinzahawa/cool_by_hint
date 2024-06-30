using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static cool_cal_by_hint.masterData.SpecProducts;

namespace cool_cal_by_hint.masterData
{
    internal static partial class SpecProducts
    {
        public static List<Product> specProductList = new List<Product>();
        public static Product selectedProduct = new Product();
        private static List<SpecDeatil> specDetailList = new List<SpecDeatil>();
        private static List<Capacity> capacityList = new List<Capacity>();

        /*public static void initData()
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
        }*/


        public static void initData()
        {
            string excelFilePath = "./masterData/spec_products.xlsx";
            string jsonData = ConvertExcelToJson(excelFilePath);
            specDetailList = JsonConvert.DeserializeObject<List<SpecDeatil>>(jsonData);
            jsonData = ConvertExcelToJson(excelFilePath, true);
            capacityList = JsonConvert.DeserializeObject<List<Capacity>>(jsonData);
            specProductList = SetProductsValue();
        }
        public static string ConvertExcelToJson(string excelFilePath, bool isCapacity = false)
        {
            List<Dictionary<string, object>> jsonData = new List<Dictionary<string, object>>();

            try
            {
                using (SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Open(excelFilePath, false))
                {
                    WorkbookPart workbookPart = spreadsheetDocument.WorkbookPart;
                    Sheet sheets = workbookPart.Workbook.Descendants<Sheet>().ElementAtOrDefault(isCapacity ? 1 : 0); // sheet1,2
                    WorksheetPart worksheetPart = (WorksheetPart)workbookPart.GetPartById(sheets.Id);
                    //IEnumerable<Sheet> sheets = workbookPart.Workbook.Descendants<Sheet>();
                    // WorksheetPart worksheetPart = (WorksheetPart)workbookPart.GetPartById(sheets.First().Id);

                    SheetData sheetData = worksheetPart.Worksheet.Elements<SheetData>().First();
                    IEnumerable<Row> rows = sheetData.Elements<Row>();

                    // Get headers from the first row
                    Row headerRow = rows.First();
                    List<string> headers = new List<string>();
                    foreach (Cell cell in headerRow.Elements<Cell>())
                    {
                        headers.Add(GetCellValue(workbookPart, cell));
                    }

                    // Convert each row (except the first row) to Dictionary
                    foreach (Row row in rows.Skip(1))
                    {
                        Dictionary<string, object> rowDict = new Dictionary<string, object>();
                        int cellIndex = 0;
                        foreach (Cell cell in row.Elements<Cell>())
                        {
                            if (cellIndex > headers.Count - 1) break;
                            string header = headers[cellIndex];
                            string cellValue = GetCellValue(workbookPart, cell);
                            rowDict[header] = cellValue;
                            cellIndex++;
                        }
                        jsonData.Add(rowDict);

                    }
                }

                // Serialize to JSON using Newtonsoft.Json
                return JsonConvert.SerializeObject(jsonData, Formatting.Indented);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return null;
            }
        }
        private static string GetCellValue(WorkbookPart workbookPart, Cell cell)
        {
            SharedStringTablePart stringTablePart = workbookPart.SharedStringTablePart;
            string value = cell.InnerText;

            if (cell.DataType != null && cell.DataType.Value == CellValues.SharedString)
            {
                return stringTablePart.SharedStringTable.ChildElements[int.Parse(value)].InnerText;
            }
            else
            {
                return value;
            }
        }
        private static List<Product> SetProductsValue(double room_temp=18)
        {

            List<Product> products = new List<Product>();
            foreach (var item in specDetailList)
            {
                string keyModel = item.model.Replace("-", "");
                Capacity capacity = capacityList.FirstOrDefault((c => Convert.ToDouble(c.air_inlet) == room_temp));
                products.Add(item: new Product
                {
                    model = item.model,//KA-4001-7EB
                    refrigerant = item.refrigerant,
                    capacity = Convert.ToDouble(capacity.GetType().GetProperty(keyModel).GetValue(capacity, null)),
                    room_temp = room_temp,
                    evaporation_temp = Convert.ToDouble(capacity.evap_temp),
                    superheating = 5.0,
                    condensation_temp = 45.5,
                    suction_gas_temp = 20.0,
                    subcooling = 0.0,
                    surface = RoundUp(item.surface),
                    tube_volume = RoundUp(item.tube_volume),
                    fin_pitch = int.Parse(item.fin_spacing),
                    test_pressure = int.Parse(item.test_pressure),
                    air_flow = int.Parse(item.air_flow),
                    air_throw = int.Parse(item.distance),
                    fan_diameter = int.Parse(item.diameter),
                    number_of_fan = int.Parse(item.number_of_fan),
                    voltage = item.voltage,
                    fan_speed = int.Parse(item.fan_speed),
                    power = int.Parse(item.input_power),
                    current = RoundUp(item.current),
                    noise_level_in_3m = int.Parse(item.noise_level_in_3m),
                    tube_material = item.tube_material,
                    fin_material = item.fin_material,
                    casing_material = item.casing_material,
                    inlet_connection = item.liquid,
                    outlet_connection = item.gas,
                    dry_weight = RoundUp(item.ta_7),
                    L = item.L,
                    E1 = item.E1,
                    E2 = item.E2,
                    E3 = item.E3,
                    E4 = item.E4,
                    H = item.H,
                    W = item.W,
                    B = item.B,
                    electric_defrost_for_coil = item.coil,
                    electric_defrost_for_tray = item.water_tray,
                    drainage = item.drainage,
                    price = RoundUp(item.price),
                    total = RoundUp(item.amount),
                    image_1 = GetPathImage(item.number_of_fan),
                    image_2 = GetPathImage(item.number_of_fan,"2")
                });

            }
            return products;
        }
        private static string GetPathImage(string number_of_fan="1", string imageNumber="1")
        {
            return $"./assets/fan{number_of_fan}-{imageNumber}.jpg";
        }

        private static string AddSeparator(string value)
        {
            string newvalue = string.Empty;
            try
            {
                newvalue = Convert.ToInt32(value).ToString("N");
            }
            catch
            {
                newvalue = value.ToString();
            }
            return newvalue;
        }
        public static double RoundUp(string input, int places = 2)
        {
            double multiplier = Math.Pow(10, Convert.ToDouble(places));

            return Math.Ceiling(Convert.ToDouble(input) * multiplier) / multiplier;
        }
        public static void SetSelectedProduct(int index=-1)
        {
            if (index >-1)
            {
            selectedProduct = specProductList[index];

            }
        }

    }

}