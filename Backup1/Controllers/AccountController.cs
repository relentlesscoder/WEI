using System;
using System.Web.Mvc;
using System.Web.Security;
using WEI.Service.Interface;
using WEI.Web.Filters;
using WEI.Web.Models;

namespace WEI.Web.Controllers
{
    public class AccountController : Controller
    {
        #region Instance Variables

        private readonly IAccountService _accountService;

        #endregion

        #region Constructors

        // Constructor Injection
        public AccountController(IAccountService accountService)
        {
            if(accountService == null)
            {
                throw new ArgumentNullException("accountService");
            }

            this._accountService = accountService;
        }

        #endregion

        #region Action Methods
        //
        // GET: /Account/
        [ElmahHandleError]
        public ActionResult LogOn()
        {
            if(Request.IsAuthenticated)
            {
                return RedirectToAction("Home", "Account");
            }
            else
            {
                return View();  
            }
        }

        [ElmahHandleError]
        [HttpPost]
        public ActionResult LogOn(LogOnViewModel model, string returnUrl)
        {
            //validate the fields.
            if(ModelState.IsValid)
            {
                //validate the user login information.
                if(Membership.ValidateUser(model.UserName, model.Password))
                {
                    //create the authentication ticket.
                    FormsAuthentication.SetAuthCookie(model.UserName, false);
                    //redirect
                    if(Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Home", "Account");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "The username or password is incorrect.");
                }
            }

            return View(model);
        }

        [CustomAuthorize(Roles = "Admin")]
        public ActionResult Home()
        {
            return View("_CmsHome");
        }

        #endregion

    }
}
