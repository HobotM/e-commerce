using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Ecommerce.Api.Data;


var builder = WebApplication.CreateBuilder(args);

// ✅ 1. Add EF Core + SQL Server using connection string from appsettings.json
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ✅ 2. Add Identity (built-in user management system)
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
.AddEntityFrameworkStores<AppDbContext>();

// ✅ 3. Configure JWT Authentication
var jwtKey = builder.Configuration["Jwt:Key"];
builder.Services.AddAuthentication(options => {
// Set default authentication to JWT Bearer
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

})
.AddJwtBearer(options => {
    // Token validation rules
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey!)),
        ValidateIssuer = false, // We'll skip issuer/audience checks for now
        ValidateAudience = false
    };
});


// ✅ 4. Add API controllers and Swagger support
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();


// ✅ 5. Build the app
var app = builder.Build();


// ✅ 6. Swagger middleware (only for development)

app.UseSwagger();
app.UseSwaggerUI();

// ✅ 7. Enable Authentication and Authorization middleware
app.UseAuthentication();
app.UseAuthorization();

// ✅ 8. Map all API routes from controllers
app.MapControllers();
// ✅ 9. Run the app
app.Run();