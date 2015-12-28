using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShadowrunInitTracker.Model;

namespace Tests
{
    [TestClass]
    public class PassOrderingTest
    {
        [TestMethod]
        public void SimpleTest()
        {
            Actor a = new Actor { Name = "A", InitiativeScore = 5 };
            Actor b = new Actor { Name = "B", InitiativeScore = 6 };
            InitiativePass pass = new InitiativePass();
            pass.Add(new ActorInitiativeEntry(a));
            pass.Add(new ActorInitiativeEntry(b));
            pass.Sort();

            Assert.AreEqual("B", (pass[0].Source as Actor).Name);
        }

        [TestMethod]
        public void CriticalGlitchTest()
        {
            Actor a = new Actor { Name = "A", InitiativeScore = 5, InitiativeGlitch = DiceRoller.SuccessType.CriticalGlitch };
            Actor b = new Actor { Name = "B", InitiativeScore = 5 };
            InitiativePass pass = new InitiativePass();
            pass.Add(new ActorInitiativeEntry(a));
            pass.Add(new ActorInitiativeEntry(b));
            pass.Sort();

            Assert.AreEqual("B", (pass[0].Source as Actor).Name);
        }

        [TestMethod]
        public void GlitchTest()
        {
            Actor a = new Actor { Name = "A", InitiativeScore = 5, InitiativeGlitch = DiceRoller.SuccessType.Glitch };
            Actor b = new Actor { Name = "B", InitiativeScore = 5 };
            InitiativePass pass = new InitiativePass();
            pass.Add(new ActorInitiativeEntry(a));
            pass.Add(new ActorInitiativeEntry(b));
            pass.Sort();

            Assert.AreEqual("B", (pass[0].Source as Actor).Name);
        }

        [TestMethod]
        public void TieBreakEdgeTest()
        {
            Actor a = new Actor { Name = "A", InitiativeScore = 5, Edge = 3 };
            Actor b = new Actor { Name = "B", InitiativeScore = 5, Edge = 4 };
            InitiativePass pass = new InitiativePass();
            pass.Add(new ActorInitiativeEntry(a));
            pass.Add(new ActorInitiativeEntry(b));
            pass.Sort();

            Assert.AreEqual("B", (pass[0].Source as Actor).Name);
        }

        [TestMethod]
        public void TieBreakInitiativeAttributeTest()
        {
            Actor a = new Actor { Name = "A", InitiativeScore = 5, TurnMode = CombatActorMode.Physical, PhysicalInitiativeAttribute = 3 };
            Actor b = new Actor { Name = "B", InitiativeScore = 5, TurnMode = CombatActorMode.Physical, PhysicalInitiativeAttribute = 4 };
            InitiativePass pass = new InitiativePass();
            pass.Add(new ActorInitiativeEntry(a));
            pass.Add(new ActorInitiativeEntry(b));
            pass.Sort();

            Assert.AreEqual("B", (pass[0].Source as Actor).Name);
        }

        [TestMethod]
        public void TieBreakReactionTest()
        {
            Actor a = new Actor { Name = "A", InitiativeScore = 5, Reaction = 3 };
            Actor b = new Actor { Name = "B", InitiativeScore = 5, Reaction = 4 };
            InitiativePass pass = new InitiativePass();
            pass.Add(new ActorInitiativeEntry(a));
            pass.Add(new ActorInitiativeEntry(b));
            pass.Sort();

            Assert.AreEqual("B", (pass[0].Source as Actor).Name);
        }
    }
}
