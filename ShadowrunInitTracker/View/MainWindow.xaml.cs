using System;
using System.Windows;

namespace ShadowrunInitTracker.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            PromptLoadCharacters();
            
        }

        private void PromptLoadCharacters()
        {
            var result = MessageBox.Show("Load Characters?", "", MessageBoxButton.YesNo);
            switch(result)
            {
                case MessageBoxResult.Yes:
                    CharacterTab.ImportCharactersCmd.Execute(null, CharactersTab);
                    break;
            }
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Console.WriteLine("{0} x {1}", this.Width, this.Height);
        }
    }
}
