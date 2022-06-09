using CCFrame.Core;
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
    public partial class DataMapControl : UserControl
    {
        public EventHandler ClickHandler;

        public Size DataMapSize { get { return dataMap_View.Size; } set { dataMap_View.Size = value; } }

        public DataMapControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 刷新数据
        /// </summary>
        /// <param name="datas"></param>
        public void ReflashData(List<CCFrame.Command.Data.IData> datas)
        {
            dataMap_View.DataSource = datas;
        }

        private void dataMap_View_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataMap_View.SelectedRows[0].DataBoundItem is CCFrame.Driver.MXPlcData selectedItem)
            {
                txt_Address.Text = selectedItem.Address;
            }
            else
            {
                //MessageBox.Show("请选择要修改的对象");
            }
        }

        private void btn_ChangeValue_Click(object sender, EventArgs e)
        {
            try
            {
                var selectedItem = dataMap_View.SelectedRows[0].DataBoundItem as CCFrame.Command.Data.IData;

                int selectedIndex = dataMap_View.SelectedRows[0].Index;

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

                DataCacheSvr.UpdateCache("DataMap", selectedItem.Address, value);

                if (ClickHandler != null) ClickHandler(sender, e);

                //DataCacheSvr.UpdateCache("DataMap", selectedItem.Address, value);

                //ReflashData();

                dataMap_View.Rows[selectedIndex].Selected = true;
            }
            catch (Exception ex)
            {
                CCFrame.Log.LogSvr.Error("数据修改错误" + ex.Message);
            }
        }
    }
}
