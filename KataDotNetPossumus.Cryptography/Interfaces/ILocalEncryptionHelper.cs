namespace KataDotNetPossumus.Cryptography.Interfaces;

public interface ILocalEncryptionHelper : IEncryptionHelper
{
	string Encrypt(int? plainValue);

	string Encrypt(string? plainText);
}