using KataDotNetPossumus.Model.DataTransferObject.Authentication;

namespace KataDotNetPossumus.Business.Interfaces;

public interface IAuthenticationBusiness
{
	Task<DtoAuthenticationResponse?> AuthenticateAsync(DtoAuthenticationRequest? requestData);
}