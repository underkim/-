using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DailyDiary.Services;

namespace DailyDiary.ViewModels
{
    public class GoogleLoginViewModel : ObservableObject
    {
        private bool isBusy;
        private string statusMessage;

        public bool IsBusy
        {
            get => isBusy;
            set => SetProperty(ref isBusy, value);
        }

        public string StatusMessage
        {
            get => statusMessage;
            set => SetProperty(ref statusMessage, value);
        }

        public ICommand LoginCommand { get; }

        public GoogleLoginViewModel()
        {
            LoginCommand = new AsyncRelayCommand(InitiateLoginAsync);
        }

        private async Task InitiateLoginAsync()
        {
            IsBusy = true;
            StatusMessage = "로그인 중...";
            try
            {

                var listener = new HttpListener();
                string redirectUri = $"http://localhost:5000/";
                listener.Prefixes.Add(redirectUri);
                listener.Start();
                Console.WriteLine("로컬 서버가 시작되었습니다...");

                var client = new HttpClient();
                //var jsonContent = "{\"key\":\"value\"}"; // 여기에 실제 데이터를 JSON 형식으로 추가합니다.
                //var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await client.GetAsync("http://172.20.115.153:8082/api/google-login");


                if (response.IsSuccessStatusCode)
                {
                    var authUrl = await response.Content.ReadAsStringAsync();
                    OpenBrowser(authUrl);

                    var context = await listener.GetContextAsync();
                    var token = context.Request.QueryString["token"]; // 세션 토큰 수신

                    if (!string.IsNullOrEmpty(token))
                    {
                        // 5. 세션 토큰 저장
                        AuthenticationService.SaveSessionToken(token);
                        Console.WriteLine("세션 토큰이 저장되었습니다.");
                    }

                    var responseString = "<!DOCTYPE html><html><head><meta charset=\"UTF-8\"></head><body>인증이 완료되었습니다. 이 창을 닫으세요.</body></html>";
                    var buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
                    context.Response.ContentType = "text/html; charset=utf-8";
                    context.Response.ContentLength64 = buffer.Length;

                    await context.Response.OutputStream.WriteAsync(buffer, 0, buffer.Length);
                    context.Response.OutputStream.Close();


                    listener.Stop();

                    StatusMessage = "브라우저에서 로그인을 완료하세요.";

                }
                else
                {
                    StatusMessage = "로그인 URL을 가져오지 못했습니다.";
                }
            }
            catch (Exception ex)
            {
                StatusMessage = $"오류 발생: {ex.Message}";
            }
            finally
            {
                IsBusy = false;
            }
        }

        private void OpenBrowser(string url)
        {
            Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
        }
    }
}
