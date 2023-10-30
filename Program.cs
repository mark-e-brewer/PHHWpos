using PHHWpos.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;
using PHHWpos;
using Microsoft.AspNetCore.Builder;
using System.Runtime.CompilerServices;
using System.Net;
using PHHWpos.DTOs;

var builder = WebApplication.CreateBuilder(args);

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
//ADD CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy(MyAllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins("https://localhost:7165",
                                "http://localhost:3000")
                                .AllowAnyHeader()
                                .AllowAnyMethod();
        });
});

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// allows passing datetimes without time zone data 
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// allows our api endpoints to access the database through Entity Framework Core
builder.Services.AddNpgsql<PHHWposDbContext>(builder.Configuration["PHHWposDbConnectionString"]);

// Set the JSON serializer options
builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

var app = builder.Build();

app.UseCors(MyAllowSpecificOrigins);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
//GET User Id from UID
app.MapGet("/uservalidate/{uid}", (PHHWposDbContext db, string uid) =>
{
    var userExists = db.Users.Where(x => x.UID == uid).FirstOrDefault();
    if (userExists == null)
    {
        return Results.StatusCode(204);
    }
    return Results.Ok(userExists);
});
//GET all Users
app.MapGet("/users", (PHHWposDbContext db) =>
{
    return db.Users.ToList();
});
//GET user by ID
app.MapGet("/user/{id}", (PHHWposDbContext db, int id) =>
{
    var user = db.Users.Where(u => u.Id == id);
    return user;
});
//POST a new User
app.MapPost("/newUser", (PHHWposDbContext db, User user) =>
{
    db.Users.Add(user);
    db.SaveChanges();
    return Results.Created($"/newUser/{user.Id}", user);

});
//GET User ID with User UID
app.MapGet("/checkuserID/{uid}", (PHHWposDbContext db, string uid) =>
{
    var user = db.Users.Where(x => x.UID == uid).ToList();
    if (uid == null)
    {
        return Results.NotFound();
    }
    else
    {
        return Results.Ok(user);
    }
});
//GET all Orders
app.MapGet("/orders", (PHHWposDbContext db) =>
{
    return db.Orders.ToList();
});
//GET Order by ID
app.MapGet("/order/{id}", (PHHWposDbContext db, int id) =>
{
    var order = db.Orders.Where(o => o.Id == id);
    return order;
});
//DELETE Order
app.MapDelete("/order/{id}", (PHHWposDbContext db, int id) =>
{
    var orderToDelete = db.Orders.Where(o => o.Id == id).FirstOrDefault();

    if (orderToDelete == null)
    {
        return Results.NotFound("Order not found");
    }

    db.Orders.Remove(orderToDelete);
    db.SaveChanges();
    return Results.Ok();
});
//DELTE a Item from an Order
app.MapDelete("/order/{orderId}/item/{itemId}", (PHHWposDbContext db, int orderId, int itemId) =>
{
    var order = db.Orders
        .Include(o => o.Items)
        .FirstOrDefault(o => o.Id == orderId);

    if (order == null)
    {
        return Results.NotFound("Order not found");
    }

    var item = order.Items.FirstOrDefault(i => i.Id == itemId);

    if (item == null)
    {
        return Results.NotFound("Item not found in the order");
    }

    order.Items.Remove(item);
    db.SaveChanges();
    return Results.Ok(order);
});
//POST a new Order
app.MapPost("/order", async (PHHWposDbContext db, Order order) =>
{
    db.Orders.Add(order);
    await db.SaveChangesAsync();
    return Results.Created($"/order/{order.Id}", order);
});
//V1 POST an existing Item to an Open Order
app.MapPost("/orderitem/{orderId}/{itemId}", (PHHWposDbContext db, int orderId, int itemId) =>
{
    var order = db.Orders
        .Include(o => o.Items)
        .FirstOrDefault(o => o.Id == orderId);

    var item = db.Items?.Find(itemId);

    if (order == null || item == null)
    {
        return Results.NotFound("Order or item not found");
    }

    order?.Items?.Add(item);

    db.SaveChanges();

    return Results.Ok();
});
//V2 POST Item to Order
app.MapPost("/order/{orderId}/item/{itemId}", (PHHWposDbContext db, int orderId, int itemId) =>
{
    var order = db.Orders.Include(o => o.Items)
                         .FirstOrDefault(o => o.Id == orderId);
    if (order == null)
    {
        return Results.NotFound("Order not found");
    }

    var itemToAdd = db.Items?.Find(itemId);


    if (itemToAdd == null)
    {
        return Results.NotFound("Item not found");
    }

    order?.Items?.Add(itemToAdd);
    db.SaveChanges();
    return Results.Ok(order);
});
//GET Order Items by Order Id
app.MapGet("/order/{id}/items", (PHHWposDbContext db, int id) =>
{
    var order = db.Orders
        .Include(o => o.Items)
        .FirstOrDefault(o => o.Id == id);

    if (order == null)
    {
        return Results.NotFound("Order not found");
    }

    return Results.Ok(order.Items);
});
//PUT update an order
app.MapPut("/order/{id}", (PHHWposDbContext db, int id, Order updatedOrder) =>
{
    var existingOrder = db.Orders
        .Include(o => o.Items)
        .FirstOrDefault(o => o.Id == id);

    if (existingOrder == null)
    {
        return Results.NotFound("Order not found");
    }

    existingOrder.Name = updatedOrder.Name;
    existingOrder.Status = updatedOrder.Status;
    existingOrder.Type = updatedOrder.Type;
    existingOrder.CustomerEmail = updatedOrder.CustomerEmail;
    existingOrder.CustomerPhone = updatedOrder.CustomerPhone;
    existingOrder.CustomerName = updatedOrder.CustomerName;

    db.SaveChanges();

    return Results.Ok(existingOrder);
});
//GET all Items
app.MapGet("/items", (PHHWposDbContext db) =>
{
    return db.Items.ToList();
});
//POST and Item
app.MapPost("/item", async (PHHWposDbContext db, Item item) =>
{
    db.Items.Add(item);
    await db.SaveChangesAsync();
    return Results.Created($"/item/{item.Id}", item);
});
//PUT an Item
app.MapPut("/item/{id}", (PHHWposDbContext db, int id, Item updatedItem) =>
{
    var existingItem = db.Items.Find(id);

    if (existingItem == null)
    {
        return Results.NotFound("Item not found");
    }

    existingItem.Name = updatedItem.Name;
    existingItem.Price = updatedItem.Price;

    db.SaveChanges();

    return Results.Ok(existingItem);
});
//GET Item by ID
app.MapGet("/item/{id}", (PHHWposDbContext db, int id) =>
{
    var item = db.Items.Where(i => i.Id == id);
    return item;
});
//GET All OPEN Orders
app.MapGet("/ordersopen", (PHHWposDbContext db) =>
{
    var openOrders = db.Orders
    .Where(order => order.Status == false)
    .ToList();

    return Results.Ok(openOrders);
});
//GET All CLOSED Orders
app.MapGet("/ordersclosed", (PHHWposDbContext db) =>
{
    var closedOrders = db.Orders
    .Where(order => order.Status == true)
    .ToList();

    return Results.Ok(closedOrders);
});
//CLOSE Order with DateNow & Add Tip/Payment Type
app.MapPost("/closeorder/{orderId}", (PHHWposDbContext db, int orderId, Order model) =>
{
    var order = db.Orders.FirstOrDefault(o => o.Id == orderId);

    if (order == null)
    {
        return Results.NotFound("Order not found");
    }

    order.Status = true;
    order.Tip = model.Tip;
    order.PaymentType = model.PaymentType;

    order.DateClosed = DateTime.Now;

    db.SaveChanges();

    return Results.NoContent();
});
//CLOSED Order DTO for Revenue Sum
app.MapGet("/closedordersummary", (PHHWposDbContext db) =>
{
    var closedOrderSummaries = db.Orders
        .Where(order => order.Status == true)
        .Select(order => new ClosedOrderSummaryDTO
        {
            OrderId = order.Id,
            Tip = order.Tip,
            Items = order.Items.Select(item => new ItemSummaryDTO
            {
                Name = item.Name,
                Price = item.Price
            }).ToList(),
            OrderType = order.Type,
            PaymentType = order.PaymentType,
            DateClosed = order.DateClosed
        })
        .ToList();

    return Results.Ok(closedOrderSummaries);
});


app.Run();
