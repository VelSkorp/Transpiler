namespace Transpiler
{
	public class Buffer
	{
		public uint A { get; set; }
		public uint B { get; set; }
		public uint C { get; set; }
		public uint D { get; set; }
		public uint[] T { get; set; }

		public void Copy(Buffer buffer)
		{
			A = buffer.A;
			B = buffer.B;
			C = buffer.C;
			D = buffer.D;
			T = buffer.T;
		}
	}
}