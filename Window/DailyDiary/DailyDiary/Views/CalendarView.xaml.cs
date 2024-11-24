using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DailyDiary.Views
{
    /// <summary>
    /// CalendarView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class CalendarView : Window
    {
        public CalendarView()
        {
            InitializeComponent();
        }

        public void CalendarDayButton_Click(object sender, RoutedEventArgs e)
        {
            
            if (sender is CalendarDayButton button && button.DataContext is DateTime clickedDate )
            {
                var diaryView = new DiaryEntryView(clickedDate);
                diaryView.Closed += (s, args) =>
                {
                    this.Show();
                };
                diaryView.Show();

                this.Hide();
            }
        
        }
    }
}
