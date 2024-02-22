using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Rebels.ShortLink.Api;
using Rebels.ShortLink.Api.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();
builder.Services.AddMemoryCache();
builder.Services.AddOptions<CounterConfig>()
                .BindConfiguration(nameof(CounterConfig))
                .ValidateDataAnnotations()
                .ValidateOnStart();
builder.Services.AddScoped<IShortLinkService, ShortLinkService>();
builder.Services.AddScoped<IEncodeShortLinkService, EncodeShortLinkService>();
builder.Services.AddScoped<ICounterService, CounterService>();

var app = builder.Build();

var counterConfig = app.Services.GetRequiredService<IOptions<CounterConfig>>().Value;
var cache = app.Services.GetRequiredService<IMemoryCache>();
var entryOptions = new MemoryCacheEntryOptions().SetPriority(CacheItemPriority.NeverRemove);
cache.Set(counterConfig.Key, counterConfig.StartingUid, entryOptions);

//pipeline configuration
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

// Make the implicit Program class public so test projects can access it
public partial class Program { }