using Confluent.Kafka;
using Microsoft.EntityFrameworkCore;
using StudentApi.BackgroudJob;
using StudentApi.Context;
using StudentApi.Implementation;
using StudentApi.Interface;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<MyContext>(option => option.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
    ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection")), x => x.MigrationsAssembly(typeof(MyContext).Assembly.FullName)));
builder.Services.AddScoped<IStudentRepository, StudentRepository>();

var clientConfig = new ClientConfig();
clientConfig.BootstrapServers = builder.Configuration["ClientConfigurations:BootstrapServers"];
clientConfig.SecurityProtocol = SecurityProtocol.SaslSsl;
clientConfig.SaslMechanism = SaslMechanism.Plain;
clientConfig.SaslUsername = builder.Configuration["ClientConfigurations:SaslUsername"];
clientConfig.SaslPassword = builder.Configuration["ClientConfigurations:SaslPassword"];
clientConfig.SslCaLocation = builder.Configuration["ClientConfigurations:SslCaLocation"];

var consumerConfig = new ConsumerConfig(clientConfig);
consumerConfig.GroupId = builder.Configuration["ClientConfigurations:GroupId"];
consumerConfig.AutoOffsetReset = AutoOffsetReset.Earliest;

builder.Services.AddSingleton<IProducer<Null, string>>(x => new ProducerBuilder<Null, string>(clientConfig).Build());
builder.Services.AddSingleton<IConsumer<Null, string>>( x => new ConsumerBuilder<Null, string>(consumerConfig).Build());

builder.Services.AddScoped<IStudentDataPublisher, StudentDataPublisher>();
builder.Services.AddHostedService<ConsumerBackgroundService>();

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

