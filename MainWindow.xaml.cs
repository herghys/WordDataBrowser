using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WordDataBrowser
{
    public partial class MainWindow : Window
    {
        private string _textToInput = string.Empty;
        private string fileName;
        private WordData data;
        private List<string> wordLists = new List<string>();

        public MainWindow()
        {
            InitializeComponent();
            NewData();
        }
        #region Save and Load
        private void NewData()
        {
            //Set Name and Path
            Utilities.fileName = string.Empty;
            Utilities.fullPath = string.Empty;
            _textToInput = string.Empty;
            //Set Combo Difficulty
            comboDifficulty.SelectedIndex = 0;
            //Clear Word Lists
            if (wordLists.Count > 0) wordLists.Clear();
            //New WordData
            data = new WordData();
            //Show Data
            ShowData();
        }

        void ShowData()
        {
            //Set Path File
            textPath.Text = string.IsNullOrEmpty(Utilities.fileName) ? "Select JSON File" : Utilities.fileName;
            //SetComboBox
            comboDifficulty.SelectedItem = data.difficulty;
            //Set Word Lists Text
            stringText.Text = _textToInput;
        }

        private void LoadData()
        {
            //Fetch Data
            data = DataJson.Fetch();
            wordLists = data.wordList;
            //Put to Text To Input
            if (wordLists != null) _textToInput = wordLists.Aggregate((x, y) => x + Utilities.delimiter + y);
            else _textToInput = string.Empty;

            ShowData();
        }
        private void SaveFile(bool isSavingAs = false)
        {
            wordLists = _textToInput.Split(Utilities.delimiter).ToList();

            data.difficulty = (Difficulties)comboDifficulty.SelectedIndex;
            data.difficultyString = comboDifficulty.SelectedItem.ToString();
            data.wordList = wordLists;
            DataJson.Write(data, isSavingAs, fileName:fileName);

            ShowData();
        }
        #endregion

        #region UI Event Handler
        private void ComboDifficulty_Changed(object sender, SelectionChangedEventArgs e)
        {
            fileName = comboDifficulty.SelectedItem.ToString();
        }
        private void OpenFile_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void SaveFile_Click(object sender, RoutedEventArgs e)
        {
            SaveFile(false);
        }

        private void SaveFileAs_Click(object sender, RoutedEventArgs e)
        {
            SaveFile(true);
        }
        private void NewFile_Click(object sender, RoutedEventArgs e)
        {
            NewData();
        }

        private void stringText_TextChanged(object sender, TextChangedEventArgs e)
        {
            _textToInput = stringText.Text;
        }
        #endregion

        #region Input Binding Events
        private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        private void NewFile_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            NewData();
        }
        private void SaveFile_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            SaveFile(false);
        }
        private void SaveNewFile_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            SaveFile(true);
        }
        private void OpenFile_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            LoadData();
        }
        #endregion
    }
}