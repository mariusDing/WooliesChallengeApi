FROM microsoft/dotnet:2.1-aspnetcore-runtime-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.1-sdk-stretch AS build
WORKDIR /src
COPY WooliesChallengeApi/WooliesChallengeApi.csproj WooliesChallengeApi/
RUN dotnet restore "WooliesChallengeApi/WooliesChallengeApi.csproj"
COPY . .
WORKDIR "/src/WooliesChallengeApi"
RUN dotnet build "WooliesChallengeApi.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "WooliesChallengeApi.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "WooliesChallengeApi.dll"]