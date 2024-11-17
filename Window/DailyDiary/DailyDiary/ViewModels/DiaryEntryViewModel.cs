using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace DailyDiary.ViewModels
{
    class DiaryEntryViewModel : INotifyPropertyChanged
    {
        private string _diaryContent;
        private DateTime _selectedDate;

        public string DiaryContent
        {
            get => _diaryContent;
            set
            {
                _diaryContent = value;
                OnPropertyChanged(nameof(DiaryContent));
            }
        }

        public DateTime SelectedDate
        {
            get => _selectedDate;
            set
            {
                _selectedDate = value;
                OnPropertyChanged(nameof(SelectedDate));
            }
        }

        public ICommand SaveCommand { get; }
        public ICommand RemoveCommand { get; } 
        public DiaryEntryViewModel(DateTime selectedDate)
        {
            _selectedDate = selectedDate;

            SaveCommand = new RelayCommand(SaveDiary, CanSaveDiary);
            RemoveCommand = new RelayCommand(RemoveDiary);
        }
        private bool CanSaveDiary() => !string.IsNullOrWhiteSpace(DiaryContent);
        private void SaveDiary()
        {
            MessageBox.Show("일기가 저장되었습니다.", "알림");
        }
        private void RemoveDiary()
        {
            var result = MessageBox.Show("정말로 삭제하시겠습니까?", "삭제 확인", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                DiaryContent = string.Empty;
                MessageBox.Show("일기가 삭제되었습니다.", "알림");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

            if(propertyName == nameof(DiaryContent))
            {
                (SaveCommand as RelayCommand)?.NotifyCanExecuteChanged();
            }
        }
    }
}
