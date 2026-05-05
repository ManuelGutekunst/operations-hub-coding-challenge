FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY src/OperationsHub.Api/OperationsHub.Api.csproj src/OperationsHub.Api/
RUN dotnet restore src/OperationsHub.Api/OperationsHub.Api.csproj

COPY src/OperationsHub.Api/ src/OperationsHub.Api/
RUN dotnet publish src/OperationsHub.Api/OperationsHub.Api.csproj -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "OperationsHub.Api.dll"]
