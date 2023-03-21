using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.CodeAnalysis.Text;

namespace BlackLotus.SourceGenerator
{

    [Generator]
    public class TypeGenerator : IIncrementalGenerator
    {
        public void Initialize(IncrementalGeneratorInitializationContext context)
        {
            var classDeclarations = context.SyntaxProvider.CreateSyntaxProvider(
                predicate: (s, t) => s is ClassDeclarationSyntax,
                transform: GetTypeSymbols).Collect();
            // var classDeclarations = context.SyntaxProvider
            //     .CreateSyntaxProvider(
            //         (s, _) => s is ClassDeclarationSyntax cds && cds.Identifier.Text.EndsWith("ment"),
            //         (c, _) => c.Node as ClassDeclarationSyntax)
            //     .Where(x => x != null)
            //     .Collect();

            context.RegisterSourceOutput(classDeclarations, GenerateSource);

// Workaround for debugging source generator. 
// Incomment below lines, rebuild solution and attach to the process in VS
//#if DEBUG
//            if (!Debugger.IsAttached)
//            {
//                Debugger.Launch();
//            }
//#endif
        }
        private void GenerateSource(SourceProductionContext context, ImmutableArray<ITypeSymbol> typeSymbols)
        {
            var sb = new StringBuilder();
            foreach (var symbol in typeSymbols)
            {
                if (symbol is null)
                    continue;

                sb.AppendLine("// " + symbol.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat));
            }

            context.AddSource($"all_types.cs", SourceText.From(sb.ToString(), Encoding.UTF8));
        }
        private ITypeSymbol GetTypeSymbols(GeneratorSyntaxContext context, CancellationToken cancellationToken)
        {
            var decl = (ClassDeclarationSyntax)context.Node;

            if (context.SemanticModel.GetDeclaredSymbol(decl, cancellationToken) is ITypeSymbol typeSymbol)
            {
                return typeSymbol;
            }

            return null;
        }

        private void GenerateSource(SourceProductionContext context, ImmutableArray<ClassDeclarationSyntax> Classes)
        {
            foreach (var foundClass in Classes)
            {
                var className = foundClass.Identifier.Text;
                var properties = foundClass.Members.Select(m => m as PropertyDeclarationSyntax)
                    .Where(x => x != null)
                    .Select(x => x.Identifier.Text)
                    .ToList();

                var classNsp = foundClass.Ancestors().FirstOrDefault(x => x is NamespaceDeclarationSyntax) as NamespaceDeclarationSyntax;
                var nmspName = classNsp.Name.ToString();

                var newClass = $@"namespace {nmspName}
            {{

                public partial class {className}
                {{
                    public int Test()
                    {{
                        return 'bla';
                    }}
                }}
            }}";
                context.AddSource($"{className}.g.cs", newClass);
            }
        }

    }

}
