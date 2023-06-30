using NUnit.Framework;

namespace DJM.EventManager.Tests
{
    [TestFixture]
    public class EventManagerTests
    {
        private const int EventId01 = 0;
        private const int EventId02 = 1;

        [Test]
        public void EventManager_TriggerEvent_NoParameters()
        {
            var eventManager = new EventManager();
            var result = false;
            
            eventManager.AddObserver(EventId01, () => result = true);
            
            Assert.IsFalse(result);
            eventManager.TriggerEvent(EventId01);
            Assert.IsTrue(result);
        }
        
        [Test]
        public void EventManager_TriggerEvent_OneParameter()
        {
            var eventManager = new EventManager<int>();

            const int input = 5;
            var result = 0;
            void Handler(int i) => result = i;
            
            eventManager.AddObserver(EventId01, Handler);
            
            Assert.AreNotEqual(input, result);
            eventManager.TriggerEvent(EventId01, input);
            Assert.AreEqual(input, result);
        }
        
        [Test]
        public void EventManager_TriggerEvent_TwoParameter()
        {
            var eventManager = new EventManager<int, string>();
        
            const int input1 = 5;
            const string input2 = "test";
            var result1 = 0;
            var result2 = string.Empty;
            void Handler(int i, string s) { result1 = i; result2 = s; }
            
            eventManager.AddObserver(EventId01, Handler);
            
            Assert.AreNotEqual(input1, result1);
            Assert.AreNotEqual(input2, result2);
            
            eventManager.TriggerEvent(EventId01, input1, input2);
            
            Assert.AreEqual(input1, result1);
            Assert.AreEqual(input2, result2);
        }
        
        
        
        [Test]
        public void EventManager_TriggerCorrectEvent_NoParameters()
        {
            var eventManager = new EventManager();

            var result1 = false;
            var result2 = false;
            
            eventManager.AddObserver(EventId01, () => result1 = true);
            eventManager.AddObserver(EventId02, () => result2 = true);
            
            Assert.IsFalse(result1);
            Assert.IsFalse(result2);
            
            eventManager.TriggerEvent(EventId02);

            Assert.IsFalse(result1);
            Assert.IsTrue(result2);
        }
        
        [Test]
        public void EventManager_TriggerCorrectEvent_OneParameter()
        {
            var eventManager = new EventManager<int>();

            const int input = 5;
            var result1 = 0;
            var result2 = 0;
            void Handler1(int i) => result1 = i;
            void Handler2(int i) => result2 = i;
            
            eventManager.AddObserver(EventId01, Handler1);
            eventManager.AddObserver(EventId02, Handler2);
            
            Assert.AreNotEqual(input, result1);
            Assert.AreNotEqual(input, result2);
            
            eventManager.TriggerEvent(EventId01, input);

            Assert.AreEqual(input, result1);
            Assert.AreNotEqual(input, result2);
        }
        
        [Test]
        public void EventManager_TriggerCorrectEvent_TwoParameter()
        {
            var eventManager = new EventManager<int, string>();
        
            const int input1 = 5;
            const string input2 = "test";
            var resultA1 = 0;
            var resultA2 = string.Empty;
            var resultB1 = 0;
            var resultB2 = string.Empty;
            void HandlerA(int i, string s) { resultA1 = i; resultA2 = s; }
            void HandlerB(int i, string s) { resultB1 = i; resultB2 = s; }
            
            eventManager.AddObserver(EventId01, HandlerA);
            eventManager.AddObserver(EventId02, HandlerB);
            
            Assert.AreNotEqual(input1, resultA1);
            Assert.AreNotEqual(input2, resultA2);
            Assert.AreNotEqual(input1, resultB1);
            Assert.AreNotEqual(input2, resultB2);
            
            eventManager.TriggerEvent(EventId01, input1, input2);
            
            Assert.AreEqual(input1, resultA1);
            Assert.AreEqual(input2, resultA2);
            Assert.AreNotEqual(input1, resultB1);
            Assert.AreNotEqual(input2, resultB2);
        }



        [Test]
        public void EventManager_TriggerEventWithMultipleObservers_NoParameters()
        {
            var eventManager = new EventManager();

            var result1 = false;
            var result2 = false;
            
            eventManager.AddObserver(EventId01, () => result1 = true);
            eventManager.AddObserver(EventId01, () => result2 = true);
            
            Assert.IsFalse(result1);
            Assert.IsFalse(result2);
            
            eventManager.TriggerEvent(EventId01);

            Assert.IsTrue(result1);
            Assert.IsTrue(result2);
        }
        
        [Test]
        public void EventManager_TriggerEventWithMultipleObservers_OneParameter()
        {
            var eventManager = new EventManager<int>();

            const int input = 5;
            var result1 = 0;
            var result2 = 0;
            void Handler1(int b) => result1 = b;
            void Handler2(int b) => result2 = b;
            
            eventManager.AddObserver(EventId01, Handler1);
            eventManager.AddObserver(EventId01, Handler2);
            
            Assert.AreNotEqual(input, result1);
            Assert.AreNotEqual(input, result2);
            
            eventManager.TriggerEvent(EventId01, input);

            Assert.AreEqual(input, result1);
            Assert.AreEqual(input, result2);
        }
        
        [Test]
        public void EventManager_TriggerEventWithMultipleObservers_TwoParameter()
        {
            var eventManager = new EventManager<int, string>();
        
            const int input1 = 5;
            const string input2 = "test";
            var resultA1 = 0;
            var resultA2 = string.Empty;
            var resultB1 = 0;
            var resultB2 = string.Empty;
            
            void HandlerA(int i, string s) { resultA1 = i; resultA2 = s; }
            void HandlerB(int i, string s) { resultB1 = i; resultB2 = s; }
            
            eventManager.AddObserver(EventId01, HandlerA);
            eventManager.AddObserver(EventId01, HandlerB);
            
            Assert.AreNotEqual(input1, resultA1);
            Assert.AreNotEqual(input2, resultA2);
            Assert.AreNotEqual(input1, resultB1);
            Assert.AreNotEqual(input2, resultB2);
            
            eventManager.TriggerEvent(EventId01, input1, input2);
            
            Assert.AreEqual(input1, resultA1);
            Assert.AreEqual(input2, resultA2);
            Assert.AreEqual(input1, resultB1);
            Assert.AreEqual(input2, resultB2);
        }
        
        
        
        [Test]
        public void EventManager_ObserverNotTriggeredAfterRemoved_NoParameters()
        {
            var eventManager = new EventManager();
            
            var result = false;
            void Handler() => result = true;

            eventManager.AddObserver(EventId01, Handler);
            eventManager.RemoveObserver(EventId01, Handler);
            
            Assert.IsFalse(result);
            eventManager.TriggerEvent(EventId01);
            Assert.IsFalse(result);
        }
        
        [Test]
        public void EventManager_ObserverNotTriggeredAfterRemoved_OneParameter()
        {
            var eventManager = new EventManager<int>();

            const int input = 5;
            var result = 0;
            void Handler(int i) => result = i;

            eventManager.AddObserver(EventId01, Handler);
            eventManager.RemoveObserver(EventId01, Handler);

            Assert.AreNotEqual(input, result);
            
            eventManager.TriggerEvent(EventId01, input);

            Assert.AreNotEqual(input, result);
        }
        
        [Test]
        public void EventManager_ObserverNotTriggeredAfterRemoved_TwoParameter()
        {
            var eventManager = new EventManager<int, string>();
        
            const int input1 = 5;
            const string input2 = "test";
            var result1 = 0;
            var result2 = string.Empty;
        
            void HandlerA(int i, string s) { result1 = i; result2 = s; }
        
            eventManager.AddObserver(EventId01, HandlerA);
            eventManager.RemoveObserver(EventId01, HandlerA);
        
            Assert.AreNotEqual(input1, result1);
            Assert.AreNotEqual(input2, result2);
            
            eventManager.TriggerEvent(EventId01, input1, input2);
            
            Assert.AreNotEqual(input1, result1);
            Assert.AreNotEqual(input2, result2);
        }
    }
}


