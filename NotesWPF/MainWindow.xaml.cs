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
using Microsoft.Win32;
using System.IO;
using static System.Net.Mime.MediaTypeNames;


namespace NotesWPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private string _openedFile;

        private bool _isFileSaved = true;

        private void SaveFile()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*"; // Фильтр файлов для диалога
            if (saveFileDialog.ShowDialog() == true)
            {
                string textToSave = new TextRange(RichTextBox.Document.ContentStart, RichTextBox.Document.ContentEnd).Text;
                string filePath = saveFileDialog.FileName;
                File.WriteAllText(filePath, textToSave);
                MessageBox.Show("Файл успешно сохранён");
            }
        }

        private void OpenFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*"; // фильтр
            
            if (openFileDialog.ShowDialog() == true)
            {
                _openedFile = openFileDialog.FileName;
                string fileText = File.ReadAllText(_openedFile);
                RichTextBox.Document.Blocks.Clear(); // Очистить содержимое RichTextBox
                RichTextBox.AppendText(fileText); 
            }
        }

        private void isFileSaved(object sender, TextChangedEventArgs e)
        {
            _isFileSaved = false;
        }
        

        private void Create()
        {
            if(!_isFileSaved)
            {
                MessageBoxResult result = MessageBox.Show("Файл не сохранен. Сохранить изменения?", "Предупреждение", MessageBoxButton.YesNoCancel);
                if (result == MessageBoxResult.Yes)
                {
                    SaveFile();
                    RichTextBox.Document.Blocks.Clear();
                    _isFileSaved = true;
                }
                else if (result == MessageBoxResult.Cancel)
                {

                }
            }
            else
            {
                RichTextBox.Document.Blocks.Clear();
                _isFileSaved = true;
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if(_openedFile == null)
            {
                SaveFile();
                _isFileSaved = true;
            }
            else
            {
                string textToSave = new TextRange(RichTextBox.Document.ContentStart, RichTextBox.Document.ContentEnd).Text;
                File.WriteAllText(_openedFile, textToSave);
                MessageBox.Show("Файл успешно сохранён");
                _isFileSaved = true;
            }           
        }

        private void OpenButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFile();
            _isFileSaved = true;
        }

        private void SaveAsButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFile();
            _isFileSaved = true;
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            Create();
            _isFileSaved = true;
        }
    }
}
