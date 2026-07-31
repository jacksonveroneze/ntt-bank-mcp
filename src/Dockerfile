# =====================================================
# Build + Publish
# =====================================================
FROM mcr.microsoft.com/dotnet/sdk:10.0-noble AS publish

WORKDIR /src

COPY ./main/Mcp/Mcp.csproj ./main/Mcp/
COPY ./main/Application/Application.csproj ./main/Application/
COPY ./main/Domain/Domain.csproj ./main/Domain/
COPY ./main/Infrastructure/Infrastructure.csproj ./main/Infrastructure/
COPY *.slnx ./
COPY nuget.config ./
COPY Directory.Build.props ./
COPY Directory.Packages.props ./

RUN dotnet restore ./main/Mcp/Mcp.csproj \
    -r linux-x64 \
    -p:PublishReadyToRun=true

COPY . .

RUN dotnet publish ./main/Mcp/Mcp.csproj \
    -c Release \
    -r linux-x64 \
    --self-contained false \
    --no-restore \
    -p:PublishReadyToRun=true \
    -p:PublishTrimmed=false \
    -p:DebugType=None \
    -p:DebugSymbols=false \
    --output /publish

# =====================================================
# Etapa Final: Imagem de Runtime
# =====================================================
# FROM mcr.microsoft.com/dotnet/aspnet:10.0-noble-chiseled-extra AS runtime

# ENV ASPNETCORE_ENVIRONMENT=Production \
#     DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false \
#     DOTNET_GCDynamicAdaptationMode=1 \
#     DOTNET_GCHeapHardLimitPercent=0x4B \
#     DOTNET_TieredCompilation=1 \
#     DOTNET_TC_QuickJitForLoops=1 \
#     DOTNET_EnableDiagnostics=1 \
#     CORECLR_ENABLE_PROFILING=0 \
#     TZ=America/Sao_Paulo \
#     LANG=pt_BR.UTF-8 \
#     LANGUAGE=pt_BR.UTF-8 \
#     LC_ALL=pt_BR.UTF-8

FROM jacksonveroneze/aspnet:10.0-alpine-1.5.0

ENV DOTNET_GCDynamicAdaptationMode=1 \
    DOTNET_GCHeapHardLimitPercent=0x4B \
    DOTNET_TieredCompilation=1 \
    DOTNET_TC_QuickJitForLoops=1

ENV ASSEMBLY_NAME=/home/app/Mcp.dll

COPY --from=publish /publish /home/app

# WORKDIR /home/app

# ENTRYPOINT ["dotnet", "/home/app/Mcp.dll"]
