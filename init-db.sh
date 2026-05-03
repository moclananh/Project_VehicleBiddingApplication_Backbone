#!/bin/bash
# Start SQL Server in background
/opt/mssql/bin/sqlservr &

# Wait for SQL Server to be ready
echo "Waiting for SQL Server to start..."
sleep 30

# Check if database already exists
DB_EXISTS=$(/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P StrongPassword123! -Q "SELECT name FROM sys.databases WHERE name = 'BiddingDb2'" | grep -c "BiddingDb2")

if [ "$DB_EXISTS" -eq 0 ]; then
    echo "Database does not exist. Initializing database..."
    /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P StrongPassword123! -i /tmp/BiddingDb2_v2.0.sql
    echo "Database initialization complete."
else
    echo "Database already exists. Skipping initialization."
fi

# Keep the container running
wait