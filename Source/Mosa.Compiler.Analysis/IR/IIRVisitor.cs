/*
 * (c) 2008 MOSA - The Managed Operating System Alliance
 *
 * Licensed under the terms of the New BSD License.
 *
 * Authors:
 *  Michael Ruck (grover) <sharpos@michaelruck.de>
 */

namespace Mosa.Compiler.Analysis.IR
{
	/// <summary>
	/// Visitor interface of the intermediate representation.
	/// </summary>
	public interface IIRVisitor : IVisitor
	{
		/// <summary>
		/// Visitation function for AddressOf.
		/// </summary>
		/// <param name="ptr">The instruction pointer.</param>
		void AddressOf(InstrPointer ptr);

		/// <summary>
		/// Visitation function for ArithmeticShiftRight.
		/// </summary>
		/// <param name="ptr">The instruction pointer.</param>
		void ArithmeticShiftRight(InstrPointer ptr);

		/// <summary>
		/// Visitation function for Call.
		/// </summary>
		/// <param name="ptr">The instruction pointer.</param>
		void Call(InstrPointer ptr);

		/// <summary>
		/// Visitation function for Epilogue.
		/// </summary>
		/// <param name="ptr">The instruction pointer.</param>
		void Epilogue(InstrPointer ptr);

		/// <summary>
		/// Visitation function for FloatingPointCompare.
		/// </summary>
		/// <param name="ptr">The instruction pointer.</param>
		void FloatCompare(InstrPointer ptr);

		/// <summary>
		/// Visitation function for FloatingPointToIntegerConversion.
		/// </summary>
		/// <param name="ptr">The instruction pointer.</param>
		void FloatToIntegerConversion(InstrPointer ptr);

		/// <summary>
		/// Visitation function for IntegerCompare.
		/// </summary>
		/// <param name="ptr">The instruction pointer.</param>
		void IntegerCompare(InstrPointer ptr);

		/// <summary>
		/// Visitation function for IntegerToFloatingPointConversion.
		/// </summary>
		/// <param name="ptr">The instruction pointer.</param>
		void IntegerToFloatConversion(InstrPointer ptr);

		/// <summary>
		/// Visitation function for Branch .
		/// </summary>
		/// <param name="ptr">The instruction pointer.</param>
		void Branch(InstrPointer ptr);

		/// <summary>
		/// Visitation function for Load.
		/// </summary>
		/// <param name="ptr">The instruction pointer.</param>
		void Load(InstrPointer ptr);

		/// <summary>
		/// Visitation function for LoadZeroExtended.
		/// </summary>
		/// <param name="ptr">The instruction pointer.</param>
		void LoadZeroExtended(InstrPointer ptr);

		/// <summary>
		/// Visitation function for LoadSignExtended.
		/// </summary>
		/// <param name="ptr">The instruction pointer.</param>
		void LoadSignExtended(InstrPointer ptr);

		/// <summary>
		/// Visitation function for LogicalAnd.
		/// </summary>
		/// <param name="ptr">The instruction pointer.</param>
		void LogicalAnd(InstrPointer ptr);

		/// <summary>
		/// Visitation function for LogicalOr.
		/// </summary>
		/// <param name="ptr">The instruction pointer.</param>
		void LogicalOr(InstrPointer ptr);

		/// <summary>
		/// Visitation function for LogicalXor.
		/// </summary>
		/// <param name="ptr">The instruction pointer.</param>
		void LogicalXor(InstrPointer ptr);

		/// <summary>
		/// Visitation function for LogicalNot.
		/// </summary>
		/// <param name="ptr">The instruction pointer.</param>
		void LogicalNot(InstrPointer ptr);

		/// <summary>
		/// Visitation function for Move.
		/// </summary>
		/// <param name="ptr">The instruction pointer.</param>
		void Move(InstrPointer ptr);

		/// <summary>
		/// Visitation function for Phi.
		/// </summary>
		/// <param name="ptr">The instruction pointer.</param>
		void Phi(InstrPointer ptr);

		/// <summary>
		/// Visitation function for Prologue.
		/// </summary>
		/// <param name="ptr">The instruction pointer.</param>
		void Prologue(InstrPointer ptr);

		/// <summary>
		/// Visitation function for Return.
		/// </summary>
		/// <param name="ptr">The instruction pointer.</param>
		void Return(InstrPointer ptr);

		/// <summary>
		/// Visitation function for EHEpilogue.
		/// </summary>
		/// <param name="ptr">The instruction pointer.</param>
		void EHEpilogue(InstrPointer ptr);

		/// <summary>
		/// Visitation function for ShiftLeft.
		/// </summary>
		/// <param name="ptr">The instruction pointer.</param>
		void ShiftLeft(InstrPointer ptr);

		/// <summary>
		/// Visitation function for ShiftRight.
		/// </summary>
		/// <param name="ptr">The instruction pointer.</param>
		void ShiftRight(InstrPointer ptr);

		/// <summary>
		/// Visitation function for MoveSignExtended.
		/// </summary>
		/// <param name="ptr">The instruction pointer.</param>
		void MoveSignExtended(InstrPointer ptr);

		/// <summary>
		/// Visitation function for Store.
		/// </summary>
		/// <param name="ptr">The instruction pointer.</param>
		void Store(InstrPointer ptr);

		/// <summary>
		/// Visitation function for MoveZeroExtended.
		/// </summary>
		/// <param name="ptr">The instruction pointer.</param>
		void MoveZeroExtended(InstrPointer ptr);

		/// <summary>
		/// Visitation function for Nop.
		/// </summary>
		/// <param name="ptr">The instruction pointer.</param>
		void Nop(InstrPointer ptr);

		/// <summary>
		/// Visitation function for AddSigned.
		/// </summary>
		/// <param name="ptr">The instruction pointer.</param>
		void AddSigned(InstrPointer ptr);

		/// <summary>
		/// Visitation function for AddUnsigned.
		/// </summary>
		/// <param name="ptr">The instruction pointer.</param>
		void AddUnsigned(InstrPointer ptr);

		/// <summary>
		/// Visitation function for AddFloat.
		/// </summary>
		/// <param name="ptr">The instruction pointer.</param>
		void AddFloat(InstrPointer ptr);

		/// <summary>
		/// Visitation function for DivFloat.
		/// </summary>
		/// <param name="ptr">The instruction pointer.</param>
		void DivFloat(InstrPointer ptr);

		/// <summary>
		/// Visitation function for DivSigned.
		/// </summary>
		/// <param name="ptr">The instruction pointer.</param>
		void DivSigned(InstrPointer ptr);

		/// <summary>
		/// Visitation function for DivUnsigned.
		/// </summary>
		/// <param name="ptr">The instruction pointer.</param>
		void DivUnsigned(InstrPointer ptr);

		/// <summary>
		/// Visitation function for MulSigned.
		/// </summary>
		/// <param name="ptr">The instruction pointer.</param>
		void MulSigned(InstrPointer ptr);

		/// <summary>
		/// Visitation function for MulFloat.
		/// </summary>
		/// <param name="ptr">The instruction pointer.</param>
		void MulFloat(InstrPointer ptr);

		/// <summary>
		/// Visitation function for MulUnsigned.
		/// </summary>
		/// <param name="ptr">The instruction pointer.</param>
		void MulUnsigned(InstrPointer ptr);

		/// <summary>
		/// Visitation function for SubFloat.
		/// </summary>
		/// <param name="ptr">The instruction pointer.</param>
		void SubFloat(InstrPointer ptr);

		/// <summary>
		/// Visitation function for SubSigned.
		/// </summary>
		/// <param name="ptr">The instruction pointer.</param>
		void SubSigned(InstrPointer ptr);

		/// <summary>
		/// Visitation function for SubUnsigned.
		/// </summary>
		/// <param name="ptr">The instruction pointer.</param>
		void SubUnsigned(InstrPointer ptr);

		/// <summary>
		/// Visitation function for RemFloat.
		/// </summary>
		/// <param name="ptr">The instruction pointer.</param>
		void RemFloat(InstrPointer ptr);

		/// <summary>
		/// Visitation function for RemSigned.
		/// </summary>
		/// <param name="ptr">The instruction pointer.</param>
		void RemSigned(InstrPointer ptr);

		/// <summary>
		/// Visitation function for RemUnsigned.
		/// </summary>
		/// <param name="ptr">The instruction pointer.</param>
		void RemUnsigned(InstrPointer ptr);

		/// <summary>
		/// Visitation function for Switch.
		/// </summary>
		/// <param name="ptr">The instruction pointer.</param>
		void Switch(InstrPointer ptr);

		/// <summary>
		/// Visitation function for Break.
		/// </summary>
		/// <param name="ptr">The instruction pointer.</param>
		void Break(InstrPointer ptr);

		/// <summary>
		/// Visitation function for Throw.
		/// </summary>
		/// <param name="ptr">The instruction pointer.</param>
		void Throw(InstrPointer ptr);

		/// <summary>
		/// Visitation function for EHPrologue.
		/// </summary>
		/// <param name="ptr">The instruction pointer.</param>
		void EHPrologue(InstrPointer ptr);

		/// <summary>
		/// Visitation function for intrinsic the method call.
		/// </summary>
		/// <param name="ptr">The instruction pointer.</param>
		void IntrinsicMethodCall(InstrPointer ptr);
	}
}