using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Web;

namespace AfricanMagicSystem.Configuration
{
    public class AppConfigurations : IConfiguration
    {
        List<string> _creditCardtype;

        public List<string> CreditCardType
        {
            get
            {
                if ((_creditCardtype != null))
                    return _creditCardtype;
                else
                {

                    NameValueCollection appSettings =
                       ConfigurationManager.AppSettings;

                    _creditCardtype = appSettings["AcceptedCreditCardTypes"].Split(',').ToList();
                    return _creditCardtype;
                }
            }
            set
            {
                _creditCardtype = value;
            }
        }
    }
}