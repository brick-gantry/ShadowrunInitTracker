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
        private static readonly string charactersExtension = "S.I.T. Characters (*.sitch)|*.sitch";
        private static XmlSerializer characterSerializer = new XmlSerializer(typeof(ObservableCollection<Character>));

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

        public void ImportCharacterSet(ObservableCollection<Character> toLoadInto)
        {
            try
            {
                var dlg = new OpenFileDialog { Filter = charactersExtension };

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    using (var inStream = dlg.OpenFile())
                    {
                        if (inStream != null)
                        {
                            foreach (var rec in characterSerializer.Deserialize(inStream) as ObservableCollection<Character>)
                                toLoadInto.Add(rec);
                            //LoadInto..AddRange(characterSerializer.Deserialize(inStream) as List<Character>);
                        }
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to import characters.");
            }
        }

        public void ExportCharacterSet(ObservableCollection<Character> toSave)
        {
            try
            {
                var dlg = new SaveFileDialog { Filter = charactersExtension };

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    using (var outStream = dlg.OpenFile())
                    {
                        if (outStream != null)
                        {
                            characterSerializer.Serialize(new StreamWriter(outStream), toSave);
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
