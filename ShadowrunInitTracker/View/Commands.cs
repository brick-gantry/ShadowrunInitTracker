using ShadowrunInitTracker.Model;
using ShadowrunInitTracker.ViewModel;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using System.Xml.Serialization;
using System.Windows.Forms;
using System.IO;

namespace ShadowrunInitTracker.View
{
    public class Commands
    {
        private static readonly string charactersExtension = "S.I.T. Characters (*.sitch)|*.sitch";
        private static readonly string combatExtension = "S.I.T. Combat (*.sitco)|*.sitco";

        private static XmlSerializer characterSerializer = new XmlSerializer(typeof(List<Character>));
        private static XmlSerializer combatSerializer = new XmlSerializer(typeof(CombatInstance));

        public static ICommand AddCharacterCommand = new RoutedCommand();
        public static ICommand EditCharacterCommand = new RoutedCommand();
        public static ICommand ViewCharactersCommand = new RoutedCommand();
        public static ICommand DeleteCharactersCommand = new RoutedCommand();
        public static ICommand AddEventCommand = new RoutedCommand();
        public static ICommand DeleteEventCommand = new RoutedCommand();
        public static ICommand ExportCharacterSetCommand = new RoutedCommand();
        public static ICommand ImportCharacterSetCommand = new RoutedCommand();
        public static ICommand StartCombatCommand = new RoutedCommand();
        public static ICommand SaveCombatCommand = new RoutedCommand();
        public static ICommand LoadCombatCommand = new RoutedCommand();
        
        public static void AddCharacter()
        {
            var vm = new CharacterEditViewModel();
            var window = new CharacterEditWindow(vm) { ShowInTaskbar = false };
            window.ShowDialog();
            if(window.DoSave)
            {
                vm.CopyToModel();
                DataLibrary.Instance.Characters.Add(vm.Model);
            }
        }

        public static void EditCharacter(Character toEdit)
        {
            var vm = new CharacterEditViewModel(toEdit);
            var window = new CharacterEditWindow(vm) { ShowInTaskbar = false };
            window.ShowDialog();
            if (window.DoSave)
            {
                vm.CopyToModel();
            }
        }

        public static void ViewCharacters()
        {
            var vm = new CharacterCollectionViewModel(DataLibrary.Instance.Characters);
            var window = new CharacterCollectionWindow(vm) { ShowInTaskbar = false };
            window.ShowDialog();
        }

        public static void DeleteCharacter(Character toDelete)
        {
            DataLibrary.Instance.Characters.Remove(toDelete);
        }

        public static void AddEvent(CombatInstance combat, CombatInstance.Time now)
        {
            var vm = new EventEditViewModel(now);
            var window = new EventEditWindow(vm);
            window.ShowInTaskbar = false;
            window.ShowDialog();
            if (window.DoSave)
            {
                vm.CopyToModel();
                combat.Events.Add(vm.Model);
            }
        }

        public static void DeleteEvent(CombatInstance combat, Event toDelete)
        {
            combat.Events.Remove(toDelete);
        }


        public static void ExportCharacterSet(List<Character> toSave)
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
            catch(Exception e)
            {
                MessageBox.Show("Failed to export characters.");
            }
        }

        public static void ImportCharacterSet(List<Character> toLoadInto)
        {
            try
            {
                var dlg = new SaveFileDialog { Filter = charactersExtension };

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    using (var inStream = dlg.OpenFile())
                    {
                        if (inStream != null)
                        {
                            toLoadInto.AddRange(characterSerializer.Deserialize(inStream) as List<Character>);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Failed to import characters.");
            }
}

        public static void StartCombat()
        {
            //todo implement
        }


        public static void SaveCombat(CombatInstance toSave)
        {
            try
            {
                var dlg = new SaveFileDialog { Filter = combatExtension };

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    using (var outStream = dlg.OpenFile())
                    {
                        if (outStream != null)
                        {
                            combatSerializer.Serialize(new StreamWriter(outStream), toSave);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Failed to save combat.");
            }
        }

        public static void LoadCombat(out CombatInstance toLoadInto)
        {
            try
            {
                var dlg = new SaveFileDialog { Filter = combatExtension };

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    using (var inStream = dlg.OpenFile())
                    {
                        if (inStream != null)
                        {
                            toLoadInto = combatSerializer.Deserialize(inStream) as CombatInstance;
                            return;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Failed to load combat.");
            }
            toLoadInto = new CombatInstance();
        }
    }
}
