FROM mcr.microsoft.com/dotnet/core/sdk:2.2-alpine AS build

ENV PROJECT_PATH=SFA.DAS.ToolsNotifications.Api/SFA.DAS.ToolsNotifications.Api.csproj
COPY ./src ./src
WORKDIR /src

RUN dotnet restore $PROJECT_PATH
RUN dotnet build $PROJECT_PATH -c release --no-restore
RUN dotnet publish $PROJECT_PATH -c release --no-build -o /app

FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-alpine
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "SFA.DAS.ToolsNotifications.Api.dll"]