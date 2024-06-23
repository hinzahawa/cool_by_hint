using Syncfusion.XlsIO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Syncfusion.XlsIO;
using System.Drawing;
using System.Data;
using cool_cal_by_hint.masterData;


namespace cool_hint
{
    /// <summary>
    /// Interaction logic for Condenser.xaml
    /// </summary>
    public partial class Condenser : Window
    {
        private string[] modelList = ["ECH-452-D-23", "ECH-452-E-28", "ECH-502-C-30"];
        private string[] specList = ["400V 3 Ph 50 Hz"];
        private int[] powerList = [50, 60];
        private string[] finMaterailList = ["Alumunum Fin", "Blue Coated Fin", "Copper Fin"];
        private string[] refrigerantList = ["R134a", "R507", "R22", "R404A", "R407C"];
        private List<int> ambientTempList = new List<int>();
        private int[] altitudeList = [0, 50, 100, 200, 300, 500];
        private int TD = 0;
        public DataPayload dataPayload = new DataPayload();
        public Condenser()
        {
            InitializeComponent();
            SpecProducts SpecProducts = new SpecProducts();
            SpecProducts.Products[] specProductList = SpecProducts.specProductList;

            SetDefualtValue();
        }

        private void single_unit_cal_checkbox_Checked(object sender, RoutedEventArgs e)
        {
            selected_model.IsEnabled = single_unit_cal_ischecked.IsChecked.Value;
            selected_model.SelectedIndex = single_unit_cal_ischecked.IsChecked.Value ? 0 : -1;
            selected_spec.IsEnabled = single_unit_cal_ischecked.IsChecked.Value;
            selected_spec.SelectedIndex = single_unit_cal_ischecked.IsChecked.Value ? 0 : -1;
            dataPayload.IsSingleUnitCal = single_unit_cal_ischecked.IsChecked.Value;

        }
        private void EnabledCalButton(bool isEnabled = true)
        {
            calculate_btn.IsEnabled = isEnabled;
        }
        private void SetDefualtValue()
        {
            // init data
            for (int i = 25; i <= 50; i++)
            {
                ambientTempList.Add(i);

            }
            dataPayload.CondenserCapacity = "70";
            dataPayload.CondensingTemp = "45"; // CondensingTemp - selected_ambient = TD
            dataPayload.TD = TD;
            dataPayload.power = powerList[0];
            dataPayload.finMaterial = finMaterailList[0];
            dataPayload.ambientTemp = ambientTempList[10];

            condensing_temp_input.Text = dataPayload.CondensingTemp;
            condenser_cap_input.Text = dataPayload.CondenserCapacity;
            td_input.Text = dataPayload.TD.ToString();

            selected_model.ItemsSource = modelList;
            selected_spec.ItemsSource = specList;
            selected_power.ItemsSource = powerList;
            selected_fin.ItemsSource = finMaterailList;
            selected_refrigerant.ItemsSource = refrigerantList;
            selected_ambient.ItemsSource = ambientTempList;
            selected_ambient.SelectedIndex = 10;
            selected_altitude.ItemsSource = altitudeList;
        }
        private void condenser_cap_input_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                //bool isNumber = Regex.IsMatch(condenser_cap_input.Text, @"^\d+$");
                int numericvalue;
                bool isNumber = int.TryParse(condenser_cap_input.Text, out numericvalue);
                if (isNumber)
                {
                    dataPayload.CondenserCapacity = condenser_cap_input.Text;
                    EnabledCalButton(true);
                }
                else
                {
                    EnabledCalButton(false);
                }

            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message.ToString());
                EnabledCalButton(false);
            }
        }
        private void condensing_temp_input_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                bool isNumber = Regex.IsMatch(condensing_temp_input.Text, @"^\d+$");
                if (isNumber != true || int.Parse(condensing_temp_input.Text)<0)
                {
                    condensing_temp_input.Text = "";
                    EnabledCalButton(false);
                }
                else
                {
                    dataPayload.CondensingTemp = condensing_temp_input.Text;
                    int TD = int.Parse(dataPayload.CondensingTemp) - dataPayload.ambientTemp;
                    dataPayload.TD = TD;
                    td_input.Text = dataPayload.TD.ToString();
                    EnabledCalButton(true);
                }

            }
            catch (Exception)
            {
                //MessageBox.Show(ex.Message.ToString());
                EnabledCalButton(false);
            }

        }
        private void powerOnchange(object sender, RoutedEventArgs e)
        {
             try
             {
                 dataPayload.power = powerList[selected_power.SelectedIndex];
             }
             catch (Exception )
             {
                 dataPayload.power = powerList[0];
                 selected_power.SelectedIndex = 0;
             }
            
        }

        private void finMaterialOnchange(object sender, SelectionChangedEventArgs e)
        {
             try
             {
                 dataPayload.finMaterial = finMaterailList[selected_fin.SelectedIndex];
             }
             catch (Exception )
             {
                 dataPayload.finMaterial = finMaterailList[0];
                 selected_fin.SelectedIndex = 0;
             }
        }

        private void selected_refrigerant_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
              try
              {
                  dataPayload.refrigerant = refrigerantList[selected_refrigerant.SelectedIndex];
              }
              catch (Exception ex)
              {
                  dataPayload.refrigerant = refrigerantList[0];
                  selected_refrigerant.SelectedIndex = 0;
              }
        }

        private void selected_ambient_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                dataPayload.ambientTemp = ambientTempList[selected_ambient.SelectedIndex];
                int TD = int.Parse(dataPayload.CondensingTemp) - dataPayload.ambientTemp;
                dataPayload.TD = TD;
                td_input.Text = dataPayload.TD.ToString();
            }
            catch (Exception ex)
            {
                dataPayload.ambientTemp = ambientTempList[0];
                selected_ambient.SelectedIndex = 0;
            }
        }

        private void selected_altitude_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
             try
             {
                 dataPayload.altitude = altitudeList[selected_altitude.SelectedIndex];
             }
             catch (Exception ex)
             {
                 dataPayload.altitude = altitudeList[0];
                 selected_altitude.SelectedIndex = 0;
             }
        }

        private void selected_model_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
             try
             {
                 dataPayload.model = modelList[selected_model.SelectedIndex];
             }
             catch (Exception ex)
             {
                 dataPayload.model = modelList[0];
                 selected_model.SelectedIndex = 0;
             }
        }

        private void selected_spec_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                dataPayload.spec = specList[selected_spec.SelectedIndex];
            }
            catch (Exception ex)
            {
                dataPayload.spec = specList[0];
                selected_spec.SelectedIndex = 0;
            }
        }

        private void td_input_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                dataPayload.TD = int.Parse(td_input.Text);
            }
            catch (Exception)
            {
                dataPayload.TD = int.Parse(dataPayload.CondensingTemp) - dataPayload.ambientTemp;
                td_input.Text = dataPayload.TD.ToString();
            }
        }

        private void calculate_btn_Click(object sender, RoutedEventArgs e)
        {
            if(dataPayload.TD <  6 || dataPayload.TD > 16)
            {
                MessageBox.Show("TD Must be between 6 - 16");
                return;
            }

         

        }
    }
    public class DataPayload
    {
        public bool IsSingleUnitCal { get; set; }
        public string model { get; set; }
        public string spec { get; set; }
        public string CondenserCapacity { get; set; }
        public string CondensingTemp { get; set; }
        public int TD { get; set; }
        public int power { get; set; }
        public string finMaterial { get; set; }
        public string refrigerant { get; set; }
        public int ambientTemp { get; set; }
        public int altitude { get; set; }

        /*public string data {
            get
            {
                return CondenserCapacity;
            } 
            set
            {
                CondenserCapacity = value;
            }
        }*/


    }

    static class Constants
    {
        public const double Pi = 3.14159;
        public const int SpeedOfLight = 300000; // km per sec.
    }
}
