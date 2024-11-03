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
using System.IO;
using System.Linq;

namespace DailyDiary
{
    /// <summary>
    /// DiaryEntryWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class DiaryEntryWindow : Window
    {
        private DateTime selectedDate;
        public DiaryEntryWindow()
        {
            InitializeComponent();
        }

        public DiaryEntryWindow(DateTime date)
        {
            InitializeComponent();
            selectedDate = date;
            txtDate.Text = selectedDate.ToString("MMMM dd, yyyy"); // 선택한 날짜 표시
            LoadDiaryEntry(); // 일기 로드
        }

        private void LoadDiaryEntry()
        {
            string filePath = $"{selectedDate:yyyyMMdd}.txt";

            // 파일이 존재하면 내용을 불러옴
            if (File.Exists(filePath))
            {
                string[] diaryData = File.ReadAllLines(filePath);
                txtTitle.Text = diaryData[0];
                txtContent.Text = string.Join(Environment.NewLine, diaryData.Skip(1));
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string filePath = $"{selectedDate:yyyyMMdd}.txt";
            File.WriteAllLines(filePath, new[] { txtTitle.Text, txtContent.Text });
            MessageBox.Show("Diary entry saved.");
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            string filePath = $"{selectedDate:yyyyMMdd}.txt";

            if (File.Exists(filePath))
            {
                File.WriteAllLines(filePath, new[] { txtTitle.Text, txtContent.Text });
                MessageBox.Show("Diary entry updated.");
            }
            else
            {
                MessageBox.Show("No entry found to update.");
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            string filePath = $"{selectedDate:yyyyMMdd}.txt";

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                txtTitle.Clear();
                txtContent.Clear();
                MessageBox.Show("Diary entry deleted.");
            }
            else
            {
                MessageBox.Show("No entry found to delete.");
            }
        }
    }

}

