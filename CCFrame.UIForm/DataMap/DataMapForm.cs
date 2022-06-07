using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CCFrame.UIForm.DataMap
{
    public partial class DataMapForm : Form
    {
        public DataMapForm()
        {
            InitializeComponent();
        }

        private async void ReflashData()
        {
            //dataGridView1.DataSource = await Task.Run(() => DataCacheSvr.GetDataList("DataMap"));
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var selectedItem = dataGridView1.SelectedRows[0].DataBoundItem as CCFrame.Driver.MXPlcData;

            txt_Address.Text = selectedItem.Address;
        }

        private void btn_ChangeValue_Click(object sender, EventArgs e)
        {
            try
            {
                var selectedItem = dataGridView1.SelectedRows[0].DataBoundItem as CCFrame.Driver.MXPlcData;

                int selectedIndex = dataGridView1.SelectedRows[0].Index;

                string value = txt_Value.Text;

                if (selectedItem.DataType == CCFrame.Command.Data.DataType.Int32)
                {
                    int result = 0;
                    var status = int.TryParse(value, out result);
                    if (!status)
                    {
                        CCFrame.Log.LogSvr.Error(selectedItem.Address + "输入的数据类型不正确");
                        return;
                    }
                }

                //DataCacheSvr.UpdateCache("DataMap", selectedItem.Address, value);

                ReflashData();

                dataGridView1.Rows[selectedIndex].Selected = true;
            }
            catch (Exception ex)
            {
                CCFrame.Log.LogSvr.Error("数据修改错误" + ex.Message);
            }
        }

        private void DataMapForm_Shown(object sender, EventArgs e)
        {
            ReflashData();
        }

        private async void Btn_Reflash_Click(object sender, EventArgs e)
        {
            //dataGridView1.DataSource = await Task.Run(() => DataCacheSvr.GetDataList("DataMap"));
        }
    }
}
