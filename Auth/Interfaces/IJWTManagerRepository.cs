using System;
using elearning_platform.DTO;
using elearning_platform.Models;

namespace elearning_platform.Auth
{
    public interface IJWTManagerRepository
    {
        Task<JWTToken?> Authenticate(LoginDTO user);
    }
}