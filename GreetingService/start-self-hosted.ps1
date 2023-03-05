dapr run `
    --app-id greeting-service `
    --app-port 5083 `
    --dapr-http-port 3502 `
    --components-path ../dapr/components `
    dotnet run