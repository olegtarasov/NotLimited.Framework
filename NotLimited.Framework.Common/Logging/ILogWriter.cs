namespace NotLimited.Framework.Common.Logging
{
	/// <summary>
	/// Generic log writer interface
	/// </summary>
	public interface ILogWriter
	{
		/// <summary>
		/// Write a line to the log
		/// </summary>
		/// <param name="text">Text to write</param>
		void WriteLine(string text);

		/// <summary>
		/// Write a message to the log
		/// </summary>
		/// <param name="text">Text to write</param>
		void Write(string text);
	}
}