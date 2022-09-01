using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ProductsDB>(opt => opt.UseInMemoryDatabase("ProductsList"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
var app = builder.Build();

app.MapGet("/", () => "Hello World");

app.MapGet("/productItems", async (ProductsDB db) => await db.Products.ToListAsync());

app.MapGet("/productItems/{productID}", async (int productID, ProductsDB db) => await db.Products.FindAsync(productID) is Products products ? Results.Ok(products) : Results.NotFound());

app.MapPut("/productItems/Finish/{productId}", async (int productId, ProductsDB db) =>
{
    var product = await db.Products.FindAsync(productId);

    if (product is null) return Results.NotFound();
    product.IsAvailable = false;
    product.ProductCount = 0;
    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.MapPut("/products/productUpdate/{productID}", async (int productID, Products intputProductData, ProductsDB db) =>
{
    var data_from_products = await db.Products.FindAsync(productID);
    if (data_from_products is null) return Results.NotFound();
    data_from_products.ProductName = intputProductData.ProductName;
    data_from_products.ProductDescription = intputProductData.ProductDescription;
    data_from_products.ProductPrice = intputProductData.ProductPrice;
    data_from_products.ProductCompany = intputProductData.ProductCompany;
    if (intputProductData.ProductCount > 0) data_from_products.ProductCount = intputProductData.ProductCount;
    else { data_from_products.IsAvailable = false; data_from_products.IsAvailable = false; }
    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.MapPut("/products/updateCount/{productID}", async (int productID, ProductsDB db) =>
{
    var product = await db.Products.FindAsync(productID);
    if (product is not null)
    {
        if (product.ProductCount == 1)
        {
            product.IsAvailable = false;
            product.ProductCount = 0;
            await db.SaveChangesAsync();
        }
        else
        {
            product.ProductCount = product.ProductCount - 1;
            await db.SaveChangesAsync();
        }
        return Results.Ok(product);
    }
    return Results.NotFound();
});

app.MapPost("/products/addProduct", async (Products product, ProductsDB db) =>
{
    db.Products.Add(product);
    await db.SaveChangesAsync();
    return Results.Created($"/productItems/{product.ProductID}", product);
});

app.MapDelete("/products/deleteProduct/{productId}", async (int productID, ProductsDB db) =>
{
    if (await db.Products.FindAsync(productID) is Products products)
    {
        db.Products.Remove(products);
        await db.SaveChangesAsync();
        return Results.Ok(products);
    }
    return Results.NotFound();
});

app.Run();


class Products
{
    [Key]
    public int ProductID { get; set; }
    public string ProductName { get; set; } = null!;
    public string ProductDescription { get; set; } = null!;
    public string ProductCompany { get; set; } = null!;
    public decimal ProductPrice { get; set; }
    public int ProductCount { get; set; }
    public bool IsAvailable { get; set; }
}

class ProductsDB : DbContext
{
    public ProductsDB(DbContextOptions<ProductsDB> options) : base(options) { }

    public DbSet<Products> Products => Set<Products>();
}