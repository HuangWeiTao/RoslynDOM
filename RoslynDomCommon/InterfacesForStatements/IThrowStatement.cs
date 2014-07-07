﻿using System.Collections.Generic;
using RoslynDom.Common;

namespace RoslynDomCommon.InterfacesForStatements
{
    public interface IThrowStatement : IStatement
    {
        string ExceptionVarName { get; }
        IReferencedType ExceptionType { get; }
        IEnumerable<IArgument> Arguments { get; }
    }
}
