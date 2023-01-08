using MongoExample.Models;
using MongoExample.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<MongoDBSettings>(builder.Configuration.GetSection("MongoDB"));
builder.Services.AddSingleton<MongoDBService>();
// builder.Services.AddCors(options =>
// {
//     options.AddPolicy(name: "ApiCorsPolicy",
//                       builder =>
//                       {
//                           builder.WithOrigins("http://localhost:3000", "https://localhost:3000")
//                             .AllowAnyHeader()
//                             .AllowAnyMethod()
//                             .AllowCredentials();
//                             //.WithMethods("OPTIONS", "GET");
//                       });
// });
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();




var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseHsts();
}

app.UseCors(options => {
    options.AllowAnyOrigin(); options.AllowAnyHeader();
    });

app.UseHttpsRedirection();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


