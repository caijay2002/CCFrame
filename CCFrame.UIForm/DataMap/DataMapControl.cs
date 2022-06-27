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
    public delegate void ClickHandler(object sender, Command.Data.IData data);

    public partial class DataMapControl : UserControl
    {
        public event ClickHandler ClickHandler;

        public Size DataMapSize { get { return dataMap_View.Size; } set { dataMap_View.Size = value; } }

        private string m_SourceKey { get; set; }

        public DataMapControl()
        {
            InitializeComponent();
            //默认读取DataMap
            m_SourceKey = "DataMap";
        }

        public void SetSourceKey(string key)
        {
            m_SourceKey = key;
        }

        public async void ReflashData()
        {
            dataMap_View.DataSource = await GetData();
        }

        /// <summary>
        /// 刷新数据
        /// </summary>
        /// <param name="datas"></param>
        public Task<List<Command.Data.IData>> GetData()
        {
            return Task.Run(() => DataCacheSvr.GetDataList("DataMap"));
            //dataMap_View.DataSource = dataList;
        }

        private void dataMap_View_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (dataMap_View.SelectedRows[0].DataBoundItem is Command.Data.IData selectedItem)
            //{
            //    txt_Address.Text = selectedItem.Address;
            //}
            //else
            //{
            //    //MessageBox.Show("请选择要修改的对象");
            //}
        }

        private void btn_ChangeValue_Click(object sender, EventArgs e)
        {
            try
            {
                var selectedItem = dataMap_View.SelectedRows[0].DataBoundItem as CCFrame.Command.Data.IData;

                int selectedIndex = dataMap_View.SelectedRows[0].Index;

                string value = txt_Value.Text;

                selectedItem.Value = Value;

                if (selectedItem.DataType == Command.Data.DataType.Int32 || selectedItem.DataType == Command.Data.DataType.Short)
                {
                    int result = 0;
                    var status = int.TryParse(value, out result);
                    if (!status)
                    {
                        CCFrame.Log.LogSvr.Error(selectedItem.Address + "输入的数据类型不正确");
                        return;
                    }
                    selectedItem.Value = result;
                }

                DataCacheSvr.UpdateCache(m_SourceKey, selectedItem.Address, value);

                if (ClickHandler != null) ClickHandler(sender, selectedItem);

                //DataCacheSvr.UpdateCache("DataMap", selectedItem.Address, value);

                ReflashData();
                //dataMap_View.DataSource = await ReflashData();

                dataMap_View.Rows[selectedIndex].Selected = true;
            }
            catch (Exception ex)
            {
                CCFrame.Log.LogSvr.Error("数据修改错误" + ex.Message);
            }
        }

        private void Btn_Reflash_Click(object sender, EventArgs e)
        {

            ReflashData();
            //if (ClickHandler != null) ClickHandler(sender, e);
        }
    }
}
