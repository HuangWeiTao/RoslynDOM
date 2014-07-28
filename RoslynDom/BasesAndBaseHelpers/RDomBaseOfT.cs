﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.CodeAnalysis;
using RoslynDom.Common;
using RoslynDomCommon;

namespace RoslynDom
{
    public abstract class RDomBase<T> : RDomBase, IDom<T>, IRoslynHasSymbol
          where T : class, IDom<T>
    {
        private ISameIntent<T> sameIntent = SameIntent_Factory.SameIntent<T>();

        protected RDomBase()
          : base()
        { }

        protected RDomBase(T oldRDom)
         : base(oldRDom)
        { }

        public abstract ISymbol Symbol { get; }

        public virtual T Copy()
        {
            var type = this.GetType();
            var constructor = type.GetTypeInfo()
                .DeclaredConstructors
                .Where(x => x.GetParameters().Count() == 1
                && typeof(T).IsAssignableFrom(x.GetParameters().First().ParameterType))
                .FirstOrDefault();
            if (constructor == null) throw new InvalidOperationException("Missing constructor for clone");
            var newItem = constructor.Invoke(new object[] { this });
            return (T)newItem;
        }

        protected override sealed bool SameIntentInternal<TLocal>(TLocal other, bool skipPublicAnnotations)
        {
            var otherAsT = other as T;
            var thisAsT = this as T;
            if (otherAsT == null) return false;
            if (!CheckSameIntent(otherAsT, skipPublicAnnotations)) { return false; }
            if (!StandardSameIntent.CheckSameIntent(thisAsT, otherAsT, skipPublicAnnotations)) return false; ;
            //if (sameIntent == null) return true; // assume that CheckSameIntent override does work
            //return sameIntent.SameIntent(this as T, other as T, skipPublicAnnotations);
            return true;
        }

        /// <summary>
        /// Derived classes can override this if the RoslynDom.Common implementations aren't working. 
        /// Do NOT override if the problem can be solved in the RoslynDom.Common implementations (SameIntent_xxx)
        /// </summary>
        /// <param name="other"></param>
        /// <param name="skipPublicAnnotations"></param>
        /// <returns></returns>
        protected virtual bool CheckSameIntent(T other, bool skipPublicAnnotations)
        {
            return true;
        }

    }
}
