FROM microsoft/dotnet:2.1-sdk-alpine AS build
WORKDIR /app

COPY Futures.sln .
COPY ./src ./src
RUN dotnet restore
RUN dotnet publish ./src/Client/Futures.Api/Futures.Api.csproj -c Release -o ../../out

FROM microsoft/dotnet:2.1-aspnetcore-runtime-alpine AS runtime
WORKDIR /app
COPY --from=build /app/src/out .
COPY init.sh .
CMD [ "/bin/ash", "init.sh" ]