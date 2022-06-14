using TodoApp.Data.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var ConnectionString = builder.Configuration.GetConnectionString(name: "DefaultConnection");
builder.Services.AddDbContext<TodoApplicationDbContext>(options => options.UseMySql(connectionString: ConnectionString, ServerVersion.AutoDetect(ConnectionString)));
builder.Services.AddScoped<ITaskService, TaskServices>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapGet("/", () => "Hello world");
app.MapControllers();


app.Run();

public partial class Program { }
