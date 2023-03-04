dapr run `
    --app-id catalog `
    --app-port 5045 `
    --dapr-http-port 3501 `
    --components-path ../dapr/components `
    dotnet run