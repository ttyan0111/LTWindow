using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTWINDOW_
{
    internal class TaiKhoan
    {
        string _strUserName, _strPassword;

        public TaiKhoan(string strUserName, string strPassword)
        {
            _strUserName = strUserName;
            _strPassword = strPassword;
        }

        public string UserName { get { return _strUserName; } }
        public string Password { get { return _strPassword; } }
    }
}
