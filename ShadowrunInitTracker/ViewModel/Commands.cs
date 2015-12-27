using ShadowrunInitTracker.Model;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using System.Xml.Serialization;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using ShadowrunInitTracker.View;

namespace ShadowrunInitTracker.ViewModel
{
    public static class Commands
    {
        private static readonly string combatExtension = "S.I.T. Combat (*.sitco)|*.sitco";

        //private static XmlSerializer combatSerializer = new XmlSerializer(typeof(CombatInstance));
        private static BinaryFormatter combatSerializer = new BinaryFormatter();
        public static ICommand StartCombatCommand = new RoutedCommand();
        public static ICommand SaveCombatCommand = new RoutedCommand();
        public static ICommand LoadCombatCommand = new RoutedCommand();
        
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
                            combatSerializer.Serialize(outStream, toSave);
                        }
                    }
                }
            }
            catch (Exception)
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
            catch (Exception)
            {
                MessageBox.Show("Failed to load combat.");
            }
            toLoadInto = new CombatInstance();
        }
    }
}
