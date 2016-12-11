using System.Threading.Tasks;

namespace xTFS.Services
{
	public interface IPopupService
	{
		Task DisplayAlert(string title, string msg);
		Task<string> DisplayOptions(string title, params string[] options);
	}
}