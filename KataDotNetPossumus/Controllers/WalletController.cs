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

		#endregion
	}
}
