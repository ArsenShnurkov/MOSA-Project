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
using Mosa.Compiler.Framework;

namespace Mosa.Compiler.Analysis.Stages
{
	public class IRConstructionStage : BaseMethodCompilerStage, IMethodCompilerStage
	{
		void IMethodCompilerStage.Run()
		{
			MethodBody body = BlockParser.Parse(methodCompiler.Method);
			IRTransformer.Transform(body);
		}
	}
}
