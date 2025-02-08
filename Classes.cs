using System;
using System.ComponentModel;
using System.Linq;

namespace Inventory_Project
{
    public abstract class Part
    {
        public int PartID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int InStock { get; set; }
        public int Min { get; set; }
        public int Max { get; set; }
    }

    public class Inhouse : Part
    {
        public int MachineID { get; set; }
    }

    public class Outsourced : Part
    {
        public string CompanyName { get; set; }
    }

    public class Product
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int InStock { get; set; }
        public int Min { get; set; }
        public int Max { get; set; }
        public BindingList<Part> AssociatedParts { get; set; } = new BindingList<Part>();

        public void AddAssociatedPart(Part part)
        {
            AssociatedParts.Add(part);
        }

        public bool RemoveAssociatedPart(int partID)
        {
            Part partToRemove = LookupAssociatedPart(partID);
            if (partToRemove != null)
            {
                return AssociatedParts.Remove(partToRemove);
            }
            return false;
        }

        public Part LookupAssociatedPart(int partID)
        {
            return AssociatedParts.FirstOrDefault(p => p.PartID == partID);
        }
    }

    public static class Inventory
    {
        public static BindingList<Part> AllParts { get; set; } = new BindingList<Part>();
        public static BindingList<Product> AllProducts { get; set; } = new BindingList<Product>();

        public static void AddPart(Part part)
        {
            AllParts.Add(part);
        }

        public static bool DeletePart(Part part)
        {
            // Ensure the part is not associated with any product
            if (AllProducts.Any(product => product.AssociatedParts.Contains(part)))
            {
                throw new InvalidOperationException("Cannot delete a part that is associated with a product.");
            }
            return AllParts.Remove(part);
        }

        public static Part LookupPart(int partID)
        {
            return AllParts.FirstOrDefault(p => p.PartID == partID);
        }

        public static void UpdatePart(int partID, Part updatedPart)
        {
            Part existingPart = LookupPart(partID);
            if (existingPart != null)
            {
                int index = AllParts.IndexOf(existingPart);
                AllParts[index] = updatedPart;
            }
        }

        public static void AddProduct(Product product)
        {
            AllProducts.Add(product);
        }

        public static bool RemoveProduct(int productID)
        {
            Product productToRemove = LookupProduct(productID);
            if (productToRemove != null)
            {
                return AllProducts.Remove(productToRemove);
            }
            return false;
        }

        public static Product LookupProduct(int productID)
        {
            return AllProducts.FirstOrDefault(p => p.ProductID == productID);
        }

        public static void UpdateProduct(int productID, Product updatedProduct)
        {
            Product existingProduct = LookupProduct(productID);
            if (existingProduct != null)
            {
                int index = AllProducts.IndexOf(existingProduct);
                AllProducts[index] = updatedProduct;
            }
        }

        // Helper for unique ID generation
        private static int nextPartID = 1;
        private static int nextProductID = 1;

        public static int GenerateUniquePartID()
        {
            return nextPartID++;
        }

        public static int GenerateUniqueProductID()
        {
            return nextProductID++;
        }
    }
}