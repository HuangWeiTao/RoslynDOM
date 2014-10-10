using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using RoslynDom.Common;
using cm=System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace RoslynDom
{
   public class RDomInvocationStatement : RDomBase<IInvocationStatement, ISymbol>, IInvocationStatement
   {

      /// <summary>
      /// Constructor to use when creating a RoslynDom from scratch
      /// </summary>
      /// <param name="expression">
      /// Expression to invoke
      /// </param>
      public RDomInvocationStatement(IExpression expression, bool suppressNewLine = false)
       : this(null, null, null)
      {
         Invocation = expression;
         if (!suppressNewLine)
         { Whitespace2Set.Add(new Whitespace2(LanguageElement.EndOfLine, "", "\r\n", "")); }
      }

      [cm.EditorBrowsable(cm.EditorBrowsableState.Never)]
      public RDomInvocationStatement(SyntaxNode rawItem, IDom parent, SemanticModel model)
           : base(rawItem, parent, model)
      { }

      [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
         "CA1811:AvoidUncalledPrivateCode", Justification = "Called via Reflection")]
      internal RDomInvocationStatement(RDomInvocationStatement oldRDom)
         : base(oldRDom)
      {
         Invocation = oldRDom.Invocation.Copy();
      }

      public override IEnumerable<IDom> Children
      { get { return new List<IDom>() { Invocation }; } }

      [Required]
      public IExpression Invocation
      {
         get { return _invocation; }
         set { SetProperty(ref _invocation, value); }
      }
      private IExpression _invocation;
   }
}