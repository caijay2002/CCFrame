
namespace CCFrame.UIForm.DataMap
{
    partial class DataMapControl
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.Btn_Reflash = new System.Windows.Forms.Button();
            this.txt_Value = new System.Windows.Forms.TextBox();
            this.txt_Address = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_ChangeValue = new System.Windows.Forms.Button();
            this.dataMap_View = new System.Windows.Forms.DataGridView();
            this.Address = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Length = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DataType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TimeStamp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PLCDataType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataMap_View)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.Btn_Reflash);
            this.panel1.Controls.Add(this.txt_Value);
            this.panel1.Controls.Add(this.txt_Address);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btn_ChangeValue);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 527);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(818, 53);
            this.panel1.TabIndex = 17;
            // 
            // Btn_Reflash
            // 
            this.Btn_Reflash.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_Reflash.Location = new System.Drawing.Point(679, 8);
            this.Btn_Reflash.Margin = new System.Windows.Forms.Padding(4);
            this.Btn_Reflash.Name = "Btn_Reflash";
            this.Btn_Reflash.Size = new System.Drawing.Size(115, 37);
            this.Btn_Reflash.TabIndex = 21;
            this.Btn_Reflash.Text = "刷新数值";
            this.Btn_Reflash.UseVisualStyleBackColor = true;
            this.Btn_Reflash.Click += new System.EventHandler(this.Btn_Reflash_Click);
            // 
            // txt_Value
            // 
            this.txt_Value.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txt_Value.Location = new System.Drawing.Point(318, 14);
            this.txt_Value.Margin = new System.Windows.Forms.Padding(4);
            this.txt_Value.Name = "txt_Value";
            this.txt_Value.Size = new System.Drawing.Size(148, 23);
            this.txt_Value.TabIndex = 20;
            // 
            // txt_Address
            // 
            this.txt_Address.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txt_Address.Enabled = false;
            this.txt_Address.Location = new System.Drawing.Point(80, 14);
            this.txt_Address.Margin = new System.Windows.Forms.Padding(4);
            this.txt_Address.Name = "txt_Address";
            this.txt_Address.Size = new System.Drawing.Size(208, 23);
            this.txt_Address.TabIndex = 19;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 18);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 17);
            this.label1.TabIndex = 18;
            this.label1.Text = "地址：";
            // 
            // btn_ChangeValue
            // 
            this.btn_ChangeValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_ChangeValue.Location = new System.Drawing.Point(542, 8);
            this.btn_ChangeValue.Margin = new System.Windows.Forms.Padding(4);
            this.btn_ChangeValue.Name = "btn_ChangeValue";
            this.btn_ChangeValue.Size = new System.Drawing.Size(115, 37);
            this.btn_ChangeValue.TabIndex = 17;
            this.btn_ChangeValue.Text = "修改数值";
            this.btn_ChangeValue.UseVisualStyleBackColor = true;
            this.btn_ChangeValue.Click += new System.EventHandler(this.btn_ChangeValue_Click);
            // 
            // dataMap_View
            // 
            this.dataMap_View.AllowUserToAddRows = false;
            this.dataMap_View.AllowUserToDeleteRows = false;
            this.dataMap_View.AllowUserToResizeColumns = false;
            this.dataMap_View.AllowUserToResizeRows = false;
            this.dataMap_View.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataMap_View.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Address,
            this.Length,
            this.Value,
            this.DataType,
            this.TimeStamp,
            this.PLCDataType,
            this.Description});
            this.dataMap_View.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataMap_View.Location = new System.Drawing.Point(0, 0);
            this.dataMap_View.Margin = new System.Windows.Forms.Padding(4);
            this.dataMap_View.MultiSelect = false;
            this.dataMap_View.Name = "dataMap_View";
            this.dataMap_View.ReadOnly = true;
            this.dataMap_View.RowTemplate.Height = 23;
            this.dataMap_View.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataMap_View.Size = new System.Drawing.Size(818, 527);
            this.dataMap_View.TabIndex = 18;
            this.dataMap_View.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataMap_View_CellClick);
            // 
            // Address
            // 
            this.Address.DataPropertyName = "Address";
            this.Address.HeaderText = "地址";
            this.Address.Name = "Address";
            this.Address.ReadOnly = true;
            this.Address.Width = 200;
            // 
            // Length
            // 
            this.Length.DataPropertyName = "Length";
            this.Length.HeaderText = "长度";
            this.Length.Name = "Length";
            this.Length.ReadOnly = true;
            this.Length.Visible = false;
            this.Length.Width = 60;
            // 
            // Value
            // 
            this.Value.DataPropertyName = "Value";
            this.Value.HeaderText = "数值";
            this.Value.Name = "Value";
            this.Value.ReadOnly = true;
            // 
            // DataType
            // 
            this.DataType.DataPropertyName = "DataType";
            this.DataType.HeaderText = "数据类型";
            this.DataType.Name = "DataType";
            this.DataType.ReadOnly = true;
            this.DataType.Width = 80;
            // 
            // TimeStamp
            // 
            this.TimeStamp.DataPropertyName = "TimeStamp";
            this.TimeStamp.HeaderText = "刷新时间";
            this.TimeStamp.Name = "TimeStamp";
            this.TimeStamp.ReadOnly = true;
            // 
            // PLCDataType
            // 
            this.PLCDataType.DataPropertyName = "PLCDataType";
            this.PLCDataType.HeaderText = "读取类型";
            this.PLCDataType.Name = "PLCDataType";
            this.PLCDataType.ReadOnly = true;
            this.PLCDataType.Visible = false;
            // 
            // Description
            // 
            this.Description.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Description.DataPropertyName = "Description";
            this.Description.HeaderText = "描述";
            this.Description.Name = "Description";
            this.Description.ReadOnly = true;
            // 
            // DataMapControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dataMap_View);
            this.Controls.Add(this.panel1);
            this.Name = "DataMapControl";
            this.Size = new System.Drawing.Size(818, 580);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataMap_View)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button Btn_Reflash;
        private System.Windows.Forms.TextBox txt_Value;
        private System.Windows.Forms.TextBox txt_Address;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_ChangeValue;
        private System.Windows.Forms.DataGridView dataMap_View;
        private System.Windows.Forms.DataGridViewTextBoxColumn Address;
        private System.Windows.Forms.DataGridViewTextBoxColumn Length;
        private System.Windows.Forms.DataGridViewTextBoxColumn Value;
        private System.Windows.Forms.DataGridViewTextBoxColumn DataType;
        private System.Windows.Forms.DataGridViewTextBoxColumn TimeStamp;
        private System.Windows.Forms.DataGridViewTextBoxColumn PLCDataType;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
    }
}
