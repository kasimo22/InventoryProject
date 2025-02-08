using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Inventory_Project
{
    public partial class AddProduct : Form
    {
        private readonly BindingList<Part> associatedParts = new BindingList<Part>();

        public AddProduct()
        {
            InitializeComponent();
            LoadCandidatePartsGrid();
            LoadAssociatedPartsGrid();
        }

        private void AddProductForm_Load(object sender, EventArgs e)
        {
            txtProductID.Text = Inventory.GenerateUniqueProductID().ToString();
            txtProductID.ReadOnly = true; // Ensure Product ID is not editable
        }

        private void LoadCandidatePartsGrid()
        {
            dgvCandidateParts.DataSource = Inventory.AllParts;
            dgvCandidateParts.AutoGenerateColumns = true;
            dgvCandidateParts.RowHeadersVisible = false;
        }

        private void LoadAssociatedPartsGrid()
        {
            dgvAssociatedParts.DataSource = associatedParts;
            dgvAssociatedParts.AutoGenerateColumns = true;
            dgvAssociatedParts.RowHeadersVisible = false;
        }

        private void BtnAddPart_Click(object sender, EventArgs e)
        {
            if (dgvCandidateParts.CurrentRow != null)
            {
                Part selectedPart = (Part)dgvCandidateParts.CurrentRow.DataBoundItem;
                if (!associatedParts.Contains(selectedPart))
                {
                    associatedParts.Add(selectedPart);
                }
                else
                {
                    MessageBox.Show("Part is already associated with the product.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Please select a part to add.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnRemovePart_Click(object sender, EventArgs e)
        {
            if (dgvAssociatedParts.CurrentRow != null)
            {
                Part selectedPart = (Part)dgvAssociatedParts.CurrentRow.DataBoundItem;
                associatedParts.Remove(selectedPart);
            }
            else
            {
                MessageBox.Show("Please select a part to remove.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            // Reset validation highlights
            ResetValidation();

            if (!ValidateAllFields())
            {
                MessageBox.Show("Please correct the highlighted fields before saving.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (associatedParts.Count == 0)
            {
                MessageBox.Show("A product must have at least one associated part.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Validate and parse fields
                if (!int.TryParse(txtInventory.Text.Trim(), out int inventory))
                    throw new FormatException("Inventory must be a valid integer.");

                if (!decimal.TryParse(txtPrice.Text.Trim(), NumberStyles.Number, CultureInfo.InvariantCulture, out decimal price))
                    throw new FormatException("Price must be a valid decimal number.");

                if (!int.TryParse(txtMin.Text.Trim(), out int min))
                    throw new FormatException("Min must be a valid integer.");

                if (!int.TryParse(txtMax.Text.Trim(), out int max))
                    throw new FormatException("Max must be a valid integer.");

                // Logical validations
                if (min > max)
                    throw new ArgumentException("Min value cannot be greater than Max.");

                if (inventory < min || inventory > max)
                    throw new ArgumentException("Inventory must be between Min and Max.");

                // Create a new product
                Product newProduct = new Product
                {
                    ProductID = Inventory.GenerateUniqueProductID(),
                    Name = txtName.Text.Trim(),
                    InStock = inventory,
                    Price = price,
                    Min = min,
                    Max = max,
                    AssociatedParts = new BindingList<Part>(associatedParts.ToList()) // Copy associated parts
                };

                // Add to inventory
                Inventory.AllProducts.Add(newProduct);

                MessageBox.Show("Product added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Unexpected Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            string query = txtSearch.Text.ToLower();
            var filteredParts = Inventory.AllParts
                .Where(p => p.Name.ToLower().Contains(query) || p.PartID.ToString() == query)
                .ToList();
            dgvCandidateParts.DataSource = new BindingList<Part>(filteredParts);
        }

        private void ResetValidation()
        {
            foreach (Control control in this.Controls)
            {
                if (control is TextBox textBox)
                {
                    textBox.BackColor = SystemColors.Window;
                }
            }
        }

        private bool ValidateAllFields()
        {
            bool allValid = true;

            allValid &= ValidateField(txtName, "string");
            allValid &= ValidateField(txtInventory, "int");
            allValid &= ValidateField(txtPrice, "decimal");
            allValid &= ValidateField(txtMin, "int");
            allValid &= ValidateField(txtMax, "int");

            return allValid;
        }

        private bool ValidateField(TextBox textBox, string dataType)
        {
            bool isValid = true;

            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                isValid = false;
                toolTipValidation.SetToolTip(textBox, "This field is required.");
                textBox.BackColor = Color.LightCoral;
                return isValid;
            }

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

            textBox.BackColor = isValid ? SystemColors.Window : Color.LightCoral;
            return isValid;
        }
    }
}
