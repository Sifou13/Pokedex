FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /Pokedex

COPY . ./
RUN dotnet restore ./Pokedex.Api
RUN dotnet publish ./Pokedex.Api -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR Pokedex.Api
COPY --from=build-env Pokedex/out .

ENTRYPOINT ["dotnet", "Pokedex.Api.dll"]