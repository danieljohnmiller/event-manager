using NUnit.Framework;

namespace DJM.EventManager.Tests
{
    [TestFixture]
    public class GlobalEventManagerTests
    {
        private const int EventId01 = 0;
        private const int EventId02 = 1;
        
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
        public void TriggerEvent_OneParameter_ValueType()
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
        public void TriggerEvent_OneParameter_ReferenceType()
        {
            var eventManager = GlobalEventManager.Instance;

            const string input = "5";
            var result = string.Empty;
            void Handler(string i) => result = i;
            
            eventManager.AddHandler<string>(EventId01, Handler);
            
            Assert.AreNotEqual(input, result);
            eventManager.TriggerEvent(EventId01, input);
            Assert.AreEqual(input, result);
        }

        [Test]
        public void HandlerNotTriggeredAfterRemoved_NoParameters()
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
        public void HandlerNotTriggeredAfterRemoved_OneParameters()
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
        
        [Test]
        public void TriggerEventWithMultipleObservers_NoParameters()
        {
            var eventManager = GlobalEventManager.Instance;

            var result1 = false;
            var result2 = false;
            var result3 = false;
            
            eventManager.AddHandler(EventId01, () => result1 = true);
            eventManager.AddHandler(EventId01, () => result2 = true);
            eventManager.AddHandler(EventId01, () => result3 = true);
            
            eventManager.TriggerEvent(EventId01);

            Assert.IsTrue(result1);
            Assert.IsTrue(result2);
            Assert.IsTrue(result3);
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
        public void ClearEvent_MixedParameters()
        {
            var eventManager = GlobalEventManager.Instance;

            const string input1 = "input";
            const int input2 = int.MaxValue;
            const float input3 = float.MaxValue;
            
            var result1 = string.Empty;
            var result2 = 0;
            var result3 = 0f;
            var result4 = false;
            
            eventManager.AddHandler<string>(EventId02, (string a) => result1 = a);
            eventManager.AddHandler<int>(EventId01, (int b) => result2 = b);
            eventManager.AddHandler<float>(EventId01, (float c) => result3 = c);
            eventManager.AddHandler(EventId01, () => result4 = true);

            eventManager.RemoveEvent(EventId01);
            
            eventManager.TriggerEvent<string>(EventId02, input1);
            eventManager.TriggerEvent<int>(EventId01, input2);
            eventManager.TriggerEvent<float>(EventId01, input3);
            eventManager.TriggerEvent(EventId01);

            Assert.AreEqual(input1, result1);
            Assert.AreNotEqual(input2, result2);
            Assert.AreNotEqual(input3, result3);
            Assert.IsFalse(result4);
        }
        
        [Test]
        public void ClearEventHandlerType_voidParameter()
        {
            var eventManager = GlobalEventManager.Instance;

            const string input1 = "input";

            var result1 = string.Empty;
            var result2 = false;
            
            eventManager.AddHandler<string>(EventId01, (string a) => result1 = a);
            eventManager.AddHandler(EventId01, () => result2 = true);
            
            eventManager.RemoveEventHandlerType(EventId01, null);
            
            eventManager.TriggerEvent<string>(EventId01, input1);
            eventManager.TriggerEvent(EventId01);

            Assert.AreEqual(input1, result1);
            Assert.IsFalse(result2);
        }
        
        [Test]
        public void ClearEventHandlerType_stringParameter()
        {
            var eventManager = GlobalEventManager.Instance;

            const string input1 = "input";

            var result1 = string.Empty;
            var result2 = false;
            
            eventManager.AddHandler<string>(EventId01, (string a) => result1 = a);
            eventManager.AddHandler(EventId01, () => result2 = true);
            
            eventManager.RemoveEventHandlerType(EventId01, typeof(string));
            
            eventManager.TriggerEvent<string>(EventId01, input1);
            eventManager.TriggerEvent(EventId01);

            Assert.AreNotEqual(input1, result1);
            Assert.IsTrue(result2);
        }
        
        [Test]
        public void ClearAllEvents()
        {
            var eventManager = GlobalEventManager.Instance;

            const string input1 = "input";
            const int input2 = int.MinValue;

            var result1 = string.Empty;
            var result2 = false;
            var result3 = int.MaxValue;
            var result4 = false;
            
            eventManager.AddHandler<string>(EventId01, (string a) => result1 = a);
            eventManager.AddHandler(EventId01, () => result2 = true);
            eventManager.AddHandler<int>(EventId02, (int a) => result3 = a);
            eventManager.AddHandler(EventId02, () => result4 = true);
            
            eventManager.RemoveAll();
            
            eventManager.TriggerEvent<string>(EventId01, input1);
            eventManager.TriggerEvent(EventId01);
            eventManager.TriggerEvent<int>(EventId02, input2);
            eventManager.TriggerEvent(EventId02);

            Assert.AreNotEqual(input1, result1);
            Assert.IsFalse(result2);
            Assert.AreNotEqual(input2, result3);
            Assert.IsFalse(result4);
        }
    }
}


