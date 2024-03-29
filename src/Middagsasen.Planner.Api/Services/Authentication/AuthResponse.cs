﻿namespace Middagsasen.Planner.Api.Services.Authentication
{
    public class AuthResponse
    {
        public AuthStatus Status { get; internal set; }
        public string? Token { get; internal set; }
    }

    public enum AuthStatus
    {
        Success,
        AuthenticationFailed,
        InvalidUsername,
    }
}