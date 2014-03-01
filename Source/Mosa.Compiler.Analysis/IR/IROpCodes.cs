/*
 * (c) 2008 MOSA - The Managed Operating System Alliance
 *
 * Licensed under the terms of the New BSD License.
 *
 * Authors:
 *  Phil Garcia (tgiphil) <phil@thinkedge.com>
 */

using Mosa.Compiler.Analysis.IR.OpCodes;

namespace Mosa.Compiler.Analysis.IR
{
	public class IROpCodes
	{
		public static readonly Nop Nop = new Nop();

		public static readonly Branch Branch = new Branch();

		public static readonly CondBranch CondBranch = new CondBranch();

		public static readonly AddFloat AddF = new AddFloat();

		public static readonly AddSigned AddS = new AddSigned();

		public static readonly AddUnsigned AddU = new AddUnsigned();

		public static readonly AddressOf AddressOf = new AddressOf();

		public static readonly ArithmeticShiftRight ArithmeticShiftRight = new ArithmeticShiftRight();

		public static readonly Break Break = new Break();

		public static readonly DivFloat DivF = new DivFloat();

		public static readonly DivSigned DivS = new DivSigned();

		public static readonly DivUnsigned DivU = new DivUnsigned();

		public static readonly Epilogue Epilogue = new Epilogue();

		public static readonly FloatCompare FloatCompare = new FloatCompare();

		public static readonly FloatToIntegerConversion FloatingPointToIntegerConversion = new FloatToIntegerConversion();

		public static readonly IntegerCompare IntegerCompare = new IntegerCompare();

		public static readonly IntegerToFloatConversion IntegerToFloatConversion = new IntegerToFloatConversion();
		
		public static readonly Load Load = new Load();

		public static readonly LoadZeroExtended LoadZeroExtended = new LoadZeroExtended();

		public static readonly LoadSignExtended LoadSignExtended = new LoadSignExtended();

		public static readonly LogicalAnd LogicalAnd = new LogicalAnd();

		public static readonly LogicalNot LogicalNot = new LogicalNot();

		public static readonly LogicalOr LogicalOr = new LogicalOr();

		public static readonly LogicalXor LogicalXor = new LogicalXor();

		public static readonly Negate Negate = new Negate();

		public static readonly Move Move = new Move();

		public static readonly MulFloat MulF = new MulFloat();

		public static readonly MulSigned MulS = new MulSigned();

		public static readonly MulUnsigned MulU = new MulUnsigned();

		public static readonly Prologue Prologue = new Prologue();

		public static readonly RemFloat RemF = new RemFloat();

		public static readonly RemSigned RemS = new RemSigned();

		public static readonly RemUnsigned RemU = new RemUnsigned();

		public static readonly Return Return = new Return();

		public static readonly EHEpilogue EHEpilogue = new EHEpilogue();

		public static readonly ShiftRight ShiftRight = new ShiftRight();

		public static readonly ShiftLeft ShiftLeft = new ShiftLeft();

		public static readonly MoveSignExtended MoveSignExtended = new MoveSignExtended();

		public static readonly Store Store = new Store();

		public static readonly SubFloat SubF = new SubFloat();

		public static readonly SubSigned SubS = new SubSigned();

		public static readonly SubUnsigned SubU = new SubUnsigned();

		public static readonly MoveZeroExtended MoveZeroExtended = new MoveZeroExtended();

		public static readonly Call Call = new Call();

		public static readonly Switch Switch = new Switch();

		public static readonly Throw Throw = new Throw();

		public static readonly EHPrologue EHPrologue = new EHPrologue();

		public static readonly IntrinsicMethodCall IntrinsicMethodCall = new IntrinsicMethodCall();

		public static readonly Phi Phi = new Phi();
	}
}
