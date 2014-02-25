using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mosa.Compiler.MosaTypeSystem;
using Mosa.Compiler.Analysis.IR;

namespace Mosa.Compiler.Analysis.CIL
{
	internal abstract class CILOpCode
	{
		public CILOpCode(CILCode code)
		{
			this.Code = code;
		}

		public CILCode Code { get; private set; }
		public virtual FlowControl FlowControl { get { return FlowControl.Next; } }

		public abstract void Parse(TransformContext ctx);
	}
}
