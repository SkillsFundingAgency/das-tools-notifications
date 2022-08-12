FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build

ENV PROJECT_PATH=SFA.DAS.ToolsNotifications.Api/SFA.DAS.ToolsNotifications.Api.csproj
COPY ./src ./src
COPY ./tests/Test-Packages.ps1 ./tests/Test-Packages.ps1
WORKDIR /
SHELL ["pwsh", "-Command", "$ErrorActionPreference = 'Stop'; $ProgressPreference = 'SilentlyContinue';"]

RUN dotnet restore src/$PROJECT_PATH
RUN ./tests/Test-Packages.ps1
RUN dotnet build src/$PROJECT_PATH -c release --no-restore
RUN dotnet publish src/$PROJECT_PATH -c release --no-build -o /app

FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "SFA.DAS.ToolsNotifications.Api.dll"]