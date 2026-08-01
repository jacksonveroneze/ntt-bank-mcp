using NttBankMcp.Mcp.Extensions;

var builder = WebApplication
    .CreateSlimBuilder(args);

builder.Configure();

var app = builder.Build();

app.Configure();

await app.RunAsync();
