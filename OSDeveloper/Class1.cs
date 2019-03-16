#if DEBUG
using OSDeveloper.IO.Logging;
using TakymLib;

namespace OSDeveloper
{
	class Class1 : DisposableBase
	{
		private Logger _logger;

		public string MojiretsuField;

		public string MojiretsuProperty
		{
			get
			{
				_logger.Debug(MojiretsuField);
				return MojiretsuField;
			}

			set
			{
				_logger.Debug(value);
				MojiretsuField = value;
			}
		}

		public Class1(string msg)
		{
			_logger = Logger.Get(nameof(Class1));
			_logger.Notice(msg);
		}

		public int ABC(int x)
		{
			_logger.Info("ABC method called");
			return 2 * x + 3;
		}
	}
}
#endif
