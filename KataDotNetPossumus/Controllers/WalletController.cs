using KataDotNetPossumus.Business.Interfaces;
using KataDotNetPossumus.Model.DataTransferObject.Wallet;
using Microsoft.AspNetCore.Mvc;

namespace KataDotNetPossumus.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class WalletController : BaseController
	{
		#region Dependencies

		private readonly IWalletBusiness walletBusiness;

		#endregion

		#region Constructors

		public WalletController(IWalletBusiness walletBusiness)
		{
			this.walletBusiness = walletBusiness;
		}

		#endregion

		#region Endpoints

		/// <summary>
		/// Gets the balance information by user ID.
		/// </summary>
		/// <param name="idUser">
		///		<para>The user ID.</para>
		/// </param>
		/// <returns>The balance information.</returns>
		[HttpGet("balance/{idUser}")]
		public async Task<DtoBalance> BalanceByUser([FromRoute] int? idUser)
		{
			return await walletBusiness.GetBalanceByUserAsync(idUser);
		}

		/// <summary>
		/// Deposits an amount into a wallet.
		/// </summary>
		/// <param name="request">
		///		<para>The request data.</para>
		/// </param>
		/// <returns>HTTP Code 200 if the deposit is successful.</returns>
		[HttpPost("deposit")]
		public async Task Deposit([FromBody] DtoTransactionRequest? request)
		{
			await walletBusiness.DepositAsync(request);
		}

		/// <summary>
		/// Exchanges currencies of a wallet.
		/// </summary>
		/// <param name="request">
		///		<para>The request data.</para>
		/// </param>
		/// <returns>HTTP Code 200 if the deposit is successful.</returns>
		[HttpPut("exchanges")]
		public async Task Exchanges([FromBody] DtoExchangeTransaction? request)
		{
			await walletBusiness.ExchangeAsync(request);
		}

		/// <summary>
		/// Withdraws an amount from a wallet.
		/// </summary>
		/// <param name="request">
		///		<para>The request data.</para>
		/// </param>
		/// <returns>HTTP Code 200 if the withdraw is successful.</returns>
		[HttpPut("withdraw")]
		public async Task Withdraw([FromBody] DtoTransactionRequest? request)
		{
			await walletBusiness.WithdrawAsync(request);
		}

		#endregion
	}
}
