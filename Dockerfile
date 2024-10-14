# Create base image
FROM mcr.microsoft.com/dotnet/aspnet:8.0-noble-chiseled-extra AS base
USER app
WORKDIR /app

# Expose ports 
EXPOSE 8080
EXPOSE 8081

# Restore Dependencies 
FROM mcr.microsoft.com/dotnet/sdk:8.0-noble AS build
WORKDIR "/src"
COPY "Bogsi.Quotable.Web/Bogsi.Quotable.Web.csproj" "Bogsi.Quotable.Web/"
COPY "Bogsi.Quotable.Application/Bogsi.Quotable.Application.csproj" "Bogsi.Quotable.Application/"
COPY "Bogsi.Quotable.Infrastructure/Bogsi.Quotable.Infrastructure.csproj" "Bogsi.Quotable.Infrastructure/"
COPY "Bogsi.Quotable.Persistence/Bogsi.Quotable.Persistence.csproj" "Bogsi.Quotable.Persistence/"
RUN dotnet restore "Bogsi.Quotable.Web/Bogsi.Quotable.Web.csproj"

# Copy All Files 
COPY ./ ./

# Build DLL's 
WORKDIR "/src/Bogsi.Quotable.Web"
ARG BUILD_CONFIGURATION=Release
RUN dotnet build "Bogsi.Quotable.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish DLL's 
FROM build AS publish 
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Bogsi.Quotable.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish

# Create release image.
FROM base AS release
WORKDIR /app
COPY --from=publish /app/publish ./

# Start
ENTRYPOINT ["dotnet", "Bogsi.Quotable.Web.dll"]
