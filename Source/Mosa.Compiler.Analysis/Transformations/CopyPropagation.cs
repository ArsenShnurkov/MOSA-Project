/*
 * (c) 2014 MOSA - The Managed Operating System Alliance
 *
 * Licensed under the terms of the New BSD License.
 *
 * Authors:
 *  Ki (kiootic) <kiootic@gmail.com>
 */

using Mosa.Compiler.Analysis.Blocks;
using Mosa.Compiler.Analysis.IR;
using Mosa.Compiler.Analysis.Operands;

namespace Mosa.Compiler.Analysis.Transformations
{
	/* 
	 * This transformation propagates the value of single-use variables to the use site.
	 */
	public class CopyPropagation
	{
		MethodBody body;

		public CopyPropagation(MethodBody body)
		{
			this.body = body;
		}

		public void Run()
		{
			InstrPointer ptr = new InstrPointer();
			foreach (var block in body.GetAllBlocks())
			{
				ptr.SetBlock(block);
				do
				{
					if (ptr.Instruction.OpCode != IROpCodes.Move)
						continue;
					Value result = ptr.Instruction.Result;
					if (result.Usages.Count == 1)
					{
						Instruction usage = result.Usages[0];
						if (usage.OpCode != IROpCodes.Phi)
						{
							for (int i = 0; i < usage.Operands.Count; i++)
								if (usage.Operands[i] is ValueOperand && ((ValueOperand)usage.Operands[i]).Value == result)
								{
									usage.Operands[i] = ptr.Instruction.Operand1;
									break;
								}
							ptr.SetOpCode(IROpCodes.Nop);
						}
					}
				} while (ptr.MoveNext());
			}
		}
	}
}
