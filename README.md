Pet project C#, for education:
    1 grpc
    2 Posgresql
    3 Cassandra
    4 Kafka for events.
    5 Docker + Kubernetes.
    6 Monitoring (Prometheus, Grafana).
    7 CQRS (mediatr) + Event Sourcing.
    8 Authorization (OAuth2/JWT).
    9 Load tests.

For start Db:
    docker-compose up -d
Check container:
    docker exec -it shop_postgres psql -U shop_user -d shop_db

For add migration:
    dotnet ef migrations add InitialCreate \
    --context ShopDbContext \
    --project Shop.Infrastructure/Shop.Infrastructure.csproj \
    --startup-project Shop.API/Shop.API.csproj

For update-database:
    dotnet ef database update \
    --context Shop.Infrastructure.DbContexts.ShopDbContext \
    --project Shop.Infrastructure/Shop.Infrastructure.csproj \
    --startup-project Shop.API/Shop.API.csproj
