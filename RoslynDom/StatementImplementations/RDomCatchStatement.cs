﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using RoslynDom.Common;

namespace RoslynDom
{
    public class RDomCatchStatement : RDomStatementBlockBase<ICatchStatement>, ICatchStatement
    {
        public RDomCatchStatement(SyntaxNode rawItem, IDom parent, SemanticModel model)
           : base(rawItem, parent, model)
        { Initialize(); }

        internal RDomCatchStatement(RDomCatchStatement oldRDom)
            : base(oldRDom)
        {
            if (oldRDom.Condition != null)
            { Condition = oldRDom.Condition.Copy(); }
            if (oldRDom.Variable != null)
            { Variable = oldRDom.Variable.Copy(); }
            if (oldRDom.ExceptionType != null)
            { ExceptionType = oldRDom.ExceptionType.Copy(); }
        }

        public override IEnumerable<IDom> Children
        {
            get
            {
                var list = new List<IDom>();
                list.Add(Condition);
                list.AddRange(base.Children.ToList());
                return list;
            }
        }

        public override IEnumerable<IDom> Descendants
        {
            get
            {
                var list = new List<IDom>();
                list.AddRange(Condition.DescendantsAndSelf);
                list.AddRange(base.Descendants.ToList());
                return list;
            }
        }

        public IExpression Condition { get; set; }
        public IVariableDeclaration Variable { get; set; }
        public IReferencedType  ExceptionType { get; set; }

    }
}