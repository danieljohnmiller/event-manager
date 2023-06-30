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
            
            eventManager.AddHandler<int>(EventId01, Handler);
            
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
            
            eventManager.AddHandler(EventId01, Handler);
            
            Assert.IsFalse(result);
            eventManager.TriggerEvent(EventId01);
            Assert.IsTrue(result);
        }
        
        [Test]
        public void TriggerEventWithMultipleObservers_OneParameters()
        {
            var eventManager = GlobalEventManager.Instance;

            const string input = "works";
            
            var result1 = string.Empty;
            var result2 = string.Empty;
            var result3 = string.Empty;
            
            eventManager.AddHandler<string>(EventId01, (string a) => result1 = a);
            eventManager.AddHandler<string>(EventId01, (string b) => result2 = b);
            eventManager.AddHandler<string>(EventId01, (string c) => result3 = c);
            
            eventManager.TriggerEvent(EventId01, input);

            Assert.AreEqual(input, result1);
            Assert.AreEqual(input, result2);
            Assert.AreEqual(input, result3);
        }
        
        
        [Test]
        public void ObserverNotTriggeredAfterRemoved_NoParameters()
        {
            var eventManager = GlobalEventManager.Instance;

            var result = false;
            void Handler() => result = true;

            eventManager.AddHandler(EventId01, Handler);
            eventManager.RemoveHandler(EventId01, Handler);
            
            Assert.IsFalse(result);
            eventManager.TriggerEvent(EventId01);
            Assert.IsFalse(result);
        }
        
        [Test]
        public void ObserverNotTriggeredAfterRemoved_OneParameters()
        {
            var eventManager = GlobalEventManager.Instance;

            const float input = 5f;
            
            var result = float.MinValue;
            void Handler(float num) => result = num;

            eventManager.AddHandler<float>(EventId01, Handler);
            eventManager.RemoveHandler<float>(EventId01, Handler);
            
            Assert.AreNotEqual(input,result);
            eventManager.TriggerEvent(EventId01, input);
            Assert.AreNotEqual(input, result);
        }
    }
}


