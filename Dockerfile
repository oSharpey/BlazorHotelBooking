#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["BlazorHotelBooking/Server/BlazorHotelBooking.Server.csproj", "BlazorHotelBooking/Server/"]
COPY ["BlazorHotelBooking/Client/BlazorHotelBooking.Client.csproj", "BlazorHotelBooking/Client/"]
COPY ["BlazorHotelBooking/Shared/BlazorHotelBooking.Shared.csproj", "BlazorHotelBooking/Shared/"]
RUN dotnet restore "./BlazorHotelBooking/Server/./BlazorHotelBooking.Server.csproj"
COPY . .
WORKDIR "/src/BlazorHotelBooking/Server"
RUN dotnet build "./BlazorHotelBooking.Server.csproj" -c Release -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./BlazorHotelBooking.Server.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BlazorHotelBooking.Server.dll"]