using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace ShadowrunInitTracker.Model
{
    public class DataIO
    {
        public static readonly string CharactersExtension = "S.I.T. Characters (*.sitch)|*.sitch";
        private static XmlSerializer characterSerializer = new XmlSerializer(typeof(CharacterCollection));

        public static CharacterCollection LoadCharacters(Stream input)
        {
            return characterSerializer.Deserialize(input) as CharacterCollection;
        }

        public static void SaveCharacters(Stream output, CharacterCollection collection)
        {

            characterSerializer.Serialize(new StreamWriter(output), collection);
        }

        public static readonly string CombatExtension = "S.I.T. Combat (*.sitco)|*.sitco";

        //private static XmlSerializer combatSerializer = new XmlSerializer(typeof(CombatInstance));
        private static BinaryFormatter combatSerializer = new BinaryFormatter();

        public static CombatInstance LoadCombat(Stream input)
        {
            return combatSerializer.Deserialize(input) as CombatInstance;
        }

        public static void SaveCombat(Stream output, CombatInstance combat)
        {

            combatSerializer.Serialize(output, combat);
        }
    }
}
