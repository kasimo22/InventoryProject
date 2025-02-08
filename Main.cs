using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Inventory_Project
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            LoadPartsGrid();
            LoadProductsGrid();
        }

        private void LoadPartsGrid()
        {
            // Bind the BindingList directly to the DataGridView
            dgvParts.DataSource = Inventory.AllParts;

            // Remove the row header column
            dgvParts.RowHeadersVisible = false;

            // Customize column headers
            dgvParts.Columns["PartID"].HeaderText = "Part ID";
            dgvParts.Columns["Name"].HeaderText = "Part Name";
            dgvParts.Columns["Price"].HeaderText = "Price";
            dgvParts.Columns["InStock"].HeaderText = "Inventory";
            dgvParts.Columns["Min"].HeaderText = "Min";
            dgvParts.Columns["Max"].HeaderText = "Max";
        }

        private void LoadProductsGrid()
        {
            // Bind the BindingList directly to the DataGridView
            dgvProducts.DataSource = Inventory.AllProducts;

            // Remove the row header column
            dgvProducts.RowHeadersVisible = false;

            // Customize column headers
            dgvProducts.Columns["ProductID"].HeaderText = "Product ID";
            dgvProducts.Columns["Name"].HeaderText = "Name";
            dgvProducts.Columns["Price"].HeaderText = "Price";
            dgvProducts.Columns["InStock"].HeaderText = "Inventory";
            dgvProducts.Columns["Min"].HeaderText = "Min";
            dgvProducts.Columns["Max"].HeaderText = "Max";
        }

        private void BtnExit_Click(object sender, EventArgs e) => Application.Exit();

        private void BtnAddPart_Click(object sender, EventArgs e)
        {
            AddPart addPart = new AddPart();
            addPart.ShowDialog();
        }

        private void BtnModifyPart_Click(object sender, EventArgs e)
        {
            if (dgvParts.CurrentRow != null)
            {
                // Get the selected part from the DataGridView
                Part selectedPart = (Part)dgvParts.CurrentRow.DataBoundItem;

                // Open the ModifyPartForm and pass the selected part
                ModifyPart modifyPart = new ModifyPart(selectedPart);
                modifyPart.ShowDialog();

                // Refresh the DataGridView to reflect changes
                dgvParts.Refresh();
            }
            else
            {
                MessageBox.Show("Please select a part to modify.");
            }
        }

        private void BtnAddProduct_Click(object sender, EventArgs e)
        {
            AddProduct addProduct = new AddProduct();
            addProduct.ShowDialog();
        }

        private void BtnSearchParts_Click(object sender, EventArgs e)
        {
            string query = txtSearchPart.Text.ToLower();
            var filteredParts = Inventory.AllParts
                .Where(p => p.Name.ToLower().Contains(query) || p.PartID.ToString() == query)
                .ToList();
            dgvParts.DataSource = new BindingList<Part>(filteredParts);
        }

        private void BtnSearchProducts_Click(object sender, EventArgs e)
        {
            string query = txtSearchProduct.Text.ToLower();
            var filteredProducts = Inventory.AllProducts
                .Where(p => p.Name.ToLower().Contains(query) || p.ProductID.ToString() == query)
                .ToList();
            dgvProducts.DataSource = new BindingList<Product>(filteredProducts);
        }

        private void BtnModifyProduct_Click(object sender, EventArgs e)
        {
            if (dgvProducts.CurrentRow != null)
            {
                // Get the selected product
                Product selectedProduct = (Product)dgvProducts.CurrentRow.DataBoundItem;

                // Open the ModifyProduct form
                ModifyProduct modifyProduct = new ModifyProduct(selectedProduct);
                modifyProduct.ShowDialog();

                // Refresh the products grid after closing the form
                LoadProductsGrid();
            }
            else
            {
                MessageBox.Show("Please select a product to modify.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnDeletePart_Click(object sender, EventArgs e)
        {
            if (dgvParts.CurrentRow != null)
            {
                // Get the selected part
                Part selectedPart = (Part)dgvParts.CurrentRow.DataBoundItem;

                // Check if the part is associated with any product
                bool isPartAssociated = Inventory.AllProducts.Any(product =>
                    product.AssociatedParts.Contains(selectedPart));

                if (isPartAssociated)
                {
                    MessageBox.Show($"The part '{selectedPart.Name}' cannot be deleted because it is associated with a product.",
                                    "Deletion Error",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                    return;
                }

                // Confirm deletion
                DialogResult result = MessageBox.Show($"Are you sure you want to delete Part '{selectedPart.Name}'?",
                                                      "Confirm Deletion",
                                                      MessageBoxButtons.YesNo,
                                                      MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    // Attempt to delete the part
                    bool isDeleted = Inventory.DeletePart(selectedPart);

                    if (isDeleted)
                    {
                        MessageBox.Show("Part deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadPartsGrid(); // Refresh the grid
                    }
                    else
                    {
                        MessageBox.Show("Failed to delete the part.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a part to delete.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnDeleteProduct_Click(object sender, EventArgs e)
        {
            if (dgvProducts.CurrentRow != null)
            {
                // Get the selected product
                Product selectedProduct = (Product)dgvProducts.CurrentRow.DataBoundItem;

                // Confirm deletion
                DialogResult result = MessageBox.Show($"Are you sure you want to delete Product '{selectedProduct.Name}' and all its associations?",
                                                      "Confirm Deletion",
                                                      MessageBoxButtons.YesNo,
                                                      MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    // Disassociate all parts
                    selectedProduct.AssociatedParts.Clear();

                    // Remove the product from the inventory
                    bool isDeleted = Inventory.AllProducts.Remove(selectedProduct);

                    if (isDeleted)
                    {
                        MessageBox.Show("Product and its associations deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadProductsGrid(); // Refresh the grid
                    }
                    else
                    {
                        MessageBox.Show("Failed to delete the product.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a product to delete.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

    }
}
