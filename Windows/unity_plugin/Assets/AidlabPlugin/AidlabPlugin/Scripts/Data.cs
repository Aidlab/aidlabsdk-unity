using UnityEngine.Events;
using UnityEngine;
using System;
using System.Collections;

namespace Aidlab
{
    /// <summary>
    /// The `Data` class provides generic data structures with event-based delegates for handling data updates.
    /// </summary>
    public static class Data
    {
        /// <summary>
        /// The `DataDelegates` class is an abstract base class for data structures, providing event handling functionality.
        /// </summary>
        public abstract class DataDelegates
        {
            protected string name;


            /// <summary>
            /// Initializes a new instance of the `DataDelegates` class with a specified name.
            /// </summary>
            /// <param name="name">The name of the data structure.</param>
            public DataDelegates(string name)
            {
                this.name = name;
            }
            private UnityEvent onDataReceivedEvents = new UnityEvent();

            /// <summary>
            /// Subscribes a Unity action to be executed when data is received.
            /// </summary>
            /// <param name="action">The Unity action to be subscribed.</param>
            public void Subscribe(UnityAction action)
            {
                lock (onDataReceivedEvents)
                {
                    onDataReceivedEvents.AddListener(action);
                }
            }

            /// <summary>
            /// Unsubscribes a Unity action from receiving data updates.
            /// </summary>
            /// <param name="action">The Unity action to be unsubscribed.</param>
            public void Unsubscribe(UnityAction action)
            {
                lock (onDataReceivedEvents)
                {
                    onDataReceivedEvents.RemoveListener(action);
                }
            }

            /// <summary>
            /// Invokes the registered delegates on the main thread after data has been received.
            /// </summary>
            /// <remarks>
            /// This method is responsible for ensuring that data received events are processed on the main thread.
            /// It enqueues the execution of the events to the main thread worker queue.
            /// </remarks>
            protected void AfterDataReceived()
            {
                //MainThreadWorker.Instance.QueueOnMainThread(() =>
                //{

                //    lock (onDataReceivedEvents)
                //    {
                //        onDataReceivedEvents.Invoke();
                //    }
                //});

                MainThreadWorker.Instance.QueueOnMainThread(onDataReceivedEvents);
            }

        }



        /// <summary>
        /// The `Data1` class represents a single-value data structure with event-based delegates for data updates.
        /// </summary>
        /// <typeparam name="T">The type of data value.</typeparam>
        public class Data1<T> : DataDelegates
        {

            /// <summary>
            /// The current value of the data.
            /// </summary>
            public T value;

            /// <summary>
            /// The timestamp associated with the data.
            /// </summary>
            public System.UInt64 timestamp;

            public Data1(string name) : base(name) { }

            /// <summary>
            /// Updates the data value and timestamp, triggering event callbacks.
            /// </summary>
            /// <param name="value">The new data value.</param>
            /// <param name="timestamp">The timestamp associated with the data update.</param>
            public void ReceiveData(T value, System.UInt64 timestamp)
            {
                this.value = value;
                this.timestamp = timestamp;
                AfterDataReceived();
            }
        }

        /// <summary>
        /// The `Data3` class represents a three-component data structure with event-based delegates for data updates.
        /// </summary>
        /// <typeparam name="T">The type of data value.</typeparam>
        public class Data3<T> : DataDelegates
        {
            /// <summary>
            /// The x-component of the data.
            /// </summary>
            public T x;

            /// <summary>
            /// The y-component of the data.
            /// </summary>
            public T y;

            /// <summary>
            /// The z-component of the data.
            /// </summary>
            public T z;

            /// <summary>
            /// The timestamp associated with the data.
            /// </summary>
            public System.UInt64 timestamp;

            public Data3(string name) : base(name) { }

            /// <summary>
            /// Updates the three components of the data and timestamp, triggering event callbacks.
            /// </summary>
            /// <param name="x">The new x-component value.</param>
            /// <param name="y">The new y-component value.</param>
            /// <param name="z">The new z-component value.</param>
            /// <param name="timestamp">The timestamp associated with the data update.</param>
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
            /// <summary>
            /// The x-component of the data.
            /// </summary>
            public T x;

            /// <summary>
            /// The y-component of the data.
            /// </summary>
            public T y;

            /// <summary>
            /// The z-component of the data.
            /// </summary>
            public T z;

            /// <summary>
            /// The w-component of the data.
            /// </summary>
            public W w;

            /// <summary>
            /// The timestamp associated with the data.
            /// </summary>
            public System.UInt64 timestamp;

            public Data4(string name) : base(name) { }

            /// <summary>
            /// Updates the four components of the data and timestamp, triggering event callbacks.
            /// </summary>
            /// <param name="x">The new x-component value.</param>
            /// <param name="y">The new y-component value.</param>
            /// <param name="z">The new z-component value.</param>
            /// <param name="w">The new w-component value.</param>
            /// <param name="timestamp">The timestamp associated with the data update.</param>
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
