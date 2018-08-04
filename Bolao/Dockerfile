FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 60879
EXPOSE 44341

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY Bolao/Bolao.csproj Bolao/
RUN dotnet restore Bolao/Bolao.csproj
COPY . .
WORKDIR /src/Bolao
RUN dotnet build Bolao.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Bolao.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Bolao.dll"]
