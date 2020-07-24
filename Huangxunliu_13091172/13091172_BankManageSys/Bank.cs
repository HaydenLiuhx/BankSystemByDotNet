using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Threading;


namespace _13091172_BankManageSys
{
    public class Bank
    {

        #region Attribute
        private User[] _userGroup = new User[2];

        //private Account[] _accountGroup = new Account[3];

        public User[] UserGroup { get => _userGroup; set => _userGroup = value; }
        //public Account[] AccountGroup { get => _accountGroup; set => _accountGroup = value; }
        public List<Account> accountGroup = new List<Account>();

        #endregion



        /// <summary>
        /// initial;add in;make files;add new accounts
        /// </summary>
        #region Initial Account
        public void InitialAccount()
        {
            #region initial three accounts for easy tests
            Account account_100001 = new Account
            {
                AccountNo = 100001,
                AccountBalance = 1000,
                FirstName = "Liu",
                LastName = "HuangXun",
                Address = "7/9 Salisbury Rd, Kemsington, NSW, 2067",
                Phone = "0414601241",
                Email = "huangxunliu@gmail.com"
            };
            accountGroup.Add(account_100001);

            Account account_100002 = new Account
            {
                AccountNo = 100002,
                AccountBalance = 10000000000,
                FirstName = "Liu",
                LastName = "Hayden",
                Address = "52 Roland Avenue, Wahroonga, NSW, 2076",
                Phone = "0414601241",
                Email = "liuhuangxun@163.com"
            };
            accountGroup.Add(account_100002);

            Account account_100003 = new Account
            {
                AccountNo = 100003,
                AccountBalance = 1000,
                FirstName = "Liu",
                LastName = "H",
                Address = "52",
                Phone = "0414601241",
                Email = "huangxunliu@gmail.com"
            };
            #endregion

            #region add accounts in list and make txt files
            accountGroup.Add(account_100003);
            for (int i = 0; i < 3; i++)
            {
                string path = Convert.ToString(accountGroup[i].AccountNo);
                if (!File.Exists(path))
                {
                    string newText = "Account No:" + accountGroup[i].AccountNo + Environment.NewLine +
                                     "Account Balance:" + accountGroup[i].AccountBalance + Environment.NewLine +
                                     "First Name:" + accountGroup[i].FirstName + Environment.NewLine +
                                     "Last Name:" + accountGroup[i].LastName + Environment.NewLine +
                                     "Address:" + accountGroup[i].Address + Environment.NewLine +
                                     "Phone:" + accountGroup[i].Phone + Environment.NewLine +
                                     "Email:" + accountGroup[i].Email + Environment.NewLine;
                    File.WriteAllText(path, newText);
                }

            }
            #endregion

            #region if close application, new-add account will vanish,so i initial new-add accounts when this application reopen after close
            //find all-number txt
            DirectoryInfo di = new DirectoryInfo(@"/Users/liuhuangxun/Projects/13091172_BankManageSys/13091172_BankManageSys/bin/Debug/netcoreapp2.1");
            foreach (var fi in di.GetFiles())
            {
                //Console.WriteLine(fi.Name);
                if (int.TryParse(fi.Name, out int i))
                {
                    string path = @fi.Name;
                    string readText = File.ReadAllText(path);
                    //Console.WriteLine(readText);
                    using (StreamReader sr = new StreamReader(path))
                    {
                        List<String> allArray = new List<string>();
                        while (sr.Peek() >= 0)
                        {
                            string str;
                            string[] strArray;
                            str = sr.ReadLine();
                            strArray = str.Split(':');
                            allArray.Add(strArray[1]);
                        }
                        Account account = new Account();
                        account.AccountNo = Convert.ToInt32(allArray[0]);
                        account.AccountBalance = Convert.ToDouble(allArray[1]);
                        account.FirstName = allArray[2];
                        account.LastName = allArray[3];
                        account.Address = allArray[4];
                        account.Phone = allArray[5];
                        account.Email = allArray[6];
                        if (accountGroup.Contains(account) == false)
                        {
                            accountGroup.Add(account);
                        }
                    }
                }
            }
            #endregion





            //foreach (Account account in accountGroup)
            //{
            //    Console.WriteLine(account);
            //}

        }
        #endregion



        #region decoration
        public void P()
        {
            Console.WriteLine("##########################################################################");
        }
        #endregion



        /// <summary>
        /// eight-function-menu
        /// </summary>
        #region ShowMenu
        public void ShowCustomMenu()
        {
            do
            {
                Console.WriteLine("***********WELCOME************");
                Console.WriteLine("1.Create Account\n2.Search for a account\n3.Desposit\n4.Withdraw\n5.A/C statement\n6.Delete Account\n7.Exit\n8.Print All Accounts Info");
                Console.WriteLine("******************************");
                Console.WriteLine("Please Choose : ");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        Console.Clear();
                        CreateAccount();
                        break;
                    case "2":
                        Console.Clear();
                        SearchForAccount();
                        break;
                    case "3":
                        Console.Clear();
                        Desposit();
                        break;
                    case "4":
                        Console.Clear();
                        Withdraw();
                        break;
                    case "5":
                        Console.Clear();
                        AccountStatement();
                        break;
                    case "6":
                        Console.Clear();
                        DeleteAccount();
                        break;
                    case "7":
                        Console.Clear();
                        System.Environment.Exit(0);
                        break;
                    case "8":
                        Console.Clear();
                        PrintAllAccount();
                        break;
                    default:
                        Console.WriteLine("Not Valid");
                        Continue();
                        break;
                }
            }
            while (true);
        }
        #endregion



        #region print all accounts' info
        public void PrintAllAccount()
        {
            foreach (Account s in accountGroup)
            {
                Console.WriteLine(s);
                P();
                Thread.Sleep(300);
            }
        }
        #endregion



        /// <summary>
        /// create accounts and add accounts into list then write accounts'info into file
        /// </summary>
        #region Create account
        public void CreateAccount()
        {
            Account account = new Account();
            Console.WriteLine("CREATE A NEW ACCOUNT\nENTER DAETAILS");
            Console.Write("First Name : ");
            account.FirstName = Console.ReadLine();
            Console.Write("Last Name : ");
            account.LastName = Console.ReadLine();
            Console.Write("Address : ");
            account.Address = Console.ReadLine();
            Console.Write("Phone : ");
            try
            {
                account.SetPhone(Console.ReadLine());
            }
            catch (ArgumentOutOfRangeException a)
            {
                Console.Clear();
                Console.WriteLine(a.Message);
                Continue();
                return;
            }
            Console.Write("Email : ");
            account.Email = Console.ReadLine();
            Random random = new Random();
            int n = random.Next(100003, 9999999);
            account.AccountNo = n;
            account.AccountBalance = 0;
            Console.WriteLine("Information input finish...\nPlease wait a second...");
            Thread.Sleep(3000);
            Console.Clear();
            Console.WriteLine("Show your Inforamtion below...");
            Console.WriteLine("Account No:" + account.AccountNo +
                                     "\nAccount Balance:" + account.AccountBalance +
                                     "\nFirst Name:" + account.FirstName +
                                     "\nLast Name:" + account.LastName +
                                     "\nAddress:" + account.Address +
                                     "\nPhone:" + account.Phone +
                                     "\nEmail:" + account.Email);
            Console.WriteLine("Is the information correct (Y/N)?");


            string yesORno = Console.ReadKey().Key.ToString();

            switch (yesORno)
            {
                case "Y":
                    accountGroup.Add(account);
                    //WriteAllText +  Environment.NewLine +
                    string accountNumber = Convert.ToString(account.AccountNo);
                    string path = @accountNumber;
                    if (!File.Exists(path))
                    {
                        string newText = "Account No:" + account.AccountNo + Environment.NewLine +
                                         "Account Balance:" + account.AccountBalance + Environment.NewLine +
                                         "First Name:" + account.FirstName + Environment.NewLine +
                                         "Last Name:" + account.LastName + Environment.NewLine +
                                         "Address:" + account.Address + Environment.NewLine +
                                         "Phone:" + account.Phone + Environment.NewLine +
                                         "Email:" + account.Email + Environment.NewLine;
                        File.WriteAllText(path, newText);
                    }
                    Console.WriteLine("\nAccount Created!\nDetails will be provides via email.");
                    Console.WriteLine("Account number is " + account.AccountNo);
                    break;
                case "N":

                    break;
            }
            Continue();
        }
        #endregion



        #region SearchForAccount
        public void SearchForAccount()
        {
            Console.WriteLine("Please enter details \nAccount Number : ");
            string accountNumber = Console.ReadLine();
            string path = @accountNumber;
            try
            {
                if (File.Exists(path))
                {
                    Console.WriteLine("Account found");
                    string readText = File.ReadAllText(path);
                    Console.WriteLine(readText);
                }
                else
                {
                    Console.WriteLine("Account not found!");
                }
                Console.WriteLine("Check another account? (Y/N)");
                string yesORno = Console.ReadKey().Key.ToString();
                switch (yesORno)
                {

                    case "Y":
                        Console.Clear();
                        SearchForAccount();
                        break;
                    case "N":
                        Continue();
                        break;
                }
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }
        }
        #endregion


        /// <summary>
        /// desposit money into accounts by class Account's function
        /// </summary>
        #region Despoist
        public void Desposit()
        {

            Console.WriteLine("Please enter desposit details \nAccount Number : ");
            string accNo = Console.ReadLine();
            try
            {
                AccountNumberLengthCheck(accNo);
            }
            catch (ArgumentOutOfRangeException a)
            {
                Console.WriteLine(a.Message);
                Console.WriteLine(
                                  "\nRetry (Y/N)");
                string yesORno = Console.ReadKey(false).Key.ToString();

                switch (yesORno)
                {
                    case "Y":
                        Console.Clear();
                        Desposit();
                        break;
                    default:
                        Console.Clear();
                        return;

                }
            }
            Console.WriteLine("Also your amount please");
            int amount = Convert.ToInt32(Console.ReadLine());
            FindAccountNo myAccountNo = new FindAccountNo(Convert.ToInt32(accNo));
            bool accountExist = !accountGroup.Exists(x => x.AccountNo == Convert.ToInt32(accNo));
            if (accountExist == true)
            {
                Console.WriteLine("Sorry, Account can't find!" +
                                  "\nRetry (Y/N)");
                string yesORno = Console.ReadKey().Key.ToString();

                switch (yesORno)
                {
                    case "Y":
                        Desposit();
                        break;
                    case "N":
                        break;

                }
            }
            foreach (Account s in accountGroup.FindAll(new Predicate<Account>(myAccountNo.IsSame)))
            {

                if (s.AccountDesposit(amount) > 0)
                {
                    Console.WriteLine("Desposit successful");
                    Console.WriteLine("Current Balance : {0}", s.AccountBalance);
                    PathRewrite(s);
                }
                else Console.WriteLine("Your amount is wrong.");
            }
            Continue();
        }
        #endregion



        #region new class for mapping account number with write-down number by using List.FindAll
        public class FindAccountNo
        {
            private int _accountNo;
            public FindAccountNo(int No)
            { this._accountNo = No; }
            public bool IsSame(Account s)
            { return (s.AccountNo == _accountNo) ? true : false; }
        }
        #endregion


        /// <summary>
        /// update file because of despoit and withdraw
        /// </summary>
        /// <param name="account"></param>
        #region PathRewrite
        public void PathRewrite(Account account)
        {
            string accountNumber = Convert.ToString(account.AccountNo);
            string path = @accountNumber;

            string newText = "Account No:" + account.AccountNo + Environment.NewLine +
                                 "Account Balance:" + account.AccountBalance + Environment.NewLine +
                                 "First Name:" + account.FirstName + Environment.NewLine +
                                 "Last Name:" + account.LastName + Environment.NewLine +
                                 "Address:" + account.Address + Environment.NewLine +
                                 "Phone:" + account.Phone + Environment.NewLine +
                                 "Email:" + account.Email + Environment.NewLine;
            File.WriteAllText(path, newText);
        }
        #endregion



        #region print info
        public void InformationPrint(Account account)
        {
            string accountNumber = Convert.ToString(account.AccountNo);
            string path = @accountNumber;
            string readText = File.ReadAllText(path);
            Console.WriteLine(readText);
        }
        #endregion



        #region withdraw
        public void Withdraw()
        {
            Console.WriteLine("Please enter withdraw details \nAccount Number : ");
            string accNo = Console.ReadLine();
            try
            {
                AccountNumberLengthCheck(accNo);
            }
            catch (ArgumentOutOfRangeException a)
            {
                Console.WriteLine(a.Message);
                Console.WriteLine(
                                  "\nRetry (Y/N)");
                string yesORno = Console.ReadKey(false).Key.ToString();

                switch (yesORno)
                {
                    case "Y":
                        Console.Clear();
                        Withdraw();
                        break;
                    default:
                        Console.Clear();
                        return;


                }
            }
            Console.WriteLine("Also your amount please");
            double amount = Convert.ToDouble(Console.ReadLine());
            FindAccountNo myAccountNo = new FindAccountNo(Convert.ToInt32(accNo));
            bool accountExist = !accountGroup.Exists(x => x.AccountNo == Convert.ToInt32(accNo));
            if (accountExist == true)
            {
                Console.WriteLine("Sorry, Account can't find!" +
                                  "\nRetry (Y/N)");
                string yesORno = Console.ReadKey().Key.ToString();

                switch (yesORno)
                {
                    case "Y":
                        Withdraw();
                        break;
                    case "N":
                        break;
                }
            }
            foreach (Account s in accountGroup.FindAll(new Predicate<Account>(myAccountNo.IsSame)))
            {

                if (s.AccountWithdraw(amount) > 0)
                {
                    Console.WriteLine("Withdraw successful");
                    Console.WriteLine("Current Balance : {0}", s.AccountBalance);
                    PathRewrite(s);
                }
                else Console.WriteLine("Your amount is wrong.");
            }
            Continue();
        }
        #endregion


        /// <summary>
        /// include email send
        /// </summary>
        #region Account Statement
        public void AccountStatement()
        {
            Console.WriteLine("Please enter details \nAccount Number : ");
            string accNo = Console.ReadLine();
            try
            {
                AccountNumberLengthCheck(accNo);
            }
            catch (ArgumentOutOfRangeException a)
            {
                Console.WriteLine(a.Message);
                Console.WriteLine(
                                  "\nRetry (Y/N)");
                string yesORno = Console.ReadKey(false).Key.ToString();

                switch (yesORno)
                {
                    case "Y":
                        Console.Clear();
                        AccountStatement();
                        break;
                    default:
                        Console.Clear();
                        return;

                }
            }
            FindAccountNo myAccountNo = new FindAccountNo(Convert.ToInt32(accNo));
            try
            {
                bool accountExist = !accountGroup.Exists(x => x.AccountNo == Convert.ToInt32(accNo));
                if (accountExist == true)
                {
                    Console.WriteLine("Sorry, Account can't find!" +
                                      "\nRetry (Y/N)");
                    string yesORno = Console.ReadKey().Key.ToString();

                    switch (yesORno)
                    {
                        case "Y":
                            AccountStatement();
                            break;
                        case "N":

                            break;
                    }
                }
                foreach (Account s in accountGroup.FindAll(new Predicate<Account>(myAccountNo.IsSame)))
                {

                    Console.WriteLine("Account find\nWe will show account information below...");
                    InformationPrint(s);
                    Console.WriteLine(
                                 "\nEmail Statement (Y/N)");
                    string yesORno = Console.ReadKey(false).Key.ToString();

                    switch (yesORno)
                    {
                        case "Y":
                            Console.Clear();
                            sendEmail(s.Email, s);
                            break;
                        default:
                            Console.Clear();
                            return;

                    }
                }
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message + "\nMeans that this account has been deleted...");

            }
            Continue();
        }
        #endregion


        /// <summary>
        /// delete both in list and files
        /// </summary>
        #region Delete Account
        public void DeleteAccount()
        {
            Console.WriteLine("Please enter details \nAccount Number : ");
            string accNo = Console.ReadLine();
            try
            {
                AccountNumberLengthCheck(accNo);
            }
            catch (ArgumentOutOfRangeException a)
            {
                Console.WriteLine(a.Message);
                Console.WriteLine(
                                  "\nRetry (Y/N)");
                string yesORno = Console.ReadKey(false).Key.ToString();

                switch (yesORno)
                {
                    case "Y":
                        Console.Clear();
                        DeleteAccount();
                        break;
                    default:
                        Console.Clear();
                        return;

                }
            }
            FindAccountNo myAccountNo = new FindAccountNo(Convert.ToInt32(accNo));
            bool accountExist = !accountGroup.Exists(x => x.AccountNo == Convert.ToInt32(accNo));
            if (accountExist == true)
            {
                Console.WriteLine("Sorry, Account can't find!" +
                                  "\nRetry (Y/N)");
                string yesORno = Console.ReadKey().Key.ToString();

                switch (yesORno)
                {
                    case "Y":
                        DeleteAccount();
                        break;
                    case "N":

                        break;
                }
            }
            foreach (Account s in accountGroup.FindAll(new Predicate<Account>(myAccountNo.IsSame)))
            {

                try
                {
                    Console.WriteLine("Account find\nWe will show account information below...");
                    string accountNumber = Convert.ToString(s.AccountNo);
                    string path = @accountNumber;
                    string readText = File.ReadAllText(path);
                    Console.WriteLine(readText);
                    Console.WriteLine("Delete?(Y/N)");
                    string yesORno = Console.ReadKey().Key.ToString();

                    switch (yesORno)
                    {
                        case "Y":
                            try
                            {
                                if (File.Exists(path))
                                {
                                    File.Delete(path);
                                    Console.WriteLine("Delete Succesfully...");
                                    accountGroup.Remove(s);
                                }
                                else Console.WriteLine("Not find...");
                            }
                            catch (IOException ioExp)
                            {
                                Console.WriteLine(ioExp.Message);
                            }
                            break;
                        case "N":
                            break;
                        default:
                            Console.WriteLine("Not vaild");
                            continue;
                    }
                }
                catch (FileNotFoundException e)
                {
                    Console.WriteLine(e.Message);
                }

            }
            Continue();
        }
        #endregion


        /// <summary>
        /// Console.clear() and readkey()
        /// </summary>
        #region Continue
        public void Continue()
        {
            Console.WriteLine("Please press any key to continue");
            Console.ReadKey(false);
            Console.Clear();
        }
        #endregion


        /// <summary>
        /// check accountnumber if more than ten characters, throw execption
        /// </summary>
        /// <param name="account_number"></param>
        #region Account Number Length Check
        public void AccountNumberLengthCheck(string account_number)
        {
            if (account_number.Length > 10)
            {
                throw new ArgumentOutOfRangeException(nameof(account_number), "accout number length should no more than 10 characters...");
            }

        }
        #endregion


        /// <summary>
        /// i use google gmail, by smtp client
        /// this function can definitely run.
        /// that i initial 3 accounts in the beginning of program, only the second account whose account no is 100002 can receive email,becasue it is hard to find a email box that have smtp function and open smtp function easily.
        /// </summary>
        /// <param name="to">to other account's mail</param>
        /// <param name="account">class Account</param>
        #region send email
        public void sendEmail(string to, Account account)
        {
            try
            {
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress("huangxunliu@gmail.com", "Bank Notice");
                mailMessage.To.Add(to);
                mailMessage.Priority = MailPriority.Normal;
                string body = Convert.ToString(account);
                mailMessage.Body = body;
                mailMessage.Subject = "Bank System Auto Email";
                mailMessage.IsBodyHtml = true;
                SmtpClient MySmtp = new SmtpClient("smtp.gmail.com", 587);
                MySmtp.Credentials = new System.Net.NetworkCredential("huangxunliu", "aptx5964440");
                MySmtp.EnableSsl = true;
                MySmtp.Send(mailMessage);
                MySmtp = null;
                mailMessage.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Fail to Send：" + ex.Message);
            }



        }
        #endregion



    }
}
