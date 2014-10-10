using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using RoslynDom.Common;
using System.ComponentModel.DataAnnotations;
namespace RoslynDom
{
   public class RDomThrowStatement : RDomBase<IThrowStatement, ISymbol>, IThrowStatement
   {
public RDomThrowStatement(IExpression _exceptionExpression)
: this(null, null, null)
{
    NeedsFormatting = true;
    ExceptionExpression = _exceptionExpression;
}
      public RDomThrowStatement(SyntaxNode rawItem, IDom parent, SemanticModel model)
         : base(rawItem, parent, model)
      { }

      [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
        "CA1811:AvoidUncalledPrivateCode", Justification = "Called via Reflection")]
      internal RDomThrowStatement(RDomThrowStatement oldRDom)
          : base(oldRDom)
      {
         ExceptionExpression = oldRDom.ExceptionExpression.Copy();
      }

      public IExpression ExceptionExpression
      {
         get { return _exceptionExpression; }
         set { SetProperty(ref _exceptionExpression, value); }
      }
      private IExpression _exceptionExpression;
   }
}