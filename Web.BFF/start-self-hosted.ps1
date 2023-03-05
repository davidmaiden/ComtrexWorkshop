dapr run `
    --app-id web-bff `
    --app-port 5045 `
    --dapr-http-port 3501 `
    --components-path ../dapr/components `
    dotnet run