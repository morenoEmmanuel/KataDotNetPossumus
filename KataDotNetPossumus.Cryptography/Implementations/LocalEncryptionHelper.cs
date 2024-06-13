using KataDotNetPossumus.Cryptography.Interfaces;
using KataDotNetPossumus.SettingHelper;
using System.Security.Cryptography;
using System.Text;

namespace KataDotNetPossumus.Cryptography.Implementations;

public class LocalEncryptionHelper : EncryptionHelper, ILocalEncryptionHelper
{
	#region Dependencies

	private readonly AppSettingHelper appSettingHelper;

	#endregion

	#region Properties

	private static readonly Dictionary<string, string> keysMap = new();
	private static readonly object toLock = new();

	#endregion

	#region Constructors

	public LocalEncryptionHelper(AppSettingHelper appSettingHelper)
	{
		this.appSettingHelper = appSettingHelper;
	}

	#endregion

	#region Public Methods

	public override Task<string> DecryptAsync(string? cipherText)
	{
		if (string.IsNullOrWhiteSpace(cipherText)) return Task.FromResult(string.Empty);

		lock (toLock)
		{
			return Task.FromResult(GetByEncryptedValue(cipherText));
		}
	}

	public string Encrypt(int? plainValue)
	{
		return Encrypt(plainValue?.ToString());
	}

	public string Encrypt(string? plainText)
	{
		if (string.IsNullOrWhiteSpace(plainText)) return string.Empty;

		return GetByOriginalValue(plainText);
	}

	public override Task<string> EncryptAsync(string? plainText)
	{
		if (string.IsNullOrWhiteSpace(plainText)) return Task.FromResult(string.Empty);

		lock (toLock)
		{
			return Task.FromResult(GetByOriginalValue(plainText));
		}
	}

	#endregion

	#region Private Methods

	private string DecryptValue(string encryptedText)
	{
		var escapedString = Uri.UnescapeDataString(encryptedText).Replace('_', '/').Replace('-', '+');

		switch (escapedString.Length % 4)
		{
			case 2:
				escapedString += "==";
				break;
			case 3:
				escapedString += "=";
				break;
		}

		var cipherTextBytes = Convert.FromBase64String(escapedString);
		var keyBytes = new Rfc2898DeriveBytes(appSettingHelper.LocalEncryptionKey, Encoding.ASCII.GetBytes(appSettingHelper.LocalEncryptionSaltKey)).GetBytes(256 / 8);

#pragma warning disable SYSLIB0022
		var symmetricKey = new RijndaelManaged
#pragma warning restore SYSLIB0022
		{
			Mode = CipherMode.CBC,
			Padding = PaddingMode.None
		};

		var cryptoTransform = symmetricKey.CreateDecryptor(keyBytes, Encoding.ASCII.GetBytes(appSettingHelper.LocalEncryptionViKey));

		var memoryStream = new MemoryStream(cipherTextBytes);
		var cryptoStream = new CryptoStream(memoryStream, cryptoTransform, CryptoStreamMode.Read);
		var plainTextBytes = new byte[cipherTextBytes.Length];

		var decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
		memoryStream.Close();
		cryptoStream.Close();

		return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount).TrimEnd("\0".ToCharArray());
	}

	private string EncryptValue(string sourceText)
	{
		var plainTextBytes = Encoding.UTF8.GetBytes(sourceText);

		var keyBytes = new Rfc2898DeriveBytes(appSettingHelper.LocalEncryptionKey, Encoding.ASCII.GetBytes(appSettingHelper.LocalEncryptionSaltKey)).GetBytes(256 / 8);

#pragma warning disable SYSLIB0022
		var symmetricKey = new RijndaelManaged
#pragma warning restore SYSLIB0022
		{
			Mode = CipherMode.CBC,
			Padding = PaddingMode.Zeros
		};

		var cryptoTransform = symmetricKey.CreateEncryptor(keyBytes, Encoding.ASCII.GetBytes(appSettingHelper.LocalEncryptionViKey));

		byte[] cipherTextBytes;

		using (var memoryStream = new MemoryStream())
		{
			using (var cryptoStream = new CryptoStream(memoryStream, cryptoTransform, CryptoStreamMode.Write))
			{
				cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
				cryptoStream.FlushFinalBlock();
				cipherTextBytes = memoryStream.ToArray();

				cryptoStream.Close();
			}

			memoryStream.Close();
		}

		var unescapedBase64String = Convert.ToBase64String(cipherTextBytes).TrimEnd('=').Replace('+', '-').Replace('/', '_');

		return Uri.EscapeDataString(unescapedBase64String);
	}

	private string GetByEncryptedValue(string encryptedValue)
	{
		if (keysMap.ContainsValue(encryptedValue))
		{
			return keysMap.FirstOrDefault(p => p.Value == encryptedValue).Key;
		}

		var originalValue = DecryptValue(encryptedValue);

		if (!keysMap.ContainsKey(originalValue))
		{
			keysMap.Add(originalValue, encryptedValue);
		}

		return keysMap.FirstOrDefault(p => p.Value == encryptedValue).Key;
	}

	private string GetByOriginalValue(string originalValue)
	{
		if (!keysMap.ContainsKey(originalValue))
		{
			keysMap.Add(originalValue, EncryptValue(originalValue));
		}

		return keysMap[originalValue];
	}

	#endregion
}