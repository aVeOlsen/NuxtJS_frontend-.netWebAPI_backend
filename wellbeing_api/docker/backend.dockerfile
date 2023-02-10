FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
EXPOSE 5001
ENV ASPNETCORE_URLS=http://+:5001

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /var/www
COPY ["wellbeing_api/wellbeing_api.csproj", "wellbeing_api/"]
RUN dotnet restore "wellbeing_api/wellbeing_api.csproj"
COPY . .
WORKDIR "/var/www/wellbeing_api"
RUN dotnet build "wellbeing_api.csproj" -c Release -o /app/wellbeing_api/build

FROM build AS publish
RUN dotnet publish "wellbeing_api.csproj" -c Release -o /app/wellbeing_api/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app/wellbeing_api
COPY --from=publish /app/wellbeing_api/publish .
ENTRYPOINT ["dotnet", "wellbeing_api.dll"]



# # syntax=docker/dockerfile:1
# FROM mcr.microsoft.com/dotnet/sdk:6.0
# COPY ./wellbeing_api /var/www/wellbeing_api
# WORKDIR /var/www/wellbeing_api
# RUN dotnet dev-certs https --trust
# RUN ["dotnet", "restore"]
# RUN ["dotnet", "build"]
# EXPOSE 5001
# ENV ASPNETCORE_URLS=http://+:5001
# # RUN chmod +x ./entrypoint.sh
# # CMD /bin/bash ./entrypoint.sh             

# # Denne virker ikke endnu, men forhåbentligt, kan jeg få den til at virke på et senere tidspunkt. Da den er en del nemmere at forklare.    
# # Er ikke sikker på at publish er nødvendigt, som vi gør brug af for oven. ^^(For at denne skal virke skal der være et entrypoint script, der laver en dotnet run. Eller mere simpelt bør dette bare køres i run.sh) 