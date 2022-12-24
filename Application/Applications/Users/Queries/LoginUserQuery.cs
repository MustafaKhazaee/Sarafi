
using AutoMapper;
using FluentValidation;
using MediatR;
using Sarafi.Application.Applications.Users.Dtos;
using Sarafi.Application.Common.MappingProfile;
using Sarafi.Application.Interfaces.Services;

namespace Sarafi.Application.Applications.Users.Queries
{
    public class LoginUserQuery : Mappable<LoginUserQuery, LoginDto>, IRequest<LoginResponse>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
    public class LoginUserQueryHandler : IRequestHandler<LoginUserQuery, LoginResponse>
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IMapper _mapper;
        private readonly IValidator<LoginUserQuery> _validator;
        public LoginUserQueryHandler(IAuthenticationService authenticationService, IMapper mapper, IValidator<LoginUserQuery> validator)
        {
            _authenticationService = authenticationService;
            _mapper = mapper;
            _validator = validator; 
        }
        public async Task<LoginResponse> Handle(LoginUserQuery request, CancellationToken cancellationToken)
        {
            var validation = await _validator.ValidateAsync(request);
            if (validation.IsValid)
            {
                var loginDto = _mapper.Map<LoginDto>(request);
                return await _authenticationService.AuthenticateAsync(loginDto);
            }
            return new LoginResponse { Response = $"{validation}" };
        }
    }
}
