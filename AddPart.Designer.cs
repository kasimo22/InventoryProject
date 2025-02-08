namespace Inventory_Project
{
    partial class AddPart
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.partTitleLabel = new System.Windows.Forms.Label();
            this.rbtnInHouse = new System.Windows.Forms.RadioButton();
            this.rbtnOutsourced = new System.Windows.Forms.RadioButton();
            this.idLabel = new System.Windows.Forms.Label();
            this.nameLabel = new System.Windows.Forms.Label();
            this.inventoryLabel = new System.Windows.Forms.Label();
            this.priceLabel = new System.Windows.Forms.Label();
            this.maxLabel = new System.Windows.Forms.Label();
            this.minLabelAdd = new System.Windows.Forms.Label();
            this.companyOrMachineLabel = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.txtID = new System.Windows.Forms.TextBox();
            this.txtMax = new System.Windows.Forms.TextBox();
            this.txtMin = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtInventory = new System.Windows.Forms.TextBox();
            this.txtPrice = new System.Windows.Forms.TextBox();
            this.txtCompanyOrMachine = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.toolTipValidation = new System.Windows.Forms.ToolTip(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // partTitleLabel
            // 
            this.partTitleLabel.AutoSize = true;
            this.partTitleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.partTitleLabel.Location = new System.Drawing.Point(12, 13);
            this.partTitleLabel.Name = "partTitleLabel";
            this.partTitleLabel.Size = new System.Drawing.Size(59, 16);
            this.partTitleLabel.TabIndex = 0;
            this.partTitleLabel.Text = "Add Part";
            // 
            // rbtnInHouse
            // 
            this.rbtnInHouse.AutoSize = true;
            this.rbtnInHouse.Checked = true;
            this.rbtnInHouse.Location = new System.Drawing.Point(121, 12);
            this.rbtnInHouse.Name = "rbtnInHouse";
            this.rbtnInHouse.Size = new System.Drawing.Size(68, 17);
            this.rbtnInHouse.TabIndex = 1;
            this.rbtnInHouse.TabStop = true;
            this.rbtnInHouse.Text = "In-House";
            this.rbtnInHouse.UseVisualStyleBackColor = true;
            this.rbtnInHouse.CheckedChanged += new System.EventHandler(this.RbtnInHouse_CheckedChanged);
            // 
            // rbtnOutsourced
            // 
            this.rbtnOutsourced.AutoSize = true;
            this.rbtnOutsourced.Location = new System.Drawing.Point(208, 12);
            this.rbtnOutsourced.Name = "rbtnOutsourced";
            this.rbtnOutsourced.Size = new System.Drawing.Size(80, 17);
            this.rbtnOutsourced.TabIndex = 2;
            this.rbtnOutsourced.TabStop = true;
            this.rbtnOutsourced.Text = "Outsourced";
            this.rbtnOutsourced.UseVisualStyleBackColor = true;
            this.rbtnOutsourced.CheckedChanged += new System.EventHandler(this.RbtnOutsourced_CheckedChanged);
            // 
            // idLabel
            // 
            this.idLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.idLabel.AutoSize = true;
            this.idLabel.Location = new System.Drawing.Point(83, 18);
            this.idLabel.Name = "idLabel";
            this.idLabel.Size = new System.Drawing.Size(18, 13);
            this.idLabel.TabIndex = 3;
            this.idLabel.Text = "ID";
            // 
            // nameLabel
            // 
            this.nameLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.nameLabel.AutoSize = true;
            this.nameLabel.Location = new System.Drawing.Point(66, 68);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(35, 13);
            this.nameLabel.TabIndex = 4;
            this.nameLabel.Text = "Name";
            // 
            // inventoryLabel
            // 
            this.inventoryLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.inventoryLabel.AutoSize = true;
            this.inventoryLabel.Location = new System.Drawing.Point(50, 118);
            this.inventoryLabel.Name = "inventoryLabel";
            this.inventoryLabel.Size = new System.Drawing.Size(51, 13);
            this.inventoryLabel.TabIndex = 5;
            this.inventoryLabel.Text = "Inventory";
            // 
            // priceLabel
            // 
            this.priceLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.priceLabel.AutoSize = true;
            this.priceLabel.Location = new System.Drawing.Point(38, 168);
            this.priceLabel.Name = "priceLabel";
            this.priceLabel.Size = new System.Drawing.Size(63, 13);
            this.priceLabel.TabIndex = 6;
            this.priceLabel.Text = "Price / Cost";
            // 
            // maxLabel
            // 
            this.maxLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.maxLabel.AutoSize = true;
            this.maxLabel.Location = new System.Drawing.Point(74, 218);
            this.maxLabel.Name = "maxLabel";
            this.maxLabel.Size = new System.Drawing.Size(27, 13);
            this.maxLabel.TabIndex = 7;
            this.maxLabel.Text = "Max";
            // 
            // minLabelAdd
            // 
            this.minLabelAdd.AutoSize = true;
            this.minLabelAdd.Location = new System.Drawing.Point(211, 284);
            this.minLabelAdd.Name = "minLabelAdd";
            this.minLabelAdd.Size = new System.Drawing.Size(24, 13);
            this.minLabelAdd.TabIndex = 8;
            this.minLabelAdd.Text = "Min";
            // 
            // companyOrMachineLabel
            // 
            this.companyOrMachineLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.companyOrMachineLabel.AutoSize = true;
            this.companyOrMachineLabel.Location = new System.Drawing.Point(39, 270);
            this.companyOrMachineLabel.Name = "companyOrMachineLabel";
            this.companyOrMachineLabel.Size = new System.Drawing.Size(62, 13);
            this.companyOrMachineLabel.TabIndex = 9;
            this.companyOrMachineLabel.Text = "Machine ID";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.companyOrMachineLabel, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.nameLabel, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.inventoryLabel, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.maxLabel, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.priceLabel, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.idLabel, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(15, 66);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(104, 303);
            this.tableLayoutPanel1.TabIndex = 10;
            // 
            // txtID
            // 
            this.txtID.Location = new System.Drawing.Point(125, 81);
            this.txtID.Name = "txtID";
            this.txtID.ReadOnly = true;
            this.txtID.Size = new System.Drawing.Size(199, 20);
            this.txtID.TabIndex = 11;
            // 
            // txtMax
            // 
            this.txtMax.Location = new System.Drawing.Point(125, 281);
            this.txtMax.Name = "txtMax";
            this.txtMax.Size = new System.Drawing.Size(72, 20);
            this.txtMax.TabIndex = 12;
            // 
            // txtMin
            // 
            this.txtMin.Location = new System.Drawing.Point(252, 281);
            this.txtMin.Name = "txtMin";
            this.txtMin.Size = new System.Drawing.Size(72, 20);
            this.txtMin.TabIndex = 13;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(125, 131);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(199, 20);
            this.txtName.TabIndex = 14;
            // 
            // txtInventory
            // 
            this.txtInventory.Location = new System.Drawing.Point(125, 181);
            this.txtInventory.Name = "txtInventory";
            this.txtInventory.Size = new System.Drawing.Size(199, 20);
            this.txtInventory.TabIndex = 15;
            // 
            // txtPrice
            // 
            this.txtPrice.Location = new System.Drawing.Point(125, 231);
            this.txtPrice.Name = "txtPrice";
            this.txtPrice.Size = new System.Drawing.Size(199, 20);
            this.txtPrice.TabIndex = 16;
            // 
            // txtCompanyOrMachine
            // 
            this.txtCompanyOrMachine.Location = new System.Drawing.Point(125, 333);
            this.txtCompanyOrMachine.Name = "txtCompanyOrMachine";
            this.txtCompanyOrMachine.Size = new System.Drawing.Size(199, 20);
            this.txtCompanyOrMachine.TabIndex = 17;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(159, 380);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 18;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.BtnSaveAdd_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(249, 380);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 19;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.BtnCancelAdd_Click);
            // 
            // AddPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 458);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtCompanyOrMachine);
            this.Controls.Add(this.txtPrice);
            this.Controls.Add(this.txtInventory);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.txtMin);
            this.Controls.Add(this.txtMax);
            this.Controls.Add(this.txtID);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.minLabelAdd);
            this.Controls.Add(this.rbtnOutsourced);
            this.Controls.Add(this.rbtnInHouse);
            this.Controls.Add(this.partTitleLabel);
            this.Name = "AddPart";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Part";
            this.Load += new System.EventHandler(this.Part_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label partTitleLabel;
        private System.Windows.Forms.RadioButton rbtnInHouse;
        private System.Windows.Forms.RadioButton rbtnOutsourced;
        private System.Windows.Forms.Label idLabel;
        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.Label inventoryLabel;
        private System.Windows.Forms.Label priceLabel;
        private System.Windows.Forms.Label maxLabel;
        private System.Windows.Forms.Label minLabelAdd;
        private System.Windows.Forms.Label companyOrMachineLabel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.TextBox txtMax;
        private System.Windows.Forms.TextBox txtMin;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtInventory;
        private System.Windows.Forms.TextBox txtPrice;
        private System.Windows.Forms.TextBox txtCompanyOrMachine;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ToolTip toolTipValidation;
    }
}