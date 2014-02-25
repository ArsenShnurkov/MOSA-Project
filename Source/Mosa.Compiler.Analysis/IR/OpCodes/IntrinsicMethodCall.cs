namespace Mosa.Compiler.Analysis.IR.OpCodes
{
	/// <summary>
	/// Intermediate representation of the intrinsic call operation.
	/// </summary>
	public sealed class IntrinsicMethodCall : IROpCode
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="IntrinsicMethodCall"/> class.
		/// </summary>
		public IntrinsicMethodCall()
		{
		}

		/// <summary>
		/// Abstract visitor method for intermediate representation visitors.
		/// </summary>
		/// <param name="visitor">The visitor.</param>
		/// <param name="ptr">The instruction pointer.</param>
		public override void Visit(IIRVisitor visitor, InstrPointer ptr)
		{
			visitor.IntrinsicMethodCall(ptr);
		}
	}
}