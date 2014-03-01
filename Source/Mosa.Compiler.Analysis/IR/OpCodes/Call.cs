namespace Mosa.Compiler.Analysis.IR.OpCodes
{
	/// <summary>
	/// Intermediate representation of a call operation.
	/// </summary>
	public sealed class Call : IROpCode
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Call"/> class.
		/// </summary>
		public Call()
			: base(0, 0)
		{
		}

		/// <summary>
		/// Visits the specified visitor.
		/// </summary>
		/// <param name="visitor">The visitor.</param>
		/// <param name="ptr">The instruction pointer.</param>
		public override void Visit(IIRVisitor visitor, InstrPointer ptr)
		{
			visitor.Call(ptr);
		}
	}
}