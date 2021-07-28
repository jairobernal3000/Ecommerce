FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Ecommerce.Api.Customers/Ecommerce.Api.Customers.csproj", "Ecommerce.Api.Customers/"]
RUN dotnet restore "Ecommerce.Api.Customers/Ecommerce.Api.Customers.csproj"
COPY . .
WORKDIR "/src/Ecommerce.Api.Customers"
RUN dotnet build "Ecommerce.Api.Customers.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Ecommerce.Api.Customers.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ecommerce.Api.Customers.dll"]
