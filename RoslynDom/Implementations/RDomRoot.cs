﻿using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using RoslynDom.Common;

namespace RoslynDom
{
     public class RDomRoot : RDomBaseStemContainer<IRoot, ISymbol>, IRoot
    {

        internal RDomRoot(SyntaxNode rawItem)
            : base(rawItem)
        {
            //Initialize2();
        }

        //internal RDomRoot(CompilationUnitSyntax rawItem,
        //    IEnumerable<IStemMember> members,
        //    IEnumerable<IUsing> usings,
        //    params PublicAnnotation[] publicAnnotations)
        //    : base(rawItem, members, usings, publicAnnotations)
        //{
        //    Initialize();
        //}

        internal RDomRoot(RDomRoot oldRDom)
            : base(oldRDom)
        { }

        //protected override void Initialize()
        //{
        //    base.Initialize();
        //    Name = "Root";
        //}
        //protected void Initialize2()
        //{
        //    Initialize();

        //    var members = ListUtilities.MakeList(TypedSyntax, x => x.Members, x => RDomFactoryHelper.StemMemberFactoryHelper.MakeItem(x));
        //    var usings = ListUtilities.MakeList(TypedSyntax, x => x.Usings, x => RDomFactoryHelper.StemMemberFactoryHelper.MakeItem(x));
        //    foreach (var member in members)
        //    { AddOrMoveStemMember(member); }
        //    foreach (var member in usings)
        //    { AddOrMoveStemMember(member); }
        //}

        //public override CompilationUnitSyntax BuildSyntax()
        //{
        //    var node = SyntaxFactory.CompilationUnit();

        //    node = RoslynUtilities.UpdateNodeIfListNotEmpty(BuildUsings(), node, (n, l) => n.WithUsings(l));
        //    node = RoslynUtilities.UpdateNodeIfListNotEmpty(BuildStemMembers(), node, (n, l) => n.WithMembers(l));

        //    return (CompilationUnitSyntax)RoslynUtilities.Format(node);
        //}

        public bool HasSyntaxErrors
        {
            get
            {
                return TypedSyntax.GetDiagnostics().Count() > 0;
            }
        }

        public IEnumerable<IClass> RootClasses
        {
            get
            {
                var rootclasses = from x in NonemptyNamespaces
                                  from y in x.Classes
                                  select y;
                return Classes.Union(rootclasses);
            }
        }

        public IEnumerable<IEnum> RootEnums
        {
            get
            {
                var rootenums = from x in NonemptyNamespaces
                                from y in x.Enums
                                select y;
                return Enums.Union(rootenums);
            }
        }

        public IEnumerable<IInterface> RootInterfaces
        {
            get
            {
                var rootinterfaces = from x in NonemptyNamespaces
                                     from y in x.Interfaces
                                     select y;
                return Interfaces.Union(rootinterfaces);
            }
        }

        public IEnumerable<IStructure> RootStructures
        {
            get
            {
                var rootstructures = from x in NonemptyNamespaces
                                     from y in x.Structures
                                     select y;
                return Structures.Union(rootstructures);
            }
        }
    }
}
