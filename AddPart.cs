using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Inventory_Project
{
    public partial class AddPart : Form
    {
        public AddPart()
        {
            InitializeComponent();
            ValidateAllFields();
        }

        private void Part_Load(object sender, EventArgs e)
        {

        }

        private void RbtnInHouse_CheckedChanged(object sender, EventArgs e)
        {
            companyOrMachineLabel.Text = "Machine ID";
            txtCompanyOrMachine.Clear();
        }

        private void RbtnOutsourced_CheckedChanged(object sender, EventArgs e)
        {
            companyOrMachineLabel.Text = "Company Name";
            txtCompanyOrMachine.Clear();
        }

        private void BtnSaveAdd_Click(object sender, EventArgs e)
        {
            // Reset validation highlights and tooltips
            ResetValidation();

            // Validate all required fields
            if (!ValidateAllFields())
            {
                // Show a message box if any fields are invalid
                MessageBox.Show("Please correct the highlighted fields before saving.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Parse validated data
                string name = txtName.Text;
                int inventory = int.Parse(txtInventory.Text);
                decimal price = decimal.Parse(txtPrice.Text);
                int min = int.Parse(txtMin.Text);
                int max = int.Parse(txtMax.Text);

                // Validate logical constraints
                if (min > max)
                {
                    MessageBox.Show("Min value cannot be greater than Max.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMin.BackColor = Color.LightCoral;
                    txtMax.BackColor = Color.LightCoral;
                    return;
                }

                if (inventory < min || inventory > max)
                {
                    MessageBox.Show("Inventory must be between Min and Max.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtInventory.BackColor = Color.LightCoral;
                    return;
                }

                // Handle saving for In-House or Outsourced part
                if (rbtnInHouse.Checked)
                {
                    int machineID = int.Parse(txtCompanyOrMachine.Text); // Validate Machine ID
                    Inhouse newPart = new Inhouse
                    {
                        PartID = Inventory.GenerateUniquePartID(),
                        Name = name,
                        Price = price,
                        InStock = inventory,
                        Min = min,
                        Max = max,
                        MachineID = machineID
                    };
                    Inventory.AddPart(newPart);
                }
                else
                {
                    string companyName = txtCompanyOrMachine.Text; // Validate Company Name
                    Outsourced newPart = new Outsourced
                    {
                        PartID = Inventory.GenerateUniquePartID(),
                        Name = name,
                        Price = price,
                        InStock = inventory,
                        Min = min,
                        Max = max,
                        CompanyName = companyName
                    };
                    Inventory.AddPart(newPart);
                }

                // Close the form after saving successfully
                this.Close();
            }
            catch (Exception ex)
            {
                // Catch any unexpected errors
                MessageBox.Show($"Error: {ex.Message}", "Unexpected Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnCancelAdd_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool ValidateField(TextBox textBox, string dataType)
        {
            bool isValid = true;

            // Check if the field is empty
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                isValid = false;
                toolTipValidation.SetToolTip(textBox, "This field is required.");
            }
            else
            {
                // Additional validation based on the data type
                switch (dataType.ToLower())
                {
                    case "int":
                        if (!int.TryParse(textBox.Text, out _))
                        {
                            isValid = false;
                            toolTipValidation.SetToolTip(textBox, "This field requires an integer.");
                        }
                        break;
                    case "decimal":
                        if (!decimal.TryParse(textBox.Text, out _))
                        {
                            isValid = false;
                            toolTipValidation.SetToolTip(textBox, "This field requires a decimal value.");
                        }
                        break;
                }
            }

            // Highlight the field if invalid
            textBox.BackColor = isValid ? SystemColors.Window : Color.LightCoral;

            return isValid;
        }

        private bool ValidateAllFields()
        {
            bool allValid = true;

            allValid &= ValidateField(txtName, "string");
            allValid &= ValidateField(txtInventory, "int");
            allValid &= ValidateField(txtPrice, "decimal");
            allValid &= ValidateField(txtMin, "int");
            allValid &= ValidateField(txtMax, "int");
            allValid &= ValidateField(txtCompanyOrMachine, rbtnInHouse.Checked ? "int" : "string");

            return allValid;
        }

        private void ResetValidation()
        {
            foreach (Control control in this.Controls)
            {
                if (control is TextBox textBox)
                {
                    textBox.BackColor = SystemColors.Window;
                    toolTipValidation.SetToolTip(textBox, string.Empty);
                }
            }
        }

        private void ToolTipValidation_Popup(object sender, PopupEventArgs e)
        {

        }

        // Attach event handlers dynamically for each TextBox
        private void AddPartForm_Load(object sender, EventArgs e)
        {
            // Attach validation events for dynamic validation
            foreach (Control control in this.Controls)
            {
                if (control is TextBox textBox)
                {
                    // Attach validation and hover events
                    textBox.MouseEnter += TextBox_MouseEnter;
                    textBox.MouseLeave += TextBox_MouseLeave;
                    textBox.TextChanged += TextBox_TextChanged;
                }
            }

            // Validate all fields to apply initial highlighting
            ValidateAllFields();
        }

        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;

            if (textBox == txtName)
                ValidateField(textBox, "string");
            else if (textBox == txtInventory || textBox == txtMin || textBox == txtMax)
                ValidateField(textBox, "int");
            else if (textBox == txtPrice)
                ValidateField(textBox, "decimal");
            else if (textBox == txtCompanyOrMachine)
                ValidateField(textBox, rbtnInHouse.Checked ? "int" : "string");
        }

        // MouseEnter Event Handler
        private void TextBox_MouseEnter(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            string message = string.Empty;

            // Set specific messages for each field
            if (textBox == txtName)
                message = "This field is required.";
            else if (textBox == txtInventory)
                message = "This field requires an integer.";
            else if (textBox == txtPrice)
                message = "This field requires a decimal value.";
            else if (textBox == txtMin)
                message = "This field requires an integer.";
            else if (textBox == txtMax)
                message = "This field requires an integer.";
            else if (textBox == txtCompanyOrMachine)
                message = rbtnInHouse.Checked ? "This field requires an integer (Machine ID)." : "This field is required (Company Name).";

            // Display the ToolTip
            toolTipValidation.SetToolTip(textBox, message);
        }

        // MouseLeave Event Handler
        private void TextBox_MouseLeave(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;

            // Clear the ToolTip when the mouse leaves
            toolTipValidation.SetToolTip(textBox, string.Empty);
        }
    }
}
