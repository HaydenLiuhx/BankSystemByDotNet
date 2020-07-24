using System;
namespace _13091172_BankManageSys
{
    public class User
    {
        #region Attribute
        private string _userName;

        
        public string Password { get => _password; set => _password = value; }
        public string UserName { get => _userName; set => _userName = value; }

        private string _password;
        #endregion

        public override string ToString()
        {
            return string.Format("Username : " + _userName +
                                 "Password : " + _password);
        }

    }
}


