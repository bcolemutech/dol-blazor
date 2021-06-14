# Build runtime image
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build_env
COPY . /app
WORKDIR /app
RUN dotnet publish -c Release -o out

# Run app
FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=build_env /app/out .
ENTRYPOINT ["dotnet", "DolBlazor.dll"]