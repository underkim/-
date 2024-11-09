using DailyDiary.Services;
using DailyDiary.Views;
using System.Configuration;
using System.Data;
using System.Web;
using System.Windows;

namespace DailyDiary
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            if (e.Args.Length > 0)
            {
                var arg = e.Args[0];
                if (arg.StartsWith("myapp://"))
                {
                    var uri = new Uri(arg);
                    var query = HttpUtility.ParseQueryString(uri.Query);
                    var token = query["token"];

                    if (!string.IsNullOrEmpty(token))
                    {
                        AuthenticationService.SaveSessionToken(token);
                        // 메인 화면으로 이동
                        var mainWindow = new CalendarView();
                        mainWindow.Show();
                        return;
                    }
                }
            }

            // 로그인 창 표시
            var loginWindow = new DiaryEntryView();
            loginWindow.Show();
        }
    }

}
