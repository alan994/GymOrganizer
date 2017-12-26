FROM microsoft/dotnet:2.0-runtime-nanoserver-1709 AS base
WORKDIR /app

FROM microsoft/dotnet:2.0-sdk-nanoserver-1709 AS build
WORKDIR /src
COPY *.sln ./
COPY services/RabbitMQ/RabbitMQ.csproj services/RabbitMQ/
COPY Data/Data.csproj Data/
COPY shared/BusinessLogic/BusinessLogic.csproj shared/BusinessLogic/
COPY shared/Helper/Helper.csproj shared/Helper/
RUN dotnet restore
COPY . .
WORKDIR /src/services/RabbitMQ
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "RabbitMQ.dll"]
