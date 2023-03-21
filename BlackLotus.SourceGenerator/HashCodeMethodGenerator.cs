using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Immutable;
using System.Linq;

namespace BlackLotus.SourceGenerator
{

    [Generator]
    public class HashCodeMethodGenerator : IIncrementalGenerator
    {
        public void Initialize(IncrementalGeneratorInitializationContext context)
        {

            var classDeclarations = context.SyntaxProvider
                .CreateSyntaxProvider(
                    (s, _) => s is ClassDeclarationSyntax cds && cds.Identifier.Text.EndsWith("Model"),
                    (c, _) => c.Node as ClassDeclarationSyntax)
                .Where(x => x != null)
                .Collect();

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
                    public override int GetHashCode()
                    {{
                        return HashCode.Combine({string.Join(", ", properties)});
                    }}
                }}
            }}";
                context.AddSource($"{className}.g.cs", newClass);
            }
        }

    }

}
