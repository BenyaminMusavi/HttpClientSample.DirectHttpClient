using Polly;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle


//====================================== Add polly ========================================================

builder.Services.AddHttpClient("posts", client =>
{
    client.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/");

})
.AddTransientHttpErrorPolicy(policy =>
policy.WaitAndRetryAsync(new[] { // 3 bar wait kon
TimeSpan.FromMilliseconds(200), // dafe avval 200 ms retry mikne , age moshkel bar taeaf shod natije ro bar migardoone
TimeSpan.FromMilliseconds(500),
TimeSpan.FromSeconds(1)})
);
//  .AddHttpMessageHandler<LogHttpRequest>();

//====================================== Add polly ========================================================



builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
