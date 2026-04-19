FROM --platform=linux/amd64 node:22-alpine AS frontend-build
WORKDIR /app
COPY learning-coach-client/package*.json ./
RUN npm ci
COPY learning-coach-client/ ./
RUN npm run build

FROM --platform=linux/amd64 mcr.microsoft.com/dotnet/sdk:10.0-alpine AS backend-build
WORKDIR /src
COPY learning-coach-api/ ./
RUN dotnet publish -c Release -o /app/publish

FROM --platform=linux/amd64 mcr.microsoft.com/dotnet/aspnet:10.0-alpine
WORKDIR /app
COPY --from=backend-build /app/publish .
COPY --from=frontend-build /app/dist ./wwwroot
ENV ASPNETCORE_ENVIRONMENT=Production
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
ENTRYPOINT ["dotnet", "LearningCoachAPI.dll"]
