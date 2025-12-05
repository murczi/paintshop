using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PaintShop.Domain;
using PaintShop.Domain.Interfaces;
using PaintShop.Domain.Services;
using PaintShop.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient("ChatApi", client =>
{
    client.BaseAddress = new Uri("https://chatapi.com/");
});

builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("Email"));
builder.Services.AddTransient<IEmailSender, SmtpEmailSender>();

var conn = builder.Configuration.GetConnectionString("Default")
           ?? Environment.GetEnvironmentVariable("DB_CONN");

builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseMySql(conn!, ServerVersion.AutoDetect(conn)));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

var jwtSection = builder.Configuration.GetSection("Jwt");
var jwtKey = jwtSection["Key"] ?? throw new InvalidOperationException("Jwt:Key is missing");
var jwtIssuer = jwtSection["Issuer"] ?? "PaintShop.Api";
var jwtAudience = jwtSection["Audience"] ?? "PaintShop.Client";

var jwtExpiresMinutes =
    int.TryParse(jwtSection["ExpiresMinutes"], out var minutes) ? minutes : 60;

var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));

builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtIssuer,
            ValidAudience = jwtAudience,
            IssuerSigningKey = signingKey,
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();


using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

app.MapPost("/addUser", (IUserService userService, [FromBody] User input) =>
{
    userService.AddNewUser(input);

    return Results.Created($"/addUser", input);
});

app.MapPost("/login", (IUserService userService, IUserRepository userRepository, [FromBody] User input) =>
{
    var valid = userService.Login(input);
    if (!valid)
        return Results.Unauthorized();
    
    var user = userRepository.GetUserByEmail(input.Email!);
    if (user is null)
        return Results.Unauthorized();

    var token = GenerateJwtToken(user);

    return Results.Ok(new { token });
});

app.MapGet("/faq", (IProductRepository repo) => repo.GetFaq());


app.MapPost("/addProduct", (IProductRepository repo, [FromBody] Product input) =>
{
    repo.AddNewProduct(input);

    return Results.Created($"/addProduct", input);
});

app.MapGet("/products", (IProductRepository repo) =>
{
    var items = repo.GetAllProducts();

    return Results.Ok(items);
});

app.MapPost("/product", (IProductRepository repo, [FromBody] int productid) =>
{
    var items = repo.GetProduct(productid);

    return Results.Ok(items);
});

app.MapPost("/getReviewsForProduct", (IProductRepository repo, [FromBody] int productid) =>
{
    var reviews = repo.GetReviewsByProductId(productid);

    return Results.Ok(reviews);
});

app.MapPost("/addReviewsForProduct", (IProductRepository repo, [FromBody] Review review) =>
{
    repo.AddReviewsForProduct(review);

    return Results.Created("/addReviewsForProduct", review);
});

app.MapPost("/aichat",async (IHttpClientFactory httpClientFactory, [FromBody] ChatRequest request) =>
{
    var client = httpClientFactory.CreateClient("ChatApi");

    var response = await client.PostAsJsonAsync("chat", request);

    if (!response.IsSuccessStatusCode)
    {
        return Results.StatusCode((int)response.StatusCode);
    }

    var reply = await response.Content.ReadFromJsonAsync<ChatResponse>();
    return Results.Ok(reply);
});

app.MapPost("/newsletter", async (IEmailSender emailSender, [FromBody] string to) =>
{
    await emailSender.SendEmailAsync(to);
    return Results.Ok(new { message = "Email sent" });
});

app.Run();

string GenerateJwtToken(User user)
{
    var claims = new List<Claim>
    {
        new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
        new Claim(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty),
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
    };

    var credentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

    var tokenDescriptor = new JwtSecurityToken(
        issuer: jwtIssuer,
        audience: jwtAudience,
        claims: claims,
        expires: DateTime.UtcNow.AddMinutes(jwtExpiresMinutes),
        signingCredentials: credentials
    );

    return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
}
