FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY Lab08RonaldoCusi.csproj ./
RUN dotnet restore Lab08RonaldoCusi.csproj
COPY . .
RUN dotnet publish Lab08RonaldoCusi.csproj -c Release --no-restore -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
ENV ASPNETCORE_URLS=http://0.0.0.0:10000
EXPOSE 10000
COPY --from=build /app/publish .
USER $APP_UID
ENTRYPOINT ["dotnet", "Lab08RonaldoCusi.dll"]
