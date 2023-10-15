using PHHWpos.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;
using PHHWpos;
using Microsoft.AspNetCore.Builder;

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
//POST an existing Item to an existing Order
app.MapPost("/orderitem/{orderId}/{itemId}", (PHHWposDbContext db, int orderId, int itemId) =>
{

    var order = db.Orders.Find(orderId);
    var item = db.Items.Find(itemId);

    if (order == null || item == null)
    {
        return Results.NotFound("Order or item not found");
    }

    if (order.Items == null)
    {
        order.Items = new List<Item>();
    }


    order.Items.Add(item);

    db.SaveChanges();

    return Results.Ok();
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


    db.SaveChanges();

    return Results.Ok(existingOrder);
});

app.Run();
