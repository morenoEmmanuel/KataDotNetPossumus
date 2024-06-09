using KataDotNetPossumus.Cryptography.Interfaces;
using KataDotNetPossumus.Exceptions;
using KataDotNetPossumus.Resources;

namespace KataDotNetPossumus.Cryptography.Implementations;

public abstract class EncryptionHelper : IEncryptionHelper
{
	#region Public Methods

	public async Task<int> DecryptAsIntAsync(string? cipherText, string? fieldName = null)
	{
		if (string.IsNullOrWhiteSpace(cipherText)) return default;

		var decrypted = await DecryptAsync(cipherText);

		return int.TryParse(decrypted, out var value)
			? value
			: string.IsNullOrWhiteSpace(fieldName)
				? default
				: throw new NoNotifyException($"{Messages.NoValidValueEncrypted} - {fieldName}");
	}

	public async Task<int> DecryptParamsAsIntAsync(string? cipherText, string? fieldName = null)
	{
		var decrypted = await DecryptParamsAsync(cipherText);

		return int.TryParse(decrypted, out var value)
			? value
			: string.IsNullOrWhiteSpace(fieldName)
				? default
				: throw new NoNotifyException($"{Messages.NoValidValueEncrypted} - {fieldName}");
	}

	public async Task<string?> DecryptParamsAsync(string? cipherText)
	{
		if (string.IsNullOrWhiteSpace(cipherText)) return null;

		var unescaped = Uri.UnescapeDataString(cipherText);

		return await DecryptAsync(unescaped);
	}

	public async Task<string> EncryptAsync(int plainValue)
	{
		return await EncryptAsync(plainValue.ToString());
	}

	public async Task<string> EncryptParamsAsync(int plainValue)
	{
		return await EncryptParamsAsync(plainValue.ToString());
	}

	public async Task<string> EncryptParamsAsync(string? plainText)
	{
		var encrypted = await EncryptAsync(plainText);

		return Uri.EscapeDataString(encrypted);
	}

	#endregion

	#region Abstract Methods

	public abstract Task<string> DecryptAsync(string? cipherText);
	public abstract Task<string> EncryptAsync(string? plainText);

	#endregion
}