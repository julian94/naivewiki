using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//app.UseHttpsRedirection();
var webroot = "/app/wwwroot";

app.MapPost("/{path}", async (string path, HttpRequest request) => {
    await using var writeStream = File.Create(Path.Combine(webroot, path));
    await request.BodyReader.CopyToAsync(writeStream);
    return "ok";
});
app.MapGet("/{path}", async (string path) => Results.File(Path.Combine(webroot, path)));

app.Run();
