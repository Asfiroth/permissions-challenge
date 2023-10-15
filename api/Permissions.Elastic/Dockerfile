FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Permissions.Elastic/Permissions.Elastic.csproj", "Permissions.Elastic/"]
RUN dotnet restore "Permissions.Elastic/Permissions.Elastic.csproj"
COPY . .
WORKDIR "/src/Permissions.Elastic"
RUN dotnet build "Permissions.Elastic.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Permissions.Elastic.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Permissions.Elastic.dll"]
