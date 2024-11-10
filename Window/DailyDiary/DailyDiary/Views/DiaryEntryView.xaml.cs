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
using System.Windows.Shapes;

namespace DailyDiary.Views
{
    /// <summary>
    /// DiaryEntryView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class DiaryEntryView : Window
    {
        public DiaryEntryView()
        {
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string content = DiaryTextBox.Text;
            if(string.IsNullOrWhiteSpace(content))
            {
                MessageBox.Show("내용을 입력해주세요.", "알림");
                return;
            }
            // 저장 로직 구현
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("정말로 삭제하시겠습니까?", "삭제 확인", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            
           if(result == MessageBoxResult.Yes)
            {
                //삭제 로직 구현
                DiaryTextBox.Clear();
                MessageBox.Show("일기가 삭제되었습니다.", "알림");
            }
        }
    }
}
