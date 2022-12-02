using REST_Web_API;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder();
string connection = "Data Source=DESKTOP-3MIJ6C8\\SQLEXPRESS;Initial Catalog=������;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;";
builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connection));

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

app.MapGet("/api/pumps", async (ApplicationContext db) => await db.Pumps.ToListAsync());

app.MapGet("/api/pumps/{id:int}", async (int id, ApplicationContext db) =>
{
    // �������� ������������ �� id
    Pump? pump = await db.Pumps.FirstOrDefaultAsync(u => u.Id == id);

    // ���� �� ������, ���������� ��������� ��� � ��������� �� ������
    if (pump == null) return Results.NotFound(new { message = "������������ �� ������" });

    // ���� ������������ ������, ���������� ���
    return Results.Json(pump);
});

app.MapDelete("/api/pumps/{id:int}", async (int id, ApplicationContext db) =>
{
    // �������� ������������ �� id
    Pump? pump = await db.Pumps.FirstOrDefaultAsync(u => u.Id == id);

    // ���� �� ������, ���������� ��������� ��� � ��������� �� ������
    if (pump == null) return Results.NotFound(new { message = "����� �� ������" });

    // ���� ������������ ������, ������� ���
    db.Pumps.Remove(pump);
    await db.SaveChangesAsync();
    return Results.Json(pump);
});

app.MapPost("/api/pumps", async (Pump pump, ApplicationContext db) =>
{
    // ��������� ������������ � ������
    await db.Pumps.AddAsync(pump);
    await db.SaveChangesAsync();
    return pump;
});

app.MapPut("/api/users", async (Pump pumpData, ApplicationContext db) =>
{
    // �������� ������������ �� id
    var pump = await db.Pumps.FirstOrDefaultAsync(u => u.Id == pumpData.Id);

    // ���� �� ������, ���������� ��������� ��� � ��������� �� ������
    if (pump == null) return Results.NotFound(new { message = "����� �� ������" });

    // ���� ������������ ������, �������� ��� ������ � ���������� ������� �������
    pump.H = pumpData.H;
    pump.Name = pumpData.Name;
    pump.Q = pumpData.Q;
    await db.SaveChangesAsync();
    return Results.Json(pump);
});

app.Run();
