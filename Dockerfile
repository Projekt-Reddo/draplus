FROM mcr.microsoft.com/dotnet/aspnet:6.0-focal AS base
WORKDIR /app
EXPOSE 7287
EXPOSE 5294

ENV ASPNETCORE_URLS=http://+:7287

# Creates a non-root user with an explicit UID and adds permission to access the /app folder
# For more info, please refer to https://aka.ms/vscode-docker-dotnet-configure-containers
RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

#restore
FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
WORKDIR /src
COPY *.sln .
COPY DraplusApiTest/*.csproj DraplusApiTest/
COPY DraplusApi/*.csproj DraplusApi/
RUN dotnet restore
COPY . .

#testing
FROM build as testing
WORKDIR /src/DraplusApi
RUN dotnet build
WORKDIR /src/DraplusApiTest
RUN dotnet test

#publish
FROM build AS publish
WORKDIR /src/DraplusApi
RUN dotnet publish -c Release -o /app/publish --runtime linux-x64 /p:UseAppHost=true

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
CMD ASPNETCORE_URLS=http://*:$PORT dotnet DraplusApi.dll --timezone "SE Asia Standard Time"
