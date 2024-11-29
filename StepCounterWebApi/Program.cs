using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
                                {
                                    options.SwaggerDoc("v1", new OpenApiInfo
                                    {
                                        Version = "v1",
                                        Title = "Step Counter API",
                                        Description = "count steps of your team mates ;)",
                                        TermsOfService = new Uri("https://example.com/terms"),
                                        Contact = new OpenApiContact
                                        {
                                            Name = "Example Contact",
                                            Url = new Uri("https://example.com/contact")
                                        },
                                        License = new OpenApiLicense
                                        {
                                            Name = "Example License",
                                            Url = new Uri("https://example.com/license")
                                        }
                                    });

                                    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                                    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
                                });

var app = builder.Build();

app.UseSwagger(options =>
                {
                    options.SerializeAsV2 = true;
                });
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
