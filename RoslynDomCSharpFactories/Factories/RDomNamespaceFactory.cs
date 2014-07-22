﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using RoslynDom.Common;

namespace RoslynDom.CSharp
{

    public class RDomNamespaceStemMemberFactory
           : RDomStemMemberFactory<RDomNamespace, NamespaceDeclarationSyntax>
    {
        public override IEnumerable<IStemMember > CreateFrom(SyntaxNode syntaxNode, IDom parent, SemanticModel model)
        {
            var syntax = syntaxNode as NamespaceDeclarationSyntax;
            var newItem = new RDomNamespace(syntaxNode, parent,model);

            // Qualified name unbundles namespaces, and if it's defined together, we want it together here. 
            // Thus, this replaces hte base Initialize name with the correct one
            newItem.Name = newItem.TypedSyntax.NameFrom();
            if (newItem.Name.StartsWith("@")) { newItem.Name = newItem.Name.Substring(1); }
            var members = ListUtilities.MakeList(syntax, x => x.Members, x => RDomFactoryHelper.GetHelper<IStemMember>().MakeItem(x, newItem, model));
            var usings = ListUtilities.MakeList(syntax, x => x.Usings, x => RDomFactoryHelper.GetHelper<IStemMember>().MakeItem(x, newItem, model));
            newItem.StemMembersAll.AddOrMoveRange(members);
            newItem.StemMembersAll.AddOrMoveRange(usings);


            return new IStemMember[] { newItem };
        }
        public override IEnumerable<SyntaxNode> BuildSyntax(IStemMember item)
        {
            var identifier = SyntaxFactory.IdentifierName(item.Name);
            var node = SyntaxFactory.NamespaceDeclaration (identifier);
            var itemAsNamespace = item as INamespace;
            if (itemAsNamespace == null) { throw new InvalidOperationException(); }
            var usingsSyntax = itemAsNamespace.Usings
                        .Select(x => RDomCSharpFactory.Factory.BuildSyntaxGroup(x))
                        .OfType<UsingDirectiveSyntax>()
                        .ToList();
            if (usingsSyntax.Count() > 0) { node = node.WithUsings(SyntaxFactory.List<UsingDirectiveSyntax>(usingsSyntax)); }

            var membersSyntax = itemAsNamespace.StemMembers
                        .SelectMany(x => RDomCSharpFactory.Factory.BuildSyntaxGroup(x))
                        .OfType<MemberDeclarationSyntax>()
                        .ToList();
            if (membersSyntax.Count() > 0) { node = node.WithMembers(SyntaxFactory.List<MemberDeclarationSyntax>(membersSyntax)); }
            // TODO: return new SyntaxNode[] { node.Format() };
            return new SyntaxNode[] { node };
        }
    }


}
