using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PustokMvcApp.Services;
using PustokMvcApp.Settings;

namespace PustokMvcApp.Controllers
{
    public class BasketController(BankService bankService,IOptions<GroupInfoSettings> groupInfoSettings) : Controller
    {
        
        public IActionResult Index()
        {
            bankService.Add();
            return Content($"Balance: {bankService.Balance}");
        }
        public IActionResult ShowInfo()
        {
            var groupInfo = groupInfoSettings.Value;
            return Content($"Name: {groupInfo.Name}, Surname: {groupInfo.Surname}");
        }
    }
}
