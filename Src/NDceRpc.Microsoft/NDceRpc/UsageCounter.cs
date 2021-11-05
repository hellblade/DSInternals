using System;
using System.Threading;

namespace NDceRpc
{
	public class UsageCounter
	{
		private const int MaxCount = int.MaxValue;

		private const int Timeout = 120000;

		private readonly Mutex _lock;

		private readonly Semaphore _count;

		/// <summary> Creates a composite name with the format and arguments specified </summary>
		public UsageCounter(string nameFormat, params object[] arguments)
		{
			string name = string.Format(nameFormat, arguments);
			_lock = new Mutex(initiallyOwned: false, name + ".Lock");
			_count = new Semaphore(int.MaxValue, int.MaxValue, name + ".Count");
		}

		/// <summary> Delegate fired inside lock if this is the first Increment() call on the name provided </summary>
		public void Increment<T>(Action<T> beginUsage, T arg)
		{
			if (!_lock.WaitOne(120000, exitContext: false))
			{
				throw new TimeoutException();
			}
			try
			{
				if (!_count.WaitOne(120000, exitContext: false))
				{
					throw new TimeoutException();
				}
				if (!_count.WaitOne(120000, exitContext: false))
				{
					_count.Release();
					throw new TimeoutException();
				}
				int counter = 1 + _count.Release();
				if (beginUsage != null && counter == 2147483646)
				{
					beginUsage(arg);
				}
			}
			finally
			{
				_lock.ReleaseMutex();
			}
		}

		/// <summary> Delegate fired inside lock if the Decrement() count reaches zero </summary>
		public void Decrement(ThreadStart endUsage)
		{
			if (!_lock.WaitOne(120000, exitContext: false))
			{
				throw new TimeoutException();
			}
			try
			{
				int counter = 1 + _count.Release();
				if (endUsage != null && counter == int.MaxValue)
				{
					endUsage();
				}
			}
			finally
			{
				_lock.ReleaseMutex();
			}
		}
	}
}
