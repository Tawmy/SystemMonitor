﻿namespace SystemMonitor.Messages;

public static class Errors
{
    public static class Environment
    {
        public static string EnvironmentVariableNotSet(string name)
        {
            return $"Environment variable {name} not set.";
        }
    }
}