﻿using System;
using System.Web;
using System.Web.Mvc;
using Elmah;

namespace WEI.Web.Filters
{
    public class ElmahHandleErrorAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);

            Exception exception = filterContext.Exception;
            if(!filterContext.ExceptionHandled || RaiseErrorSignal(exception) || IsFiltered(filterContext))
            {
                return;
            }

            LogException(exception);
        }

        private static bool RaiseErrorSignal(Exception exception)
        {
            var context = HttpContext.Current;
            if (context == null)
                return false;
            var signal = ErrorSignal.FromContext(context);
            if (signal == null)
                return false;
            signal.Raise(exception, context);
            return true;
        }

        private static bool IsFiltered(ExceptionContext context)
        {
            var config = context.HttpContext.GetSection("elmah/errorFilter")
                         as ErrorFilterConfiguration;

            if (config == null)
                return false;

            var testContext = new ErrorFilterModule.AssertionHelperContext(
                                      context.Exception, HttpContext.Current);

            return config.Assertion.Test(testContext);
        }

        private static void LogException(Exception exception)
        {
            var context = HttpContext.Current;
            ErrorLog.GetDefault(context).Log(new Error(exception, context));
        }
    }
}