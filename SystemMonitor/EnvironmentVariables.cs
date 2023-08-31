namespace SystemMonitor;

public static class EnvironmentVariables
{
    /// <summary>
    ///     Used for JWT Token validation.
    /// </summary>
    public const string AuthAudience = "AUTH_AUDIENCE";

    /// <summary>
    ///     Used for JWT Token validation.
    /// </summary>
    public const string AuthAuthority = "AUTH_AUTHORITY";

    /// <summary>
    ///     OIDC Client ID for Swagger UI. Optional.
    /// </summary>
    public const string SwaggerClientId = "SWAGGER_CLIENT_ID";

    /// <summary>
    ///     OIDC Client Secret for Swagger UI. Optional.
    /// </summary>
    public const string SwaggerClientSecret = "SWAGGER_CLIENT_SECRET";

    /// <summary>
    ///     OIDC auth endpoint URL.
    /// </summary>
    public const string SwaggerUrlAuth = "SWAGGER_URL_AUTH";

    /// <summary>
    ///     OIDC token endpoint URL.
    /// </summary>
    public const string SwaggerUrlToken = "SWAGGER_URL_TOKEN";

    /// <summary>
    ///     Monitor a remote server instead of the host. Optional.
    /// </summary>
    public const string RemoteServerUri = "REMOTE_SERVER_URI";
}