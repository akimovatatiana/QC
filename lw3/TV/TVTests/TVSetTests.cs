using Microsoft.VisualStudio.TestTools.UnitTesting;
using TV;

namespace TVTests
{
    [TestClass]
    public class TVSetTests
    {
        [TestMethod]
        public void TVSet_IsTurnedOffByDefault()
        {
            TVSet tv = new TVSet();
            Assert.IsFalse(tv.IsTurnedOn());
        }

        [TestMethod]
        public void TVSet_CantSelectChannelWhenTurnedOff()
        {
            TVSet tv = new TVSet();
            Assert.IsFalse(tv.SelectChannel(2));
        }

        [TestMethod]
        public void TVSet_DisplayZeroChannelByDefault()
        {
            TVSet tv = new TVSet();
            Assert.AreEqual(0, tv.GetChannel());
        }

        [TestMethod]
        public void TVSet_CanBeTurnedOn()
        {
            TVSet tv = new TVSet();
            tv.TurnOn();
            Assert.IsTrue(tv.IsTurnedOn());
        }

        [TestMethod]
        public void TVSet_CanBeTurnedOff()
        {
            TVSet tv = new TVSet();
            tv.TurnOn();
            tv.TurnOff();
            Assert.IsFalse(tv.IsTurnedOn());
        }

        [TestMethod]
        public void TVSet_CanSelectChannelFromOneToNinetyNine_WhenTurnedOn()
        {
            TVSet tv = new TVSet();
            tv.TurnOn();

            Assert.IsTrue(tv.SelectChannel(1));
            Assert.AreEqual(1, tv.GetChannel());

            Assert.IsTrue(tv.SelectChannel(99));
            Assert.AreEqual(99, tv.GetChannel());

            Assert.IsTrue(tv.SelectChannel(42));
            Assert.AreEqual(42, tv.GetChannel());

            Assert.IsFalse(tv.SelectChannel(0));
            Assert.AreEqual(42, tv.GetChannel());

            Assert.IsFalse(tv.SelectChannel(100));
            Assert.AreEqual(42, tv.GetChannel());
        }

        [TestMethod]
        public void TVSet_CanSelectPreviousChannel_WhenTurnedOn()
        {
            TVSet tv = new TVSet();
            tv.TurnOn();

            Assert.IsTrue(tv.SelectChannel(10));
            Assert.IsTrue(tv.SelectChannel(12));
            Assert.IsTrue(tv.SelectPreviousChannel());
            Assert.AreEqual(10, tv.GetChannel());
        }

        [TestMethod]
        public void TVSet_CantSelectPreviousChannel_WhenTurnedOff()
        {
            TVSet tv = new TVSet();
            Assert.IsFalse(tv.SelectPreviousChannel());
        }

        [TestMethod]
        public void TVSet_CantSetChannelName_WhenTurnedOff()
        {
            TVSet tv = new TVSet();
            Assert.IsFalse(tv.SetChannelName(1, "MTV"));
        }

        [TestMethod]
        public void TVSet_CanSetChannelName_WhenTurnedOn()
        {
            TVSet tv = new TVSet();
            tv.TurnOn();
            Assert.IsTrue(tv.SetChannelName(1, "MTV"));
        }

        [TestMethod]
        public void TVSet_CantSetEmptyStringChannelName_WhenTurnedOn()
        {
            TVSet tv = new TVSet();
            tv.TurnOn();
            Assert.IsFalse(tv.SetChannelName(1, ""));
        }
    }
}
