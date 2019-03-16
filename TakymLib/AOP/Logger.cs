using System;
using System.IO;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Messaging;

namespace DotnetExlib.AOP
{
	public class Logger : IAspectBehavior
	{
		/// <summary>
		///  ログの出力先のライターです。
		/// </summary>
		public static TextWriter Out;

		private Type _type;
		public Logger(Type target)
		{
			_type = target;
		}

		public void PreInitializer(IConstructionCallMessage msg)
		{
			Out.WriteLine($"{_type.FullName}.{msg.MethodName} : Start to create object instance. . .");
			//Out.WriteLine($"{_type.FullName}.{msg.MethodName} : インスタンス生成開始 . . .");
		}

		public void PostInitializer(IConstructionCallMessage msg)
		{
			Out.WriteLine($"{_type.FullName}.{msg.MethodName} : 正常にインスタンスを生成しました。");
			//Out.WriteLine($"{_type.FullName}.{msg.MethodName} : 正常にインスタンスを生成しました。");
		}

		public void PreCallMethod(IMethodCallMessage msg)
		{
			Out.WriteLine($"{_type.FullName}.{msg.MethodName} : Start function");
			//Out.WriteLine($"{_type.FullName}.{msg.MethodName} : 実行開始");
		}

		public void PostCallMethod(IMethodCallMessage msg)
		{
			Out.WriteLine($"{_type.FullName}.{msg.MethodName} : End function");
			//Out.WriteLine($"{_type.FullName}.{msg.MethodName} : 実行終了");
		}
	}
}
