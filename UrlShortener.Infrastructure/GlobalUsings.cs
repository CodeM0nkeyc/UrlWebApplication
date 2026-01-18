// Global using directives

global using System.Security.Claims;
global using System.Security.Cryptography;
global using System.Text;
global using Microsoft.AspNetCore.Authentication.Cookies;
global using Microsoft.AspNetCore.Authorization;
global using Microsoft.AspNetCore.Http;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore.ChangeTracking;
global using Microsoft.EntityFrameworkCore.Metadata.Builders;
global using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using UrlShortener.Infrastructure.Persistence.DbContext;
global using UrlShortener.Infrastructure.Persistence.Entities;
global using UrlShortener.Infrastructure.Persistence.Entities.Common;
global using UrlShortener.Infrastructure.Persistence.Repositories.About;
global using UrlShortener.Infrastructure.Persistence.Repositories.Url;
global using UrlShortener.Infrastructure.Persistence.Repositories.User;
global using UrlShortener.Infrastructure.Services.Security.Authentication;
global using UrlShortener.Infrastructure.Services.Security.Authorization.Handlers;
global using UrlShortener.Infrastructure.Services.Security.PasswordHasher;
global using UrlShortener.Infrastructure.Services.Security.UserDataAccessor;
global using UrlShortener.Infrastructure.Services.Url;