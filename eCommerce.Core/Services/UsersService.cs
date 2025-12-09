using AutoMapper;
using eCommerce.Core.DTO;
using eCommerce.Core.Entities;
using eCommerce.Core.RepositoryContracts;
using eCommerce.Core.ServiceContracts;

namespace eCommerce.Core.Services;

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
        ApplicationUser? user = await _usersRepository.GetUserByEmailAndPassword(loginRequest.Email, loginRequest.Password);

        if (user == null)
        {
            return new AuthenticationResponse
            {
                Success = false,
                Token = null
            };
        }

        // Generate JWT token
        string token = _jwtService.GenerateJwtToken(user.UserID, user.Email, user.PersonName);

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
        string token = _jwtService.GenerateJwtToken(registeredUser.UserID, registeredUser.Email, registeredUser.PersonName);

        // Return success response
        return _mapper.Map<AuthenticationResponse>(registeredUser) with
        {
            Success = true,
            Token = token
        };
    }
}