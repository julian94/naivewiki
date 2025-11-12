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

app.MapPut("/{path}", async (string path, HttpRequest request) => {
    await using var writeStream = File.Create(Path.Combine(webroot, path));
    await request.BodyReader.CopyToAsync(writeStream);
    return "ok";
});

app.MapGet("/{path}", async (string path) => {
    var filePath = Path.Combine(webroot, path);
    var contentType = Path.GetExtension(path) switch
    {
        ".html" => "text/html",
        ".htm" => "text/html",
        ".png" => "image/png",
        ".gif" => "image/gif",
        ".json" => "application/json",
        _ => "text/plain",
    };
    return Results.File(filePath, contentType);
});


app.Run();
