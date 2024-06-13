namespace KataDotNetPossumus.Cryptography.Interfaces;

public interface IEncryptionHelper
{
	Task<int> DecryptAsIntAsync(string? cipherText, string? fieldName = null);
	Task<string> DecryptAsync(string? cipherText);
	Task<int> DecryptParamsAsIntAsync(string? cipherText, string? fieldName = null);
	Task<string?> DecryptParamsAsync(string? cipherText);
	Task<string> EncryptAsync(int plainValue);
	Task<string> EncryptAsync(string? plainText);
	Task<string> EncryptParamsAsync(int plainValue);
	Task<string> EncryptParamsAsync(string? plainText);
}