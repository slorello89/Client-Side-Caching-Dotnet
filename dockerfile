FROM mcr.microsoft.com/dotnet/sdk:6.0


WORKDIR /app
ADD . /app

ENV ASPNETCORE_URLS http://*:5000
RUN ls /app
RUN dotnet dev-certs https --trust
RUN dotnet restore /app/ClientSideCaching.csproj

ENTRYPOINT ["dotnet","/app/bin/Debug/net6.0/ClientSideCaching.dll"]