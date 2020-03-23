using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using SharedMemory;

namespace IPCInterface
{
    public static class IPC
    {
        public static readonly string MemoryName = "RimHelperProxy";
        public static readonly int MemorySize = 10 * 1024 * 1024; // 10Mb

        private static readonly int BufferSizePos = sizeof(int);
        private static readonly int BufferPos = sizeof(int) * 2;
        private static readonly int BufferSize = MemorySize - BufferPos;

        private static BufferReadWrite _sharedMemory;
        private static readonly BinaryFormatter BinaryFormatter = new BinaryFormatter();

        public static bool Active => _sharedMemory != null;

        #region MemoryAlloc

        public static void Create()
        {
            try
            {
                _sharedMemory = new BufferReadWrite(MemoryName, MemorySize);
                State = 0;
                StringBuf = "null";
            }
            catch (IOException e) // try connect
            {
                _sharedMemory = new BufferReadWrite(MemoryName);
            }
            catch (Exception e)
            {
                _sharedMemory = null;
                throw e;
            }
        }

        public static bool Connect()
        {
            try
            {
                _sharedMemory = new BufferReadWrite(MemoryName);
            }
            catch
            {
                _sharedMemory = null;
                return false;
            }

            return true;
        }

        public static void Close()
        {
            if (_sharedMemory != null)
            {
                _sharedMemory.Close();
                _sharedMemory = null;
            }
        }

        #endregion

        // state - must be int type or enum
        public static T StateCallback<T>(object state, Func<T> callback, object sendParam = null)
        {
            if (sendParam != null)
            {
                if (sendParam is string s)
                {
                    StringBuf = s;
                }
                else
                {
                    SetObjectBuf(sendParam);
                }
            }

            State = (int)state;
            while (State == (int)state)
                Thread.Sleep(100);
            return callback();
        }

        public static int State
        {
            get
            {
                _sharedMemory.Read(out int action, 0);
                return action;
            }
            set { _sharedMemory.Write(ref value, 0); }
        }

        public static string StringBuf
        {
            get
            {
                _sharedMemory.Read(out int bufSize, BufferSizePos);

                if (bufSize == 0)
                    return string.Empty;

                byte[] buf = new byte[bufSize];
                _sharedMemory.Read(buf, BufferPos);

                return Encoding.UTF8.GetString(buf);
            }
            set
            {
                if (String.IsNullOrEmpty(value))
                    value = "null";

                int bufSize = Encoding.UTF8.GetByteCount(value.ToCharArray());
                byte[] buf = new byte[bufSize];
                Encoding.UTF8.GetBytes(value, 0, value.Length, buf, 0);
                _sharedMemory.Write(ref bufSize, BufferSizePos);
                _sharedMemory.Write(buf, BufferPos);
            }
        }

        #region SerializedObject

        public static T GetObjectBuf<T>()
        {
            _sharedMemory.Read(out int bufSize, BufferSizePos);

            if (bufSize == 0)
                return default(T);

            var data = new byte[bufSize];
            _sharedMemory.Read(data, BufferPos);
            using (var memoryStream = new MemoryStream(data))
            {
                return (T) BinaryFormatter.Deserialize(memoryStream);
            }
        }

        public static void SetObjectBuf<T>(T obj)
        {
            byte[] data = Serialize(obj);
            int bufSize = data.Length;
            if (bufSize > BufferSize)
            {
                bufSize = 0;
                _sharedMemory.Write(ref bufSize, BufferSizePos);
                throw new Exception($"[IPC] SetObjectBuf size > bufSize: {bufSize / (1024 * 1024)}Mb(object: {typeof(T)})");
            }
            _sharedMemory.Write(ref bufSize, BufferSizePos);
            _sharedMemory.Write(data, BufferPos);
        }

        private static byte[] Serialize<T>(T obj)
        {
            try
            {
                using (var memoryStream = new MemoryStream())
                {
                    BinaryFormatter.Serialize(memoryStream, obj);
                    return memoryStream.ToArray();
                }
            }
            catch (Exception ex)
            {
                //throw new Exception($"[IPC] Serialize<{typeof(T)} error '{ex.Message}'");
                return null;
            }
        }

#endregion
    }
}