using Common;
using Common.Messaging;
using HandlerTemplate.Commands;
using HandlerTemplate.Services.AddCommand;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddEventHandlersAndNecessaryWork(typeof(AddCommandProcessor));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/TestHandler1", async (IServiceProvider _serviceProvider) =>
    {
        var messageContainer = new MessageContainer<AddCommand, CommandMetadata>(new AddCommand(4),
            new CommandMetadata([], string.Empty, Guid.NewGuid()), new MessageSource(Guid.NewGuid()));

        var handler =
            _serviceProvider.GetRequiredKeyedService<IMessageContainerHandler<AddCommand, CommandMetadata>>(
                nameof(AddCommand));

        await handler.HandleAsync(messageContainer);
    })
    .WithName("Test 1")
    .WithOpenApi();
//
// app.MapGet("/TestHandler2", async (IMessageContainerHandler<RemoveCommand, CommandMetadata> handler) =>
//     {
//        
//
//         await handler.HandleAsync(messageContainer);
//     })
//     .WithName("Test 2")
//     .WithOpenApi();
// //
app.Run();

namespace HandlerTemplate
{
    record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
    {
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
    }
}