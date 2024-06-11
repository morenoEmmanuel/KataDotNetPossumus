using KataDotNetPossumus.Model.DataTransferObject.General;

namespace KataDotNetPossumus.Model.DataTransferObject.Authentication;

public class DtoAuthenticationRequest : DtoSelfSerializable
{
	public string? Username { get; set; }
	public string? Password { get; set; }
}