using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using testwithnet8;
using testwithnet8.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IMessageSender, EmailMessageSender>();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddHostedService<DatabaseMigrator>();

var app = builder.Build();


// Apply database migrations on startup


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/categories", async (Category category, [FromServices] ApplicationDbContext db) =>
{
    db.Categories.Add(category); // OR db.Add(category)
    await db.SaveChangesAsync();
});

app.MapPost("/products", async (Product product, [FromServices] ApplicationDbContext db) =>
{
    db.Products.Add(product); // OR db.Add(category)
    await db.SaveChangesAsync();
});

app.MapPost("/people", async (CreatePersonDto request, [FromServices] ApplicationDbContext db) =>
{
    var person = new Person
    {
        FirstName = request.FirstName,
        LastName = request.LastName
    };
    db.People.Add(person);
    await db.SaveChangesAsync();

    return Results.Created($"/people/{person.Id}", person);
});


app.MapGet("/people/{id}", async (int id, [FromServices] ApplicationDbContext db) =>
{
    var person = await db.People.FindAsync(id);


    if (person == null)
    {
        return Results.NotFound();
    }

    var personDto = new PersonDto
    {
        Id = person.Id,
        FirstName = person.FirstName,
        LastName = person.LastName
    };

    return Results.Ok(personDto);
});


app.MapGet("/products/{id}", async (int id, [FromServices] ApplicationDbContext db) =>
{
    var product = await db.Products
        .Include(x => x.Category)
        .FirstOrDefaultAsync(p => p.Id == id);

    var productDto = new ProductDto();
    productDto.Id = product.Id;
    productDto.ProductName = product.ProductName;
    productDto.Price = product.Price;

    var categoryDto = new CategoryDto();
    categoryDto.Id = product.Category.Id;
    categoryDto.CategoryName = product.Category.CategoryName;

    productDto.CategoryDto = categoryDto;

    return productDto;
});


app.MapPut("/products", async (UpdateProductDto productDto, [FromServices] ApplicationDbContext db) =>
{
    Product existingProduct = await db.Products.FindAsync(productDto.Id);
    existingProduct.Id = productDto.Id;
    existingProduct.Price = productDto.Price;
    existingProduct.ProductName = productDto.ProductName;
    existingProduct.Cost = productDto.Cost;
    db.Products.Update(existingProduct);
    await db.SaveChangesAsync();
    return Results.Ok();
});


app.Run();