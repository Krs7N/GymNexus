var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplicationDbContext(builder.Configuration);
builder.Services.AddApplicationIdentity(builder.Configuration);
builder.Services.AddApplicationServices();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(config =>
    {
        config.WithOrigins("http://localhost:4200", "https://localhost:7089")
            .AllowAnyHeader()
            .WithMethods(HttpMethod.Get.Method, HttpMethod.Post.Method, HttpMethod.Put.Method,
                HttpMethod.Delete.Method);
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.UseStaticFiles();

app.MapControllers();

await app.RunAsync();