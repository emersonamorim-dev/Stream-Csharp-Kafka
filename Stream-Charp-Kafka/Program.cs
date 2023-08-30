using System;
using Confluent.Kafka;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Carrega as configurações do Kafka do appsettings.json
var kafkaConfig = builder.Configuration.GetSection("Kafka").Get<ProducerConfig>();
builder.Services.AddSingleton(kafkaConfig);

// Adiciona o produtor ao container de injeção de dependência
builder.Services.AddSingleton<IProducer<Null, string>>(sp =>
    new ProducerBuilder<Null, string>(sp.GetRequiredService<ProducerConfig>()).Build());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

