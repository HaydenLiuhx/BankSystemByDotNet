using System;
using System.Net.Mail;

namespace _13091172_BankManageSys
{
    public class Account
    {
        #region Attributes
        private int _accountNo;
        private double _accountBalance;
        private string _firstName;
        private string _lastName;
        private string _address;
        private string _phone;
        private string _email;


        public int AccountNo { get => _accountNo; set => _accountNo = value; }
        public double AccountBalance { get => _accountBalance; set => _accountBalance = value; }
        public string FirstName { get => _firstName; set => _firstName = value; }
        public string LastName { get => _lastName; set => _lastName = value; }
        public string Address { get => _address; set => _address = value; }
        public string Phone { get => _phone; set => _phone = value; }
        public string Email { get => _email; set => _email = value; }

        #endregion




        #region Account Withdraw
        /// <summary>
        /// Withdraw
        /// </summary>
        /// <returns>Balance,if error,return -1</returns>
        public double AccountWithdraw(double money)
        {
            if (money > 0)
            {
                if (money <= _accountBalance)
                {
                    _accountBalance -= money;
                    return _accountBalance;
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                return -1;
            }
        }
        #endregion

        #region Account Desposit
        /// <summary> 
        /// Desposit
        /// </summary>
        /// <param name="money"></param>
        /// <returns>balance,if error,return-1</returns>
        public double AccountDesposit(double money)
        {
            if (money > 0)
            {
                _accountBalance += money;
                return _accountBalance;
            }
            else
            {
                return -1;
            }
        }
        #endregion

        public Account()
        {

        }


        /// <summary>
        /// no more than ten characters
        /// </summary>
        /// <param name="phone"></param>
        #region SetPhone
        public void SetPhone(string phone)
        {
            if (phone.Length > 10)
            {
                throw new ArgumentOutOfRangeException(nameof(phone), "phone length should no more than 10 characters...");
            }
            Phone = phone;
        }
        #endregion


        /// <summary>
        /// override for easily print
        /// </summary>
        /// <returns></returns>
        #region override Tostring
        public override string ToString()
        {
            return string.Format("Account No:" + _accountNo +
                                     "\nAccount Balance:" + _accountBalance +
                                     "\nFirst Name:" + _firstName +
                                     "\nLast Name:" + _lastName +
                                     "\nAddress:" + _address +
                                     "\nPhone:" + _phone +
                                     "\nEmail:" + _email);
        }
        #endregion



        #region for Tostring
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            Account objAsPart = obj as Account;
            if (objAsPart == null) return false;
            else return Equals(objAsPart);
        }
        public override int GetHashCode()
        {
            return AccountNo;
        }
        public bool Equals(Account other)
        {
            if (other == null) return false;
            return (this.AccountNo.Equals(other.AccountNo));
        }

        #endregion
    }
}
