/*Viết một chương trình đơn giản minh họa
quản lý tài khoản ATM: khi rút tiền hoặc
chuyển tiền thì hệ thống sẽ gởi tự động tin
nhắn đến handphone của chủ tài khoản.
Hướng dẫn:
-Khi rút tiền hoặc chuyển tiền xong: phát
sinh sự kiện “đã rút tiền” hoặc “đã chuyển
tiền*/
using System.Text;

namespace OOP6
{
    public delegate void ATMDelegate(string message);
    public class ATM
    {
        public event ATMDelegate TransactionCompleted;
        public string accountname { get; set; }
        public string bankid { get; set; }

        public int Accountbalance { get; set; }
        public void Withdraw(int a)
        {
            if (Accountbalance > 0 && a <= Accountbalance)
            {
                Accountbalance -= a;
                if (TransactionCompleted != null)
                {
                    TransactionCompleted.Invoke($"Đã rút {a} từ tài khoản của {accountname}. Số dư còn lại: {Accountbalance}.");
                }
            }
            else
            {
                Console.WriteLine("Giao dịch thất bại. Số tiền không hợp lệ hoặc không đủ.");
            }
        }
        public void Transfer(int a, ATM user2)
        {
            if (Accountbalance > 0 && a <= Accountbalance)
            {
                Accountbalance -= a;
                user2.Accountbalance += a;
                if (TransactionCompleted != null)
                {
                    TransactionCompleted.Invoke($"Đã chuyển {a} từ tài khoản của {accountname} đến {user2.accountname} Số dư còn lại: {Accountbalance}.");
                    Console.WriteLine();
                    TransactionCompleted.Invoke($"Tài khoản của {user2.accountname} nhận được {a} từ {accountname}. Số dư hiện tại: {user2.Accountbalance}");
                }
            }
            else
            {
                Console.WriteLine("Giao dịch thất bại. Số tiền không hợp lệ hoặc không đủ.");
            }
        }
        public void Showbalance()
        {
            Console.WriteLine($"Số dư hiện có của {accountname}: {Accountbalance}");
        }
        public ATM()
        {
            accountname = "None";
            Accountbalance = 0;
            bankid = "None";
        }
        public ATM(string accountname_, string bankid_, int accountbalance_)
        {
            accountname = accountname_;
            Accountbalance = accountbalance_;
            bankid = bankid_;
        }
    }
    public class SMS
    {
        public void SendSMS(string message)
        {
            Console.WriteLine($"[SMS]: {message}");
        }
    }
    public class Program
    {
        
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            ATM bailu= new ATM("Bailu","01",3500000);
            ATM Dat = new ATM("Dat", "02", 2000000);

            SMS SMS_ = new SMS();

            bailu.TransactionCompleted += SMS_.SendSMS;
            Dat.TransactionCompleted += SMS_.SendSMS;

            bailu.Showbalance();
            Dat.Showbalance();
            Console.WriteLine();

            bailu.Withdraw(500000);
            Dat.Transfer(300000, bailu);
        }
    }
}


