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
    public partial class ModifyProduct : Form
    {
        private readonly Product productToModify;

        public ModifyProduct(Product product)
        {
            InitializeComponent();
            productToModify = product;
        }

        private void ModifyProductForm_Load(object sender, EventArgs e)
        {
            if (productToModify == null)
            {
                MessageBox.Show("Error: No product to modify.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            // Populate fields with product data
            txtProductID.Text = productToModify.ProductID.ToString();
            txtProductID.ReadOnly = true; // Ensure Product ID cannot be modified
            txtName.Text = productToModify.Name;
            txtInventory.Text = productToModify.InStock.ToString();
            txtPrice.Text = productToModify.Price.ToString("F2");
            txtMin.Text = productToModify.Min.ToString();
            txtMax.Text = productToModify.Max.ToString();

            // Load associated parts grid
            LoadAssociatedPartsGrid();

            // Load candidate parts grid
            LoadCandidatePartsGrid();
        }

        private void LoadCandidatePartsGrid()
        {
            dgvCandidateParts.DataSource = Inventory.AllParts;
            dgvCandidateParts.AutoGenerateColumns = true;
            dgvCandidateParts.RowHeadersVisible = false;
        }

        private void LoadAssociatedPartsGrid()
        {
            dgvAssociatedParts.DataSource = new BindingList<Part>(productToModify.AssociatedParts);
            dgvAssociatedParts.AutoGenerateColumns = true;
            dgvAssociatedParts.RowHeadersVisible = false;
        }

        private void BtnAddPart_Click(object sender, EventArgs e)
        {
            if (dgvCandidateParts.CurrentRow != null)
            {
                Part selectedPart = (Part)dgvCandidateParts.CurrentRow.DataBoundItem;
                if (!productToModify.AssociatedParts.Contains(selectedPart))
                {
                    productToModify.AddAssociatedPart(selectedPart);
                    LoadAssociatedPartsGrid();
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
                bool success = productToModify.RemoveAssociatedPart(selectedPart.PartID);
                if (success)
                {
                    LoadAssociatedPartsGrid();
                }
                else
                {
                    MessageBox.Show("Failed to remove the associated part.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Please select a part to remove.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            ResetValidation();

            if (!ValidateAllFields())
            {
                MessageBox.Show("Please correct the highlighted fields before saving.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!productToModify.AssociatedParts.Any())
            {
                MessageBox.Show("A product must have at least one associated part.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Parse input values
                string name = txtName.Text;
                int inventory = int.Parse(txtInventory.Text);
                decimal price = decimal.Parse(txtPrice.Text);
                int min = int.Parse(txtMin.Text);
                int max = int.Parse(txtMax.Text);

                // Logical validation
                if (min > max)
                {
                    MessageBox.Show("Min value cannot be greater than Max.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (inventory < min || inventory > max)
                {
                    MessageBox.Show("Inventory must be between Min and Max.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Update product with new values
                productToModify.Name = name;
                productToModify.InStock = inventory;
                productToModify.Price = price;
                productToModify.Min = min;
                productToModify.Max = max;

                MessageBox.Show("Product modified successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                textBox.BackColor = Color.LightCoral;
            }
            else
            {
                switch (dataType.ToLower())
                {
                    case "int":
                        if (!int.TryParse(textBox.Text, out _))
                        {
                            isValid = false;
                            textBox.BackColor = Color.LightCoral;
                        }
                        break;
                    case "decimal":
                        if (!decimal.TryParse(textBox.Text, out _))
                        {
                            isValid = false;
                            textBox.BackColor = Color.LightCoral;
                        }
                        break;
                }
            }

            return isValid;
        }
    }
}
