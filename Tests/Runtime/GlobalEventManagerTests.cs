using NUnit.Framework;

namespace DJM.EventManager.Tests
{
    [TestFixture]
    public class GlobalEventManagerTests
    {
        private const int EventId01 = 0;
        private const int EventId02 = 1;
        
        
        [Test]
        public void TriggerEvent_OneParameter()
        {
            var eventManager = GlobalEventManager.Instance;

            const int input = 5;
            var result = 0;
            void Handler(int i) => result = i;
            
            eventManager.AddObserver<int>(EventId01, Handler);
            
            Assert.AreNotEqual(input, result);
            eventManager.TriggerEvent(EventId01, input);
            Assert.AreEqual(input, result);
        }
        
        [Test]
        public void TriggerEvent_NoParameter()
        {
            var eventManager = GlobalEventManager.Instance;
            
            var result = false;
            void Handler() => result = true;
            
            eventManager.AddObserver(EventId01, Handler);
            
            Assert.IsFalse(result);
            eventManager.TriggerEvent(EventId01);
            Assert.IsTrue(result);
        }
    }
}


