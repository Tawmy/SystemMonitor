FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY ["SystemMonitor/SystemMonitor.csproj", "SystemMonitor/"]
RUN dotnet restore "SystemMonitor/SystemMonitor.csproj"
COPY . .
WORKDIR "/src/SystemMonitor"
RUN dotnet build "SystemMonitor.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SystemMonitor.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SystemMonitor.dll"]
