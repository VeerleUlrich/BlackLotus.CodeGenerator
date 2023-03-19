using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackLotus.CodeGenerator
{
    public static class ExtensionMethods 
    {
        public static string GetPropertyName(this PropertyDeclarationSyntax property)
        {
            return property.Identifier.ValueText;
        }
    }
}
