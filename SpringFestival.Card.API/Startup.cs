using System;
using System.Collections.Generic;
using System.Threading;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using SpringFestival.Card.Common.Enums;
using SpringFestival.Card.Service;
using SpringFestival.Card.Service.Implements;
using SpringFestival.Card.Storage;
using SpringFestival.Card.Storage.Implements;


namespace SpringFestival.Card.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "SpringFestival.Card.API", Version = "v1"});
            });

            services.AddAutoMapper(typeof(AutoMapperProfile));

            services.AddScoped<ICardService, CardService>();
            services.AddScoped<ICardRepository, CardRepository>();
            services.AddScoped<IAudienceService, AudienceService>();
            services.AddScoped<IAudienceRepository, AudienceRepository>();

            services.AddSingleton(new AppSettings(Configuration));

            services.AddAWSService<IAmazonDynamoDB>(Configuration.GetAWSOptions("DynamoDb"));

            // CreateTable();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SpringFestival.Card.API v1"));

            // app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }

        /// <summary>
        /// init DynamoDB table
        /// </summary>
        private void CreateTable()
        {
            var dynamoDbConfig = Configuration.GetSection("DynamoDb");

            var clientConfig = new AmazonDynamoDBConfig
            {
                ServiceURL = dynamoDbConfig.GetValue<string>("ServiceURL")
            };
            var client = new AmazonDynamoDBClient(clientConfig);
            CreateTable("Card", "EntityId");
            CreateTable("Audience", "EntityId");

            void CreateTable(string tableName, string hashKey)
            {
                // Low Level API: client
                Console.WriteLine("Verify table => " + tableName);
                var tableResponse = client.ListTablesAsync().Result;

                // client.DeleteTableAsync(tableName);
                // return;
                
                if (!tableResponse.TableNames.Contains(tableName))
                {
                    Console.WriteLine("Table not found, creating table => " + tableName);
                    client.CreateTableAsync(new CreateTableRequest
                    {
                        TableName = tableName,
                        ProvisionedThroughput = new ProvisionedThroughput
                        {
                            ReadCapacityUnits = 3,
                            WriteCapacityUnits = 1
                        },
                        KeySchema = new List<KeySchemaElement>
                        {
                            new KeySchemaElement
                            {
                                AttributeName = hashKey,
                                KeyType = KeyType.HASH
                            }
                        },
                        AttributeDefinitions = new List<AttributeDefinition>
                        {
                            new AttributeDefinition
                            {
                                AttributeName = hashKey,
                                AttributeType = ScalarAttributeType.S
                            }
                        }
                    });

                    var isTableAvailable = false;
                    while (!isTableAvailable)
                    {
                        Console.WriteLine("Waiting for table to be active...");
                        Thread.Sleep(2000);
                        var tableStatus = client.DescribeTableAsync(tableName).Result;
                        isTableAvailable = tableStatus.Table.TableStatus == "ACTIVE";
                    }
                }
            }
        }
    }
}