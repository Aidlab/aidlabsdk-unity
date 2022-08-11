using UnityEngine.Events;

namespace Aidlab
{
    public static class Data
    {
        public abstract class DataDelegates
        {
            protected string name;

            public DataDelegates(string name) { this.name = name; }
            private UnityEvent onDataReceivedEvents = new UnityEvent();
            public void Subscribe(UnityAction action) { onDataReceivedEvents.AddListener(action); }
            public void Unsubscribe(UnityAction action) { onDataReceivedEvents.RemoveListener(action); }
            protected void AfterDataReceived()
            {
                MainThreadWorker.ExecuteOnMainThread.Enqueue(onDataReceivedEvents);
            }
        }


        public class Data1<T> : DataDelegates
        {            
            public T value;
            public System.UInt64 timestamp;

            public Data1(string name) : base(name) { }

            public void ReceiveData(T value, System.UInt64 timestamp)
            {
                this.value = value;
                this.timestamp = timestamp;
                AfterDataReceived();
            }
        }

        public class Data3<T> : DataDelegates
        {
            public T x;
            public T y;
            public T z;
            public System.UInt64 timestamp;

            public Data3(string name) : base(name) { }

            public void ReceiveData(T x, T y, T z, System.UInt64 timestamp)
            {
                this.x = x;
                this.y = y;
                this.z = z;
                this.timestamp = timestamp;
                AfterDataReceived();
            }
        }

        public class Data4<T, W> : DataDelegates
        {
            public T x;
            public T y;
            public T z;
            public W w;
            public System.UInt64 timestamp;

            public Data4(string name) : base(name) { }

            public void ReceiveData(T x, T y, T z, W w, System.UInt64 timestamp)
            {
                this.x = x;
                this.y = y;
                this.z = z;
                this.w = w;
                this.timestamp = timestamp;
                AfterDataReceived();
            }
        }
    }
}
