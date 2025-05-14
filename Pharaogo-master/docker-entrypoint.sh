#!/bin/sh
set -e

echo "Waiting for SQL Server to be ready..."
while ! nc -z db 1433; do
  sleep 1
done
echo "SQL Server is up. Running migrations..."
sleep 5

echo "Starting the app..."
exec dotnet WebApplication7.dll