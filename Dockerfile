FROM mcr.microsoft.com/dotnet/sdk:10.0 AS dotnet-build
WORKDIR /build

COPY / .

RUN dotnet publish -c Release -o /publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime-env
WORKDIR /app

COPY --from=dotnet-build /publish .

ENTRYPOINT [ "dotnet", "naivewiki.dll" ]
