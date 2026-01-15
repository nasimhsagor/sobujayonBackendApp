using AutoMapper;
using sobujayonApp.Core.DTO;
using sobujayonApp.Core.Entities;
using sobujayonApp.Core.RepositoryContracts;
using sobujayonApp.Core.ServiceContracts;

namespace sobujayonApp.Core.Services;

internal class UsersService : IUsersService
{
    private readonly IUsersRepository _usersRepository;
    private readonly IMapper _mapper;
    private readonly IJwtService _jwtService;

    public UsersService(IUsersRepository usersRepository, IMapper mapper, IJwtService jwtService)
    {
        _usersRepository = usersRepository;
        _mapper = mapper;
        _jwtService = jwtService;
    }


    public async Task<AuthenticationResponse?> Login(LoginRequest loginRequest)
    {

        ApplicationUser? user = await _usersRepository.GetUserByEmail(loginRequest.Email);

        // Verify the user exists AND the password matches the stored hash
        if (user == null || !BCrypt.Net.BCrypt.Verify(loginRequest.Password, user.Password))
        {
            return new AuthenticationResponse
            {
                Success = false,
                Token = null
            };
        }

        string token = _jwtService.GenerateJwtToken(user.Id, user.Email, user.Name);

        return _mapper.Map<AuthenticationResponse>(user) with
        {
            Success = true,
            Token = token
        };
    }

    public async Task<AuthenticationResponse?> Register(RegisterRequest registerRequest)
    {
        // Create a new ApplicationUser object from RegisterRequest
        ApplicationUser user = _mapper.Map<ApplicationUser>(registerRequest);

        // HASH THE PASSWORD before sending it to the repository
        user.Password = BCrypt.Net.BCrypt.HashPassword(registerRequest.Password);

        ApplicationUser? registeredUser = await _usersRepository.AddUser(user);

        if (registeredUser == null)
        {
            return new AuthenticationResponse
            {
                Success = false,
                Token = null
            };
        }

        // Generate JWT token
        string token = _jwtService.GenerateJwtToken(registeredUser.Id, registeredUser.Email, registeredUser.Name);

        // Return success response
        return _mapper.Map<AuthenticationResponse>(registeredUser) with
        {
            Success = true,
            Token = token
        };
    }

    //public async Task<AuthenticationResponse?> Login(LoginRequest loginRequest)
    //{
    //    ApplicationUser? user = await _usersRepository.GetUserByEmailAndPassword(loginRequest.Email, loginRequest.Password);

    //    if (user == null || !BCrypt.Net.BCrypt.Verify(loginRequest.Password, user.Password))
    //    {
    //        return new AuthenticationResponse
    //        {
    //            Success = false,
    //            Token = null
    //        };
    //    }

    //    // Generate JWT token
    //    string token = _jwtService.GenerateJwtToken(user.Id, user.Email, user.Name);

    //    return _mapper.Map<AuthenticationResponse>(user) with
    //    {
    //        Success = true,
    //        Token = token
    //    };
    //}
}