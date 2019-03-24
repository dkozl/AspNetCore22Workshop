 # Stage 1
 FROM mcr.microsoft.com/dotnet/core/sdk AS builder
 WORKDIR /

 # copies the rest of your code
 COPY . .
 RUN dotnet publish --output /publish/ --configuration Release

 # Stage 2
 FROM mcr.microsoft.com/dotnet/core/aspnet
 WORKDIR /publish
 COPY --from=builder /publish .
 ENTRYPOINT ["dotnet", "Workshop.App.dll"]