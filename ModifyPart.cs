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
    public partial class ModifyPart : Form
    {
        private readonly Part _partToModify;

        public ModifyPart(Part part)
        {
            InitializeComponent();
            _partToModify = part;
        }

        private void RbtnInHouse_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnInHouse.Checked)
            {
                companyOrMachineLabel.Text = "Machine ID";
                txtCompanyOrMachine.Text = ""; // Clear the field
                txtCompanyOrMachine.BackColor = SystemColors.Window; // Reset background
            }
        }

        private void RbtnOutsourced_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnOutsourced.Checked)
            {
                companyOrMachineLabel.Text = "Company Name";
                txtCompanyOrMachine.Text = ""; // Clear the field
                txtCompanyOrMachine.BackColor = SystemColors.Window; // Reset background
            }
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
                // Validate fields
                ResetValidation();

                if (!ValidateAllFields())
                {
                    MessageBox.Show("Please correct the highlighted fields before saving.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

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

                // Handle In-House or Outsourced
                Part modifiedPart;
                if (rbtnInHouse.Checked)
                {
                    if (!int.TryParse(txtCompanyOrMachine.Text, out int machineID))
                    {
                        MessageBox.Show("Machine ID must be an integer.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtCompanyOrMachine.BackColor = Color.LightCoral;
                        return;
                    }

                    modifiedPart = new Inhouse
                    {
                        PartID = _partToModify.PartID, // Keep the same ID
                        Name = name,
                        Price = price,
                        InStock = inventory,
                        Min = min,
                        Max = max,
                        MachineID = machineID
                    };
                }
                else
                {
                    string companyName = txtCompanyOrMachine.Text;
                    if (string.IsNullOrWhiteSpace(companyName))
                    {
                        MessageBox.Show("Company Name is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtCompanyOrMachine.BackColor = Color.LightCoral;
                        return;
                    }

                    modifiedPart = new Outsourced
                    {
                        PartID = _partToModify.PartID, // Keep the same ID
                        Name = name,
                        Price = price,
                        InStock = inventory,
                        Min = min,
                        Max = max,
                        CompanyName = companyName
                    };
                }

                // Update the part in the inventory
                Inventory.UpdatePart(modifiedPart.PartID, modifiedPart);

                MessageBox.Show("Part modified successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
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
        private void ModifyPartForm_Load(object sender, EventArgs e)
        {
            // Attach validation events and validate all fields initially
            if (_partToModify is Inhouse inhousePart)
            {
                rbtnInHouse.Checked = true;
                companyOrMachineLabel.Text = "Machine ID";
                txtCompanyOrMachine.Text = inhousePart.MachineID.ToString();
            }
            else if (_partToModify is Outsourced outsourcedPart)
            {
                rbtnOutsourced.Checked = true;
                companyOrMachineLabel.Text = "Company Name";
                txtCompanyOrMachine.Text = outsourcedPart.CompanyName;
            }

            // Populate other fields
            txtID.Text = _partToModify.PartID.ToString();
            txtName.Text = _partToModify.Name;
            txtInventory.Text = _partToModify.InStock.ToString();
            txtPrice.Text = _partToModify.Price.ToString("F2");
            txtMin.Text = _partToModify.Min.ToString();
            txtMax.Text = _partToModify.Max.ToString();

            // Attach dynamic validation and event handlers for fields
            foreach (Control control in this.Controls)
            {
                if (control is TextBox textBox)
                {
                    textBox.MouseEnter += TextBox_MouseEnter;
                    textBox.MouseLeave += TextBox_MouseLeave;
                    textBox.TextChanged += TextBox_TextChanged;

                    // Initial validation
                    if (textBox == txtName)
                        ValidateField(textBox, "string");
                    else if (textBox == txtInventory || textBox == txtMin || textBox == txtMax)
                        ValidateField(textBox, "int");
                    else if (textBox == txtPrice)
                        ValidateField(textBox, "decimal");
                    else if (textBox == txtCompanyOrMachine)
                        ValidateField(textBox, rbtnInHouse.Checked ? "int" : "string");
                }
            }
        }

        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            bool isValid = true;

            // Determine validation rules based on the control
            if (textBox == txtName)
            {
                isValid = !string.IsNullOrWhiteSpace(textBox.Text); // Ensure a valid string
            }
            else if (textBox == txtInventory || textBox == txtMin || textBox == txtMax)
            {
                isValid = int.TryParse(textBox.Text, out _); // Ensure a valid integer
            }
            else if (textBox == txtPrice)
            {
                isValid = decimal.TryParse(textBox.Text, out _); // Ensure a valid decimal
            }
            else if (textBox == txtCompanyOrMachine)
            {
                if (rbtnInHouse.Checked)
                {
                    isValid = int.TryParse(textBox.Text, out _); // Machine ID requires an integer
                }
                else
                {
                    isValid = !string.IsNullOrWhiteSpace(textBox.Text); // Company Name requires a string
                }
            }

            // Update background color based on validity
            textBox.BackColor = isValid ? SystemColors.Window : Color.LightCoral;
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
