using System.Collections.Generic;
using System.Threading;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace BlackLotus.SourceGenerator;


[Generator]
public class FilterGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        IncrementalValuesProvider<EnumDeclarationSyntax> enumDeclarations = context.SyntaxProvider
            .CreateSyntaxProvider(
                predicate: static (s, _) => IsAttribute(s), // select nodes with the marker attribute
                transform: static (ctx, _) => GetFilterAttrivutes(ctx)) // select the enum with the [GraphQLFilter] attribute
            .Where(static m => m is not null)!; // filter out attributed enums that we don't care about
    }
    
    // private static ISyntaxInfo? TryGetFilterAttributes(
    //     GeneratorSyntaxContext context,
    //     CancellationToken cancellationToken)
    // {
    //     if (context.Node is BaseTypeDeclarationSyntax { AttributeLists.Count: > 0 } possibleType)
    //         foreach (var attributeListSyntax in possibleType.AttributeLists)
    //         {
    //             foreach (var attributeSyntax in attributeListSyntax.Attributes)
    //             {
    //                 var symbol = context.SemanticModel.GetSymbolInfo(attributeSyntax).Symbol;
    //
    //                 if (symbol is not IMethodSymbol attributeSymbol)
    //                 {
    //                     continue;
    //                 }
    //
    //                 var attributeContainingTypeSymbol = attributeSymbol.ContainingType;
    //                 var fullName = attributeContainingTypeSymbol.ToDisplayString();
    //                 
    //                 // Check hotchocolate TypeAttributeInspector.cs for a capture of the generic and non generic variant of the object type extension attribute.
    //                 
    //                 
    //             }
    //         }
    //     return null;
    // }
    //
    static EnumDeclarationSyntax? GetFilterAttrivutes(GeneratorSyntaxContext context)
    {
        // we know the node is a EnumDeclarationSyntax thanks to IsSyntaxTargetForGeneration
        var enumDeclarationSyntax = (EnumDeclarationSyntax)context.Node;

        // loop through all the attributes on the method
        foreach (AttributeListSyntax attributeListSyntax in enumDeclarationSyntax.AttributeLists)
        {
            foreach (AttributeSyntax attributeSyntax in attributeListSyntax.Attributes)
            {
                if (context.SemanticModel.GetSymbolInfo(attributeSyntax).Symbol is not IMethodSymbol attributeSymbol)
                {
                    // weird, we couldn't get the symbol, ignore it
                    continue;
                }

                INamedTypeSymbol attributeContainingTypeSymbol = attributeSymbol.ContainingType;
                string fullName = attributeContainingTypeSymbol.ToDisplayString();

                // Is the attribute the [EnumExtensions] attribute?
                if (fullName == "BlackLotus.CodeGenerator.GraphQLFilterAttribute")
                {
                    // return the enum
                    return enumDeclarationSyntax;
                }
            }
        }

        // we didn't find the attribute we were looking for
        return null;
    }   
    
    private static bool IsAttribute(SyntaxNode node)
        => IsTypeWithAttribute(node);

    private static bool IsTypeWithAttribute(SyntaxNode node)
        => node is BaseTypeDeclarationSyntax { AttributeLists.Count: > 0 };
    
    private static bool IsAssemblyAttributeList(SyntaxNode node)
        => node is AttributeListSyntax;
 
}