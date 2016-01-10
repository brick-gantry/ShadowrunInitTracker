using ShadowrunInitTracker.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;
using System.Windows.Data;
using System.Windows.Forms;
using System.Windows.Input;

namespace ShadowrunInitTracker.ViewModel
{
    public enum CombatMode { Setup, RollingInitiative, Combat }
    public class ModeVisibilityConverter : IValueConverter
    {
        public ModeVisibilityConverter() { }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var currentMode = (CombatMode)value;
            var targetMode = (CombatMode)parameter;
            return currentMode == targetMode ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class SelectableActor
    {
        public bool Selected { get; set; }
        public Actor Actor { get; set; }
    }
    
    public class CombatViewModel : INotifyPropertyChanged
    {
        public CharacterCollection Characters { get { return DataLibrary.Characters; } }
        public ObservableCollection<SelectableActor> SelectableActors { get; set; } = new ObservableCollection<SelectableActor>();
        public Actor SelectedActor { get { return Combat.AllActors.CurrentEntry.Source as Actor; } }

        public void RollInit(Actor actor, bool useEdge)
        {
            actor.RollInit(useEdge);
        }

        public CombatInstance Combat
        {
            get { return DataLibrary.Combat; }
            set { DataLibrary.Combat = value; }
        }

        public CombatViewModel()
        {
            BuildAvailableList();
            Characters.CollectionChanged += Characters_CollectionChanged;
        }

        ~CombatViewModel()
        {
            Characters.CollectionChanged -= Characters_CollectionChanged;
        }

        #region setup
        private void Characters_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            BuildAvailableList();
        }

        public void BuildAvailableList()
        {
            SelectableActors.Clear();
            foreach (var c in DataLibrary.Characters)
            {
                SelectableActors.Add(new SelectableActor {
                    Selected = true,//c.Type == CharacterType.PC,
                    Actor = new Actor(c) });
            }
        }
        #endregion

        #region combat
        public void StartCombat()
        {
            Combat.Reset();

            foreach (var a in SelectableActors)
            {
                if (a.Selected)
                    Combat.AddActor(a.Actor);
            }

            CurrentMode = CombatMode.RollingInitiative;
        }
        
        public void Next()
        {
            var result = Combat.Next();
            if(result == CombatInstance.NextResult.NoMoreActors)
            {
                CurrentMode = CombatMode.RollingInitiative;
            }
        }
        
        public void AcceptInitiativeRolls()
        {
            Combat.BuildInit();

            Next();

            CurrentMode = CombatMode.Combat;
        }
        
        public void EndCombat()
        {
            CurrentMode = CombatMode.Setup;
        }
        
        public void SaveCombat()
        {
            try
            {
                var dlg = new SaveFileDialog { Filter = DataIO.CombatExtension };

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    using (var outStream = dlg.OpenFile())
                    {
                        if (outStream != null)
                        {
                            DataIO.SaveCombat(outStream, Combat);
                        }
                    }
                }
            }
            catch (Exception)
            {
                System.Windows.Forms.MessageBox.Show("Failed to save combat.");
            }
        }
        
        public void LoadCombat()
        {
            try
            {
                var dlg = new SaveFileDialog { Filter = DataIO.CombatExtension };

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    using (var inStream = dlg.OpenFile())
                    {
                        if (inStream != null)
                        {
                            Combat = DataIO.LoadCombat(inStream);
                        }
                    }
                }
            }
            catch (Exception)
            {
                System.Windows.Forms.MessageBox.Show("Failed to load combat.");
            }
        }
        #endregion

        #region toggle
        CombatMode currentMode = CombatMode.Setup;
        public CombatMode CurrentMode
        {
            get { return currentMode; }
            set
            {
                currentMode = value;
                NotifyPropertyChanged("CurrentMode");
            }
        }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
