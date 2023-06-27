using NUnit.Framework;
using System;

namespace DJM.EventManager.Tests
{
    [TestFixture]
    public class EventManagerTests
    {
        private const int EventId01 = 0;
        private const int EventId02 = 1;
        private const int EventId03 = 2;
        
        [Test]
        public void EventManager_TriggerEvent()
        {
            var eventManager = new EventManager<int>();

            var wasCalled = false;
            
            eventManager.AddObserver(EventId01, () => wasCalled = true);
            
            eventManager.TriggerEvent(EventId01);

            Assert.IsTrue(wasCalled);
        }
        
        [Test]
        public void EventManager_TriggerCorrectEvent()
        {
            var eventManager = new EventManager<int>();

            var wasCalled1 = false;
            var wasCalled2 = false;
            
            eventManager.AddObserver(EventId01, () => wasCalled1 = true);
            eventManager.AddObserver(EventId02, () => wasCalled2 = true);
            
            eventManager.TriggerEvent(EventId02);

            Assert.IsFalse(wasCalled1);
            Assert.IsTrue(wasCalled2);
        }
        
        [Test]
        public void EventManager_TriggerEventWithMultipleObservers()
        {
            var eventManager = new EventManager<int>();

            var wasCalled1 = false;
            var wasCalled2 = false;
            
            eventManager.AddObserver(EventId01, () => wasCalled1 = true);
            eventManager.AddObserver(EventId01, () => wasCalled2 = true);
            
            eventManager.TriggerEvent(EventId01);

            Assert.IsTrue(wasCalled1);
            Assert.IsTrue(wasCalled2);
        }
        
        [Test]
        public void EventManager_ObserverNotTriggeredAfterRemoved()
        {
            var eventManager = new EventManager<int>();

            var wasCalled1 = false;

            Action action = () => wasCalled1 = true;
            
            eventManager.AddObserver(EventId01, action);
            eventManager.RemoveObserver(EventId01, action);
            
            eventManager.TriggerEvent(EventId01);

            Assert.IsFalse(wasCalled1);
        }
    }
}


