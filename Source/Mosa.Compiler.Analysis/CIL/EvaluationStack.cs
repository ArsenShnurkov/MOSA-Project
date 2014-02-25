/*
 * (c) 2014 MOSA - The Managed Operating System Alliance
 *
 * Licensed under the terms of the New BSD License.
 *
 * Authors:
 *  Ki (kiootic) <kiootic@gmail.com>
 */

using System;

namespace Mosa.Compiler.Analysis.CIL
{
	internal class EvaluationStack
	{
		int ptr = 0;
		Value[] stack;

		public EvaluationStack(uint maxStack)
		{
			stack = new Value[maxStack];
		}

		public EvaluationStack(EvaluationStack origin)
		{
			ptr = origin.ptr;
			stack = (Value[])origin.stack.Clone();
		}

		public void Push(Value value)
		{
			if (ptr == stack.Length)
				throw new InvalidOperationException("Evaluation stack overflow.");

			stack[ptr++] = value;
		}

		public Value Pop()
		{
			if (ptr == 0)
				throw new InvalidOperationException("Evaluation stack empty.");

			return stack[--ptr];
		}

		public Value Peek()
		{
			if (ptr == 0)
				throw new InvalidOperationException("Evaluation stack empty.");

			return stack[ptr - 1];
		}

		public int Depth { get { return ptr; } }

		public Value[] Stack { get { return stack; } }
	}
}
