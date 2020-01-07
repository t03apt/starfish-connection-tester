FROM mcr.microsoft.com/dotnet/core/runtime:3.0-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["StarfishConnectionTester/StarfishConnectionTester.csproj", "StarfishConnectionTester/"]
RUN dotnet restore "StarfishConnectionTester/StarfishConnectionTester.csproj"
COPY . .
WORKDIR "/src/StarfishConnectionTester"
RUN dotnet build "StarfishConnectionTester.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "StarfishConnectionTester.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "StarfishConnectionTester.dll"]