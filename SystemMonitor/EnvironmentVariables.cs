namespace SystemMonitor;

public static class EnvironmentVariables
{
    /// <summary>
    ///     Used for JWT Token validation. Optional.
    /// </summary>
    /// <remarks>
    /// If this and <see cref="AuthAuthority"/> are set, client authentication is enabled.
    /// </remarks>
    public const string AuthAudience = "AUTH_AUDIENCE";

    /// <summary>
    ///     Used for JWT Token validation. Optional.
    /// </summary>
    /// <remarks>
    /// If this and <see cref="AuthAudience"/> are set, client authentication is enabled.
    /// </remarks>
    public const string AuthAuthority = "AUTH_AUTHORITY";

    /// <summary>
    ///     OIDC Client ID for Swagger UI. Optional.
    /// </summary>
    /// <remarks>
    /// Only in effect if <see cref="SwaggerClientId"/> and <see cref="SwaggerClientSecret"/> are set.
    /// </remarks>
    public const string SwaggerClientId = "SWAGGER_CLIENT_ID";

    /// <summary>
    ///     OIDC Client Secret for Swagger UI. Optional.
    /// </summary>
    /// <remarks>
    ///     Only in effect if <see cref="SwaggerClientId"/> and <see cref="SwaggerClientSecret"/> are set.
    ///     <br/><br/>
    ///     Do not set this if Swagger is available outside of your network.
    /// </remarks>
    public const string SwaggerClientSecret = "SWAGGER_CLIENT_SECRET";

    /// <summary>
    ///     OIDC auth endpoint URL. Optional.
    /// </summary>
    /// <remarks>
    /// If this and <see cref="SwaggerUrlToken"/> are set, Swagger will show the "authorize" button.
    /// </remarks>
    public const string SwaggerUrlAuth = "SWAGGER_URL_AUTH";

    /// <summary>
    ///     OIDC token endpoint URL. Optional.
    /// </summary>
    /// <remarks>
    /// If this and <see cref="SwaggerUrlAuth"/> are set, Swagger will show the "authorize" button.
    /// </remarks>
    public const string SwaggerUrlToken = "SWAGGER_URL_TOKEN";

    /// <summary>
    ///     Monitor a remote server instead of the host. Optional.
    /// </summary>
    public const string RemoteServerUri = "REMOTE_SERVER_URI";
}