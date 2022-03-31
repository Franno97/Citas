#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["NuGet.Config", "."]
COPY ["host/Mre.Sb.Appointment.HttpApi.Host/Mre.Sb.Appointment.HttpApi.Host.csproj", "host/Mre.Sb.Appointment.HttpApi.Host/"]
COPY ["src/Mre.Sb.UnidadAdministrativa.HttpApi.Client/Mre.Sb.UnidadAdministrativa.HttpApi.Client.csproj", "src/Mre.Sb.UnidadAdministrativa.HttpApi.Client/"]
COPY ["src/Mre.Sb.Appointment.Application/Mre.Sb.Appointment.Application.csproj", "src/Mre.Sb.Appointment.Application/"]
COPY ["src/Mre.Sb.Appointment.Domain/Mre.Sb.Appointment.Domain.csproj", "src/Mre.Sb.Appointment.Domain/"]
COPY ["src/Mre.Sb.Appointment.Domain.Shared/Mre.Sb.Appointment.Domain.Shared.csproj", "src/Mre.Sb.Appointment.Domain.Shared/"]
COPY ["src/Mre.Sb.Appointment.Application.Contracts/Mre.Sb.Appointment.Application.Contracts.csproj", "src/Mre.Sb.Appointment.Application.Contracts/"]
COPY ["host/Mre.Sb.Appointment.Host.Shared/Mre.Sb.Appointment.Host.Shared.csproj", "host/Mre.Sb.Appointment.Host.Shared/"]
COPY ["src/Mre.Sb.Appointment.HttpApi/Mre.Sb.Appointment.HttpApi.csproj", "src/Mre.Sb.Appointment.HttpApi/"]
COPY ["src/Mre.Sb.Appointment.EntityFrameworkCore/Mre.Sb.Appointment.EntityFrameworkCore.csproj", "src/Mre.Sb.Appointment.EntityFrameworkCore/"]
RUN dotnet restore "host/Mre.Sb.Appointment.HttpApi.Host/Mre.Sb.Appointment.HttpApi.Host.csproj"
COPY . .
WORKDIR "/src/host/Mre.Sb.Appointment.HttpApi.Host"
RUN dotnet build "Mre.Sb.Appointment.HttpApi.Host.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Mre.Sb.Appointment.HttpApi.Host.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Mre.Sb.Appointment.HttpApi.Host.dll"]