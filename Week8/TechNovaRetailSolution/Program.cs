using System;
using System.Collections.Generic;
using System.Linq;
namespace TechNovaInventorySystem
{

 public abstract class Product
 {
 public string Id { get; }
 public string Name { get; }
 public decimal Price { get; protected set; }
 public int Quantity { get; protected set; }
 public DateTime DateAdded { get; }
 protected Product(string id, string name, decimal price, int quantity)
 {
 if (string.IsNullOrWhiteSpace(id))
 throw new ArgumentException("Invalid Id");
 if (string.IsNullOrWhiteSpace(name))
 throw new ArgumentException("Invalid Name");
 if (price <= 0)
 throw new ArgumentException("Price must be positive");
 if (quantity < 0)
 throw new ArgumentException("Quantity cannot be negative");
 Id = id;
 Name = name;
 Price = price;
 Quantity = quantity;
 DateAdded = DateTime.Now;
 }
 public virtual decimal CalculateInventoryValue()
 {
 return Price * Quantity;
 }
 public override string ToString()
 {
 return $"{Id} | {Name} | {GetType().Name} | Qty: {Quantity} | Price: {Price:C}";
 }
 }

 public abstract class ElectronicProduct : Product
 {
 public string Brand { get; }
 public string Model { get; }
 public int WarrantyPeriodMonths { get; }
 public int PowerUsageWatts { get; }
 public DateTime ManufacturingDate { get; }
 protected ElectronicProduct(
 string id,
 string name,
 decimal price,
 int quantity,
 string brand,
 string model,
 int warrantyMonths,
 int powerUsageWatts,
 DateTime manufacturingDate)
 : base(id, name, price, quantity)
 {
 Brand = brand;
 Model = model;
 WarrantyPeriodMonths = warrantyMonths;
 PowerUsageWatts = powerUsageWatts;
 ManufacturingDate = manufacturingDate;
 }
 public DateTime WarrantyExpiry()
 {
 return ManufacturingDate.AddMonths(WarrantyPeriodMonths);
 }
 }
 public class Laptop : ElectronicProduct
 {
 public int RAM_GB { get; }
 public int Storage_GB { get; }
 public Laptop(
 string id,
 string name,
 decimal price,
 int quantity,
 string brand,
 string model,
 int warrantyMonths,
 int powerUsageWatts,
 DateTime manufacturingDate,
 int ram,
 int storage)
 : base(id, name, price, quantity, brand, model, warrantyMonths, powerUsageWatts,
manufacturingDate)
 {
 RAM_GB = ram;
 Storage_GB = storage;
 }
 public override string ToString()
 {
 return base.ToString() + $" | Brand: {Brand} | RAM: {RAM_GB}GB | Storage: {Storage_GB}GB";
 }
 }
 public class GroceryProduct : Product
 {
 public DateTime ExpiryDate { get; }
 public double WeightKg { get; }
 public bool IsOrganic { get; }
 public string StorageTemperature { get; }
 public GroceryProduct(
 string id,
 string name,
 decimal price,
 int quantity,
 DateTime expiryDate,
 double weightKg,
 bool isOrganic,
 string storageTemperature)
 : base(id, name, price, quantity)
 {
 ExpiryDate = expiryDate;
 WeightKg = weightKg;
 IsOrganic = isOrganic;
 StorageTemperature = storageTemperature;
 }
 public bool IsExpired()
 {
 return DateTime.Now > ExpiryDate;
 }
 public override string ToString()
 {
 return base.ToString() + $" | Expiry: {ExpiryDate:d} | Organic: {IsOrganic}";
 }
 }
 public class ClothingProduct : Product
 {
 public string Size { get; }
 public string FabricType { get; }
 public string Gender { get; }
 public string Color { get; }
 public ClothingProduct(
 string id,
 string name,
 decimal price,
 int quantity,
 string size,
 string fabricType,
 string gender,
 string color)
 : base(id, name, price, quantity)
 {
 Size = size;
 FabricType = fabricType;
 Gender = gender;
 Color = color;
 }
 public override string ToString()
 {
 return base.ToString() + $" | Size: {Size} | Fabric: {FabricType} | Gender: {Gender}";
 }
 }
 public class Inventory<T> where T : Product
 {
 private readonly List<T> _items = new List<T>();
 public void Add(T product)
 {
 if (_items.Any(p => p.Id == product.Id))
 throw new Exception("Duplicate Product ID");
 _items.Add(product);
 }
 public void Remove(string id)
 {
 var product = _items.FirstOrDefault(p => p.Id == id);
 if (product != null)
 _items.Remove(product);
 }
 public T Find(string id)
 {
 return _items.FirstOrDefault(p => p.Id == id);
 }
 public decimal TotalValue()
 {
 return _items.Sum(p => p.CalculateInventoryValue());
 }
 public List<T> GetAll()
 {
 return _items;
 }
 }
 class Program
 {
 static void Main()
 {
 var laptopInventory = new Inventory<Laptop>();
 var groceryInventory = new Inventory<GroceryProduct>();
 var clothingInventory = new Inventory<ClothingProduct>();
 var laptop = new Laptop(
 "E001",
 "Gaming Laptop",
 1200m,
 5,
 "Dell",
 "G15",
 24,
 180,
 DateTime.Now.AddMonths(-3),
 16,
 512);
 var milk = new GroceryProduct(
 "G001",
 "Milk",
 3.5m,
 50,
 DateTime.Now.AddDays(7),
 1.0,
 true,
 "Refrigerated");
 var shirt = new ClothingProduct(
 "C001",
 "Casual Shirt",
 25m,
 30,
 "L",
 "Cotton",
 "Men",
 "Blue");
 laptopInventory.Add(laptop);
 groceryInventory.Add(milk);
 clothingInventory.Add(shirt);
 Console.WriteLine("=== Electronics ===");
 foreach (var item in laptopInventory.GetAll())
 Console.WriteLine(item);
 Console.WriteLine("\n=== Grocery ===");
 foreach (var item in groceryInventory.GetAll())
 Console.WriteLine(item);
 Console.WriteLine("\n=== Clothing ===");
 foreach (var item in clothingInventory.GetAll())
 Console.WriteLine(item);
 Console.WriteLine($"\nTotal Laptop Inventory Value: {laptopInventory.TotalValue():C}");
 }
 }
}
