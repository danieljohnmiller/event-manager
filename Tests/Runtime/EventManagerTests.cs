using System;
using NUnit.Framework;

namespace DJM.EventManager.Tests
{
    [TestFixture]
    public class EventManagerTests
    {
        private const int EventId01 = 0;
        private const int EventId02 = 1;
        private const int EventId03 = 2;
        
        /*----------------------------- Add Observer -----------------------------*/
        
        // add observer to new event
        
        [Test]
        public void AddObserver_NewEvent0Params_IncrementsObserverCount()
        {
            // Arrange
            var eventManager = new EventManager<int>();
            void Handler() { }
            var numberOfObservers = eventManager.GetObserverCount(EventId01);
            
            // Act
            eventManager.AddObserver(EventId01, Handler);

            // Assert
            Assert.AreEqual(numberOfObservers + 1, eventManager.GetObserverCount(EventId01));
        }

        [Test]
        public void AddObserver_NewEvent1Params_IncrementsObserverCount()
        {
            // Arrange
            var eventManager = new EventManager<int>();
            void Handler(int i) { }
            var numberOfObservers = eventManager.GetObserverCount(EventId01);
            
            // Act
            eventManager.AddObserver<int>(EventId01, Handler);
            
            // Assert
            Assert.AreEqual(numberOfObservers+1, eventManager.GetObserverCount(EventId01));
        }
        
        [Test]
        public void AddObserver_NewEvent2Params_IncrementsObserverCount()
        {
            // Arrange
            var eventManager = new EventManager<int>();
            void Handler(int i, string s) { }
            var numberOfObservers = eventManager.GetObserverCount(EventId01);
            
            // Act
            eventManager.AddObserver<int, string>(EventId01, Handler);
            
            // Assert
            Assert.AreEqual(numberOfObservers+1, eventManager.GetObserverCount(EventId01));
        }
        
        [Test]
        public void AddObserver_NewEvent3Params_IncrementsObserverCount()
        {
            // Arrange
            var eventManager = new EventManager<int>();
            void Handler(int i, string s, object o) { }
            var numberOfObservers = eventManager.GetObserverCount(EventId01);
            
            // Act
            eventManager.AddObserver<int, string, object>(EventId01, Handler);

            // Assert
            Assert.AreEqual(numberOfObservers+1, eventManager.GetObserverCount(EventId01));
        }
        
        [Test]
        public void AddObserver_NewEvent4Params_IncrementsObserverCount()
        {
            // Arrange
            var eventManager = new EventManager<int>();
            void Handler(int i, string s, object o, float[] f) { }
            var numberOfObservers = eventManager.GetObserverCount(EventId01);
            
            // Act
            eventManager.AddObserver<int, string, object, float[]>(EventId01, Handler);

            // Assert
            Assert.AreEqual(numberOfObservers+1, eventManager.GetObserverCount(EventId01));
        }

        // add observer to existing event
        
        [Test]
        public void AddObserver_ExistingEvent0Params_IncrementsEventObserverCount()
        {
            // Arrange
            var eventManager = new EventManager<int>();
            void ExistingHandler() { }
            void Handler() { }
            eventManager.AddObserver(EventId01, ExistingHandler);
            var numberOfObservers = eventManager.GetObserverCount(EventId01);
            
            // Act
            eventManager.AddObserver(EventId01, Handler);

            // Assert
            Assert.AreEqual(numberOfObservers + 1, eventManager.GetObserverCount(EventId01));
        }
        
        [Test]
        public void AddObserver_ExistingEvent1Params_IncrementsEventObserverCount()
        {
            // Arrange
            var eventManager = new EventManager<int>();
            void ExistingHandler(int i) { }
            void Handler(int i) { }
            eventManager.AddObserver<int>(EventId01, ExistingHandler);
            var numberOfObservers = eventManager.GetObserverCount(EventId01);
            
            // Act
            eventManager.AddObserver<int>(EventId01, Handler);
            
            // Assert
            Assert.AreEqual(numberOfObservers+1, eventManager.GetObserverCount(EventId01));
        }
        
        [Test]
        public void AddObserver_ExistingEvent2Params_IncrementsEventObserverCount()
        {
            // Arrange
            var eventManager = new EventManager<int>();
            void ExistingHandler(int i, string s) { }
            void Handler(int i, string s) { }
            eventManager.AddObserver<int, string>(EventId01, ExistingHandler);
            var numberOfObservers = eventManager.GetObserverCount(EventId01);
            
            // Act
            eventManager.AddObserver<int, string>(EventId01, Handler);
            
            // Assert
            Assert.AreEqual(numberOfObservers+1, eventManager.GetObserverCount(EventId01));
        }
        
        [Test]
        public void AddObserver_ExistingEvent3Params_IncrementsEventObserverCount()
        {
            // Arrange
            var eventManager = new EventManager<int>();
            void ExistingHandler(int i, string s, object o) { }
            void Handler(int i, string s, object o) { }
            eventManager.AddObserver<int, string, object>(EventId01, ExistingHandler);
            var numberOfObservers = eventManager.GetObserverCount(EventId01);
            
            // Act
            eventManager.AddObserver<int, string, object>(EventId01, Handler);

            // Assert
            Assert.AreEqual(numberOfObservers+1, eventManager.GetObserverCount(EventId01));
        }
        
        [Test]
        public void AddObserver_ExistingEvent4Params_IncrementsEventObserverCount()
        {
            // Arrange
            var eventManager = new EventManager<int>();
            void ExistingHandler(int i, string s, object o, float[] f) { }
            void Handler(int i, string s, object o, float[] f) { }
            eventManager.AddObserver<int, string, object, float[]>(EventId01, ExistingHandler);
            var numberOfObservers = eventManager.GetObserverCount(EventId01);
            
            // Act
            eventManager.AddObserver<int, string, object, float[]>(EventId01, Handler);

            // Assert
            Assert.AreEqual(numberOfObservers+1, eventManager.GetObserverCount(EventId01));
        }
        
        // add observer to existing event, with incorrect param signature

        [Test]
        public void AddObserver_0ParamsEventIncorrectParamSignature_ThrowsArgumentException()
        {
            // Arrange
            var eventManager = new EventManager<int>();
            void ExistingHandler() { }
            eventManager.AddObserver(EventId01, ExistingHandler);
            
            void Handler(int i) { }

            // Act and Assert
            Assert.Throws<ArgumentException>(delegate { eventManager.AddObserver<int>(EventId01, Handler); });
        }
        
        [Test]
        public void AddObserver_1ParamsEventIncorrectParamSignature_ThrowsArgumentException()
        {
            // Arrange
            var eventManager = new EventManager<int>();
            void ExistingHandler(int i) { }
            eventManager.AddObserver<int>(EventId01, ExistingHandler);
            
            void Handler(int i, string s) { }
            
            // Act and Assert
            Assert.Throws<ArgumentException>(delegate { eventManager.AddObserver<int, string>(EventId01, Handler); });
        }
        
        [Test]
        public void AddObserver_2ParamsEventIncorrectParamSignature_ThrowsArgumentException()
        {
            // Arrange
            var eventManager = new EventManager<int>();
            void ExistingHandler(int i, string s) { }
            eventManager.AddObserver<int, string>(EventId01, ExistingHandler);
            
            void Handler() { }
            
            // Act and Assert
            Assert.Throws<ArgumentException>(delegate { eventManager.AddObserver(EventId01, Handler); });
        }
        
        [Test]
        public void AddObserver_3ParamsEventIncorrectParamSignature_ThrowsArgumentException()
        {
            // Arrange
            var eventManager = new EventManager<int>();
            void ExistingHandler(int i, string s, object o) { }
            eventManager.AddObserver<int, string, object>(EventId01, ExistingHandler);
            
            void Handler(int i) { }
            
            // Act and Assert
            Assert.Throws<ArgumentException>(delegate { eventManager.AddObserver<int>(EventId01, Handler); });
        }
        
        [Test]
        public void AddObserver_4ParamsEventIncorrectParamSignature_ThrowsArgumentException()
        {
            // Arrange
            var eventManager = new EventManager<int>();
            void ExistingHandler(int i, string s, object o, float[] f) { }
            eventManager.AddObserver<int, string, object, float[]>(EventId01, ExistingHandler);
            
            void Handler(object o, float[] f) { }
            
            // Act and Assert
            Assert.Throws<ArgumentException>(delegate { eventManager.AddObserver<object, float[]>(EventId01, Handler); });

        }

        /*----------------------------- Remove Observer -----------------------------*/
        
        [Test]
        public void RemoveObserver_0ParamsEvent_DecrementsObserverCount()
        {
            // Arrange
            var eventManager = new EventManager<int>();
            void Handler() { }
            eventManager.AddObserver(EventId01, Handler);
            var numberOfObservers = eventManager.GetObserverCount(EventId01);
            
            // Act
            eventManager.RemoveObserver(EventId01, Handler);

            // Assert
            Assert.AreEqual(numberOfObservers-1, eventManager.GetObserverCount(EventId01));
        }
        
        [Test]
        public void RemoveObserver_1ParamsEvent_DecrementsObserverCount()
        {
            // Arrange
            var eventManager = new EventManager<int>();
            void Handler(int i) { }
            eventManager.AddObserver<int>(EventId01, Handler);
            var numberOfObservers = eventManager.GetObserverCount(EventId01);
            
            // Act
            eventManager.RemoveObserver<int>(EventId01, Handler);

            // Assert
            Assert.AreEqual(numberOfObservers-1, eventManager.GetObserverCount(EventId01));
        }
        
        [Test]
        public void RemoveObserver_2ParamsEvent_DecrementsObserverCount()
        {
            // Arrange
            var eventManager = new EventManager<int>();
            void Handler(int i, string s) { }
            eventManager.AddObserver<int, string>(EventId01, Handler);
            var numberOfObservers = eventManager.GetObserverCount(EventId01);
            
            // Act
            eventManager.RemoveObserver<int, string>(EventId01, Handler);

            // Assert
            Assert.AreEqual(numberOfObservers-1, eventManager.GetObserverCount(EventId01));
        }
        
        [Test]
        public void RemoveObserver_3ParamsEvent_DecrementsObserverCount()
        {
            // Arrange
            var eventManager = new EventManager<int>();
            void Handler(int i, string s, object o) { }
            eventManager.AddObserver<int, string, object>(EventId01, Handler);
            var numberOfObservers = eventManager.GetObserverCount(EventId01);
            
            // Act
            eventManager.RemoveObserver<int, string, object>(EventId01, Handler);

            // Assert
            Assert.AreEqual(numberOfObservers-1, eventManager.GetObserverCount(EventId01));
        }
        
        [Test]
        public void RemoveObserver_4ParamsEvent_DecrementsObserverCount()
        {
            // Arrange
            var eventManager = new EventManager<int>();
            void Handler(int i, string s, object o, float[] f) { }
            eventManager.AddObserver<int, string, object, float[]>(EventId01, Handler);
            var numberOfObservers = eventManager.GetObserverCount(EventId01);
            
            // Act
            eventManager.RemoveObserver<int, string, object, float[]>(EventId01, Handler);

            // Assert
            Assert.AreEqual(numberOfObservers-1, eventManager.GetObserverCount(EventId01));
        }
        
        // attempt to remove observer, with incorrect param signature
        
        [Test]
        public void RemoveObserver_0ParamsEventIncorrectParamSignature_ThrowsArgumentException()
        {
            // Arrange
            var eventManager = new EventManager<int>();
            void ExistingHandler() { }
            eventManager.AddObserver(EventId01, ExistingHandler);
            
            void Handler(int i) { }

            // Act and Assert
            Assert.Throws<ArgumentException>(delegate { eventManager.RemoveObserver<int>(EventId01, Handler); });
        }
        
        [Test]
        public void RemoveObserver_1ParamsEventIncorrectParamSignature_ThrowsArgumentException()
        {
            // Arrange
            var eventManager = new EventManager<int>();
            void ExistingHandler(int i) { }
            eventManager.AddObserver<int>(EventId01, ExistingHandler);
            
            void Handler(int i, string s) { }

            // Act and Assert
            Assert.Throws<ArgumentException>(delegate { eventManager.RemoveObserver<int, string>(EventId01, Handler); });
        }
        
        [Test]
        public void RemoveObserver_2ParamsEventIncorrectParamSignature_ThrowsArgumentException()
        {
            // Arrange
            var eventManager = new EventManager<int>();
            void ExistingHandler(int i, string s) { }
            eventManager.AddObserver<int, string>(EventId01, ExistingHandler);
            
            void Handler() { }

            // Act and Assert
            Assert.Throws<ArgumentException>(delegate { eventManager.RemoveObserver(EventId01, Handler); });
        }
        
        [Test]
        public void RemoveObserver_3ParamsEventIncorrectParamSignature_ThrowsArgumentException()
        {
            // Arrange
            var eventManager = new EventManager<int>();
            void ExistingHandler(int i, string s, object o) { }
            eventManager.AddObserver<int, string, object>(EventId01, ExistingHandler);
            
            void Handler(int[] arr) { }

            // Act and Assert
            Assert.Throws<ArgumentException>(delegate { eventManager.RemoveObserver<int[]>(EventId01, Handler); });
        }
        
        [Test]
        public void RemoveObserver_4ParamsEventIncorrectParamSignature_ThrowsArgumentException()
        {
            // Arrange
            var eventManager = new EventManager<int>();
            void ExistingHandler(int i, string s, object o, float[] f) { }
            eventManager.AddObserver<int, string, object, float[]>(EventId01, ExistingHandler);
            
            void Handler(object o) { }

            // Act and Assert
            Assert.Throws<ArgumentException>(delegate { eventManager.RemoveObserver<object>(EventId01, Handler); });
        }
        
        /*----------------------------- Trigger Event -----------------------------*/
        
        // trigger event with one handler/observer
        
        [Test]
        public void TriggerEvent_0ParamEventOneObserver_HandlerInvoked()
        {
            // Arrange
            var eventManager = new EventManager<int>();
            var wasInvoked = false;
            void Handler() => wasInvoked = true;
            eventManager.AddObserver(EventId01, Handler);

            // Act
            eventManager.TriggerEvent(EventId01);
            
            // Assert
            Assert.IsTrue(wasInvoked);
        }
        
        [Test]
        public void TriggerEvent_1ParamEventOneObserver_HandlerInvoked()
        {
            // Arrange
            var eventManager = new EventManager<int>();
            var wasInvoked = false;
            void Handler(string s) => wasInvoked = true;
            eventManager.AddObserver<string>(EventId01, Handler);

            // Act
            eventManager.TriggerEvent<string>(EventId01, "test");
            
            // Assert
            Assert.IsTrue(wasInvoked);
        }
        
        [Test]
        public void TriggerEvent_2ParamEventOneObserver_HandlerInvoked()
        {
            // Arrange
            var eventManager = new EventManager<int>();
            var wasInvoked = false;
            void Handler(string s, float f) => wasInvoked = true;
            eventManager.AddObserver<string, float>(EventId01, Handler);

            // Act
            eventManager.TriggerEvent<string, float>(EventId01, "test", 0.5f);
            
            // Assert
            Assert.IsTrue(wasInvoked);
        }
        
        [Test]
        public void TriggerEvent_3ParamEventOneObserver_HandlerInvoked()
        {
            // Arrange
            var eventManager = new EventManager<int>();
            var wasInvoked = false;
            void Handler(string s, float f, object o) => wasInvoked = true;
            eventManager.AddObserver<string, float, object>(EventId01, Handler);

            // Act
            eventManager.TriggerEvent<string, float, object>(EventId01, "test", 0.5f, new object());
            
            // Assert
            Assert.IsTrue(wasInvoked);
        }
        
        [Test]
        public void TriggerEvent_4ParamEventOneObserver_HandlerInvoked()
        {
            // Arrange
            var eventManager = new EventManager<int>();
            var wasInvoked = false;
            void Handler(string s, float f, object o, int i) => wasInvoked = true;
            eventManager.AddObserver<string, float, object, int>(EventId01, Handler);

            // Act
            eventManager.TriggerEvent<string, float, object, int>(EventId01, "test", 0.5f, new object(), 0);
            
            // Assert
            Assert.IsTrue(wasInvoked);
        }
        
        
        // trigger event with two handlers/observers
        
        
        [Test]
        public void TriggerEvent_0ParamEventMultipleObservers_HandlersInvoked()
        {
            // Arrange
            var eventManager = new EventManager<int>();
            var handler1WasInvoked = false;
            var handler2WasInvoked = false;
            void Handler1() => handler1WasInvoked = true;
            void Handler2() => handler2WasInvoked = true;
            eventManager.AddObserver(EventId01, Handler1);
            eventManager.AddObserver(EventId01, Handler2);

            // Act
            eventManager.TriggerEvent(EventId01);
            
            // Assert
            Assert.IsTrue(handler1WasInvoked);
            Assert.IsTrue(handler2WasInvoked);
        }
        
        [Test]
        public void TriggerEvent_1ParamEventMultipleObservers_HandlersInvoked()
        {
            // Arrange
            var eventManager = new EventManager<int>();
            var handler1WasInvoked = false;
            var handler2WasInvoked = false;
            void Handler1(string s) => handler1WasInvoked = true;
            void Handler2(string s) => handler2WasInvoked = true;
            eventManager.AddObserver<string>(EventId01, Handler1);
            eventManager.AddObserver<string>(EventId01, Handler2);

            // Act
            eventManager.TriggerEvent<string>(EventId01, "test");
            
            // Assert
            Assert.IsTrue(handler1WasInvoked);
            Assert.IsTrue(handler2WasInvoked);
        }
        
        [Test]
        public void TriggerEvent_2ParamEventMultipleObservers_HandlersInvoked()
        {
            // Arrange
            var eventManager = new EventManager<int>();
            var handler1WasInvoked = false;
            var handler2WasInvoked = false;
            void Handler1(string s, float f) => handler1WasInvoked = true;
            void Handler2(string s, float f) => handler2WasInvoked = true;
            eventManager.AddObserver<string, float>(EventId01, Handler1);
            eventManager.AddObserver<string, float>(EventId01, Handler2);

            // Act
            eventManager.TriggerEvent<string, float>(EventId01, "test", 0.5f);
            
            // Assert
            Assert.IsTrue(handler1WasInvoked);
            Assert.IsTrue(handler2WasInvoked);
        }
        
        [Test]
        public void TriggerEvent_3ParamEventMultipleObservers_HandlersInvoked()
        {
            // Arrange
            var eventManager = new EventManager<int>();
            var handler1WasInvoked = false;
            var handler2WasInvoked = false;
            void Handler1(string s, float f, object o) => handler1WasInvoked = true;
            void Handler2(string s, float f, object o) => handler2WasInvoked = true;
            eventManager.AddObserver<string, float, object>(EventId01, Handler1);
            eventManager.AddObserver<string, float, object>(EventId01, Handler2);

            // Act
            eventManager.TriggerEvent<string, float, object>(EventId01, "test", 0.5f, new object());
            
            // Assert
            Assert.IsTrue(handler1WasInvoked);
            Assert.IsTrue(handler2WasInvoked);
        }
        
        [Test]
        public void TriggerEvent_4ParamEventMultipleObservers_HandlersInvoked()
        {
            // Arrange
            var eventManager = new EventManager<int>();
            var handler1WasInvoked = false;
            var handler2WasInvoked = false;
            void Handler1(string s, float f, object o, int i) => handler1WasInvoked = true;
            void Handler2(string s, float f, object o, int i) => handler2WasInvoked = true;
            eventManager.AddObserver<string, float, object, int>(EventId01, Handler1);
            eventManager.AddObserver<string, float, object, int>(EventId01, Handler2);

            // Act
            eventManager.TriggerEvent<string, float, object, int>(EventId01, "test", 0.5f, new object(), 0);
            
            // Assert
            Assert.IsTrue(handler1WasInvoked);
            Assert.IsTrue(handler2WasInvoked);
        }

        // trigger event with incorrect parameters

        [Test]
        public void TriggerEvent_0ParamEventIncorrectParamSignature_ThrowsArgumentException()
        {
            // Arrange
            var eventManager = new EventManager<int>();
            void Handler() { }
            eventManager.AddObserver(EventId01, Handler);
            
            // Act and Assert
            Assert.Throws<ArgumentException>(delegate { eventManager.TriggerEvent<int>(EventId01, 5); });
        }
        
        [Test]
        public void TriggerEvent_1ParamEventIncorrectParamSignature_ThrowsArgumentException()
        {
            // Arrange
            var eventManager = new EventManager<int>();
            void Handler(int i) { }
            eventManager.AddObserver<int>(EventId01, Handler);
            
            // Act and Assert
            Assert.Throws<ArgumentException>(delegate { eventManager.TriggerEvent<int, string>(EventId01, 1, "test"); });
        }
        
        [Test]
        public void TriggerEvent_2ParamEventIncorrectParamSignature_ThrowsArgumentException()
        {
            // Arrange
            var eventManager = new EventManager<int>();
            void Handler(int i, string s) { }
            eventManager.AddObserver<int, string>(EventId01, Handler);

            // Act and Assert
            Assert.Throws<ArgumentException>(delegate { eventManager.TriggerEvent(EventId01); });
        }
        
        [Test]
        public void TriggerEvent_3ParamEventIncorrectParamSignature_ThrowsArgumentException()
        {
            // Arrange
            var eventManager = new EventManager<int>();
            void Handler(int i, string s, object o) { }
            eventManager.AddObserver<int, string, object>(EventId01, Handler);

            // Act and Assert
            Assert.Throws<ArgumentException>(delegate { eventManager.TriggerEvent<int[]>(EventId01, new []{1,2,3}); });
        }
        
        [Test]
        public void TriggerEvent_4ParamEventIncorrectParamSignature_ThrowsArgumentException()
        {
            // Arrange
            var eventManager = new EventManager<int>();
            void Handler(int i, string s, object o, float[] f) { }
            eventManager.AddObserver<int, string, object, float[]>(EventId01, Handler);

            // Act and Assert
            Assert.Throws<ArgumentException>(delegate { eventManager.TriggerEvent<object>(EventId01, new object()); });
        }
        
        
        /*----------------------------- GetObserverCount -----------------------------*/
        
        [Test]
        public void GetObserverCount_EventWithThreeObservers_returnsNumberOfObservers()
        {
            // Arrange
            var eventManager = new EventManager<int>();
            void Handler1(int i, string s) { }
            void Handler2(int i, string s) { }
            void Handler3(int i, string s) { }
            eventManager.AddObserver<int, string>(EventId01, Handler1);
            eventManager.AddObserver<int, string>(EventId01, Handler2);
            eventManager.AddObserver<int, string>(EventId01, Handler3);
            
            // Act
            var numberOfObservers = eventManager.GetObserverCount(EventId01);
            
            // Assert
            Assert.AreEqual(3, numberOfObservers);
        }
        
        [Test]
        public void GetObserverCount_EventWithNoObservers_returnsNumberOfObservers()
        {
            // Arrange
            var eventManager = new EventManager<int>();

            // Act
            var numberOfObservers = eventManager.GetObserverCount(EventId01);
            
            // Assert
            Assert.AreEqual(0, numberOfObservers);
        }
        
        /*----------------------------- ClearObservers -----------------------------*/
        
        [Test]
        public void ClearObservers_EventWithThreeObservers_ClearAllObserversFromEvent()
        {
            // Arrange
            var eventManager = new EventManager<int>();
            void Handler1(int i, string s) { }
            void Handler2(int i, string s) { }
            void Handler3(int i, string s) { }
            eventManager.AddObserver<int, string>(EventId01, Handler1);
            eventManager.AddObserver<int, string>(EventId01, Handler2);
            eventManager.AddObserver<int, string>(EventId01, Handler3);

            // Act
            eventManager.ClearObservers(EventId01);
            
            // Assert
            Assert.AreEqual(0, eventManager.GetObserverCount(EventId01));
        }
        
        /*----------------------------- ClearAllObservers -----------------------------*/

        [Test]
        public void ClearAllObservers_MultipleEventsEachWithMultipleObservers_ClearAllObservers()
        {
            // Arrange
            var eventManager = new EventManager<int>();
            
            void Event1Handler1() { }
            void Event1Handler2() { }
            eventManager.AddObserver(EventId01, Event1Handler1);
            eventManager.AddObserver(EventId01, Event1Handler2);
            
            void Event2Handler1(int i, string s) { }
            void Event2Handler2(int i, string s) { }
            eventManager.AddObserver<int, string>(EventId02, Event2Handler1);
            eventManager.AddObserver<int, string>(EventId02, Event2Handler2);
            
            void Event3Handler1(float f, object o) { }
            void Event3Handler2(float f, object o) { }
            eventManager.AddObserver<float, object>(EventId03, Event3Handler1);
            eventManager.AddObserver<float, object>(EventId03, Event3Handler2);

            // Act
            eventManager.ClearAllObservers();
            
            // Assert
            Assert.AreEqual(0, eventManager.GetObserverCount(EventId01));
            Assert.AreEqual(0, eventManager.GetObserverCount(EventId02));
            Assert.AreEqual(0, eventManager.GetObserverCount(EventId03));
        }
        
    }
}