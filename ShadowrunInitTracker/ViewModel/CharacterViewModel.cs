using ShadowrunInitTracker.Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using System.Windows.Input;
using System.Xml.Serialization;

namespace ShadowrunInitTracker.ViewModel
{
    public class CharacterViewModel : INotifyPropertyChanged
    {

        public ObservableCollection<Character> Characters { get { return DataLibrary.Characters; } }
        Character selectedCharacter;
        public Character SelectedCharacter
        {
            get { return selectedCharacter; }
            set
            {
                selectedCharacter = value;
                NotifyPropertyChanged("SelectedCharacter");
            }
        }

        public void AddCharacter()
        {
            Character newCharacter = new Character { Name = "<unnamed character>" };
            Characters.Add(newCharacter);
            SelectedCharacter = newCharacter;
        }

        public void DeleteCharacter(Character toDelete)
        {
            Characters.Remove(toDelete);
        }

        public void ImportAllCharacters()
        {
            try
            {
                var dlg = new OpenFileDialog { Filter = DataIO.CharactersExtension };

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    using (var inStream = dlg.OpenFile())
                    {
                        if (inStream != null)
                        {
                            foreach (var rec in DataIO.LoadCharacters(inStream))
                                DataLibrary.Characters.Add(rec);
                        }
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to import characters.");
            }
        }

        public void ExportAllCharacters()
        {
            try
            {
                var dlg = new SaveFileDialog { Filter = DataIO.CharactersExtension };

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    using (var outStream = dlg.OpenFile())
                    {
                        if (outStream != null)
                        {
                            DataIO.SaveCharacters(outStream, DataLibrary.Characters);
                        }
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to export characters.");
            }
        }
        
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
