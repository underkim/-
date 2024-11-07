using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DailyDiary.Services;
using DailyDiary.Views;

namespace DailyDiary.ViewModels
{
    public class GoogleLoginViewModel : ObservableObject
    {
        private bool isBusy;
        private string statusMessage;
        private Window parentWindow;


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

        public GoogleLoginViewModel(Window parentWindow)
        {
            this.parentWindow = parentWindow;
            LoginCommand = new AsyncRelayCommand(InitiateLoginAsync);
        }

        private async Task InitiateLoginAsync()
        {
            IsBusy = true;
            StatusMessage = "로그인 중...";
            try
            {

                var listener = new HttpListener();
                string redirectUri = $"http://localhost:5000/api/google-login/callback/";
                listener.Prefixes.Add(redirectUri);
                listener.Start();
                Console.WriteLine("로컬 서버가 시작되었습니다...");

                var client = new HttpClient();
                

                var response = await client.GetAsync("http://172.20.115.153:8082/api/google-login");


                if (response.IsSuccessStatusCode)
                {
                    var authUrl = await response.Content.ReadAsStringAsync();
                    OpenBrowser(authUrl);

                    var context = await listener.GetContextAsync();
                    var token = context.Request.QueryString["code"]; 

                    if (!string.IsNullOrEmpty(token))
                    {
                       
                        AuthenticationService.SaveSessionToken(token);
                        StatusMessage = "로그인 성공";

                        
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            var calendarView = new CalendarView();
                            calendarView.Show();
                            parentWindow.Close();
                        });
                    }

                    var responseString = "<!DOCTYPE html><html><head><meta charset=\"UTF-8\"></head><body>인증이 완료되었습니다. 이 창을 닫으세요.</body></html>";
                    var buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
                    context.Response.ContentType = "text/html; charset=utf-8";
                    context.Response.ContentLength64 = buffer.Length;

                    await context.Response.OutputStream.WriteAsync(buffer, 0, buffer.Length);
                    context.Response.OutputStream.Close();

                    listener.Stop();
                    var data = new { accessToken = AuthenticationService.GetSessionToken() };
                    string json = JsonSerializer.Serialize(data);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var resp = await client.PostAsync("http://172.20.115.153:8082/api/google-login",content);
                    
                    if(response.IsSuccessStatusCode)
                    {
                        string responseData = await response.Content.ReadAsStringAsync();
                        Console.WriteLine("Response: " + responseData);
                    }
                    else
                    {
                        Console.WriteLine("Request failed: " + response.StatusCode);
                    }

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
