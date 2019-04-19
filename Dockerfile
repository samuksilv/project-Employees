FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY ["Employees.API/Employees.API.csproj", "Employees.API/"]
RUN dotnet restore "Employees.API/Employees.API.csproj"
COPY . .
WORKDIR "/src/Employees.API"
RUN dotnet build "Employees.API.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Employees.API.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Employees.API.dll"]
