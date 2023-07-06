
using NUnit.Framework;

namespace DJM.EventManager.Tests
{
    [TestFixture]
    public class EventManagerTests
    {
        private const int EventId01 = 0;

        [Test]
        public void TriggerEvent_NoParameter()
        {
            var eventManager = new EventManager<int>();
            
            var result = false;
            void Handler() => result = true;
            
            eventManager.AddObserver(EventId01, Handler);
            
            Assert.IsFalse(result);
            eventManager.TriggerEvent(EventId01);
            Assert.IsTrue(result);
        }
        
        [Test]
        public void TriggerEvent_OneParameter()
        {
            var eventManager = new EventManager<int>();

            const int input = int.MaxValue;
            var result = int.MinValue;
            void Handler(int i) => result = i;
            
            eventManager.AddObserver<int>(EventId01, Handler);
            
            Assert.AreNotEqual(input, result);
            eventManager.TriggerEvent<int>(EventId01, input);
            Assert.AreEqual(input, result);
        }
        
        [Test]
        public void TriggerEvent_TwoParameters()
        {
            var eventManager = new EventManager<int>();

            const int input1 = int.MaxValue;
            const string input2 = "input";
            
            var result1 = int.MinValue;
            var result2 = string.Empty;

            void Handler(int i, string s)
            {
                result1 = i;
                result2 = s;
            }
            
            eventManager.AddObserver<int, string>(EventId01, Handler);
            
            Assert.AreNotEqual(input1, result1);
            Assert.AreNotEqual(input2, result2);
            eventManager.TriggerEvent<int, string>(EventId01, input1, input2);
            Assert.AreEqual(input1, result1);
            Assert.AreEqual(input2, result2);
        }
        
        [Test]
        public void TriggerEvent_ThreeParameters()
        {
            var eventManager = new EventManager<int>();

            const int input1 = int.MaxValue;
            const string input2 = "input";
            object input3 = new object();
            
            var result1 = int.MinValue;
            var result2 = string.Empty;
            object result3 = null;

            void Handler(int i, string s, object o)
            {
                result1 = i;
                result2 = s;
                result3 = o;
            }
            
            eventManager.AddObserver<int, string, object>(EventId01, Handler);
            
            Assert.AreNotEqual(input1, result1);
            Assert.AreNotEqual(input2, result2);
            Assert.AreNotEqual(input3, result3);
            eventManager.TriggerEvent<int, string, object>(EventId01, input1, input2, input3);
            Assert.AreEqual(input1, result1);
            Assert.AreEqual(input2, result2);
            Assert.AreEqual(input3, result3);
        }
        
        [Test]
        public void TriggerEvent_FourParameters()
        {
            var eventManager = new EventManager<int>();

            const int input1 = int.MaxValue;
            const string input2 = "input";
            object input3 = new object();
            float[] input4 = { 0.1f, 0.2f, 0.3f };
            
            var result1 = int.MinValue;
            var result2 = string.Empty;
            object result3 = null;
            float[] result4 = new float[1];

            void Handler(int i, string s, object o, float[] f)
            {
                result1 = i;
                result2 = s;
                result3 = o;
                result4 = f;
            }
            
            eventManager.AddObserver<int, string, object, float[]>(EventId01, Handler);
            
            Assert.AreNotEqual(input1, result1);
            Assert.AreNotEqual(input2, result2);
            Assert.AreNotEqual(input3, result3);
            Assert.AreNotEqual(input4, result4);
            eventManager.TriggerEvent<int, string, object, float[]>(EventId01, input1, input2, input3, input4);
            Assert.AreEqual(input1, result1);
            Assert.AreEqual(input2, result2);
            Assert.AreEqual(input3, result3);
            Assert.AreEqual(input4, result4);
        }
    }
}


