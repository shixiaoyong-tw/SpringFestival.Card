FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /source

COPY . .

RUN dotnet restore

RUN dotnet publish -c release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 As runtime
EXPOSE 80
ENV ASPNETCORE_URLS="http://*:80"

WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "SpringFestival.Card.API.dll"]