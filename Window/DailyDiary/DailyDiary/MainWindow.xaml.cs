using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DailyDiary
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Calendar_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            // 사용자가 클릭한 날짜가 선택된 상태인지 확인
            if (calendar.SelectedDate.HasValue)
            {
                DateTime selectedDate = calendar.SelectedDate.Value;

                // 선택한 날짜로 일기 작성 창 열기
                DiaryEntryWindow diaryEntryWindow = new DiaryEntryWindow(selectedDate);
                diaryEntryWindow.ShowDialog();
            }
        }
    }
}