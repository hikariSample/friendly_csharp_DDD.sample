#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
#EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Fs.AuthServer/Fs.AuthServer.csproj", "Fs.AuthServer/"]
RUN dotnet restore "Fs.AuthServer/Fs.AuthServer.csproj"
COPY . .
WORKDIR "/src/Fs.AuthServer"
RUN dotnet build "Fs.AuthServer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Fs.AuthServer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "Fs.Admin.Web.dll"]
# Use the following instead for Heroku
CMD ASPNETCORE_URLS=http://*:$PORT dotnet Fs.AuthServer.dll
