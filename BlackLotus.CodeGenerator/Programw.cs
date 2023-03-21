// using System;
// using System.Collections.Generic;
// using System.IO;
// using System.Linq;
// using System.Text;
// using Microsoft.CodeAnalysis;
// using Microsoft.CodeAnalysis.CSharp;
// using Microsoft.CodeAnalysis.CSharp.Syntax;
//
// namespace BlackLotus.CodeGenerator
// {
//     public partial class Program
//     {
//         private static string _command;
//         private static string _domainName = "Inventory"; //== name of project
//         private static string _modelName = "Experiments"; 
//         private static string _queryName = "GetExperimentsBySubgroupId"; 
//         private static string _pathToBlackLotus = @"D:\git_repos\BlackLotus\src";
//
//         public static void Main(string[] args)
//         {
//             Console.WriteLine("starting!");
//             return;
//             SourceGeneratorStarter.Start();
//             return;
//             //HOW TO BUILD A TREE
//             
//             
//             var domainInfo = new DomainInfo(_domainName, _modelName,
//                 $@"{_pathToBlackLotus}\{_domainName}.Infrastructure\{_modelName}", _queryName);
//             _command = "scratch";
//             if(_command == "scratch")
//             {
//                 CreateModelFiles(domainInfo);
//             }
//
//             return;
//             
//             var sourceCode = File.ReadAllText(@"D:\Code\C#\roslyn\BlackLotus.CodeGenerator\BlackLotus.CodeGenerator\Task.cs");
//             var root = GetRoot(sourceCode);
//             //identify model?
//             var models = ReadModelFile(root);
//             //return;
//             var className = root.DescendantNodes().OfType<ClassDeclarationSyntax>().First();
//             var methods = root.DescendantNodes().OfType<MethodDeclarationSyntax>();
//
//             Console.WriteLine(className.Identifier.ToString());
//             foreach (var method in methods)
//             {
//                 Console.WriteLine(method.Identifier.ToString());
//             }
//
//             //SyntaxTree tree = CSharpSyntaxTree.ParseText(sourceCode);
//             //CompilationUnitSyntax root = tree.GetCompilationUnitRoot();
//             //var classes = root.DescendantNodes().OfType<ClassDeclarationSyntax>();
//             //Console.WriteLine($"The tree is a {root.Kind()} node.");
//             //Console.WriteLine($"The tree has {root.Members.Count} elements in it.");
//             //Console.WriteLine($"The tree has {root.Usings.Count} using statements. They are:");
//             //foreach (UsingDirectiveSyntax element in root.Usings)
//             //    Console.WriteLine($"\t{element.Name}");
//             return;
//
//
//             //domain + queryName + models
//             var domainName = args[0];
//             var subdomainName = args[1];
//             var queryName = args[2];
//             var commannd = args[3];
//
//             //1. read existing code per layer
//
//             //NB: we can automate the creation of the objecttypes from the models
//             //read model (get current location + file with models)
//             //make list of all models
//             //write objecttypes from models
//
//
//             //2. change existing code per layer
//
//             // We will change the namespace of this sample code.
//             var code =
//             @"  using System; 
//
//                 namespace OldNamespace 
//                 { 
//                     public class Person
//                     {
//                         public string Name { get; set; }
//                         public int Age {get; set; }
//                     }
//                 }";
//
//
//             var dateAddition = DateTime.Now;
//             File.WriteAllText($@"D:\Code\C#\roslyn\BlackLotus.CodeGenerator\testfiles\{dateAddition}_test.cs", code);
//         }
//
//         private static void CreateModelFiles(DomainInfo domainInfo)
//         {
//             //check if folders exist.
//             var infraProject = Directory.GetDirectories($@"{_pathToBlackLotus}\{domainInfo.DomainName}.Infrastructure");
//             var pathToModel = @$"{_pathToBlackLotus}\{domainInfo.DomainName}.Infrastructure\{domainInfo.ModelName}";
//             if (!Directory.Exists(pathToModel))
//             {
//                 Directory.CreateDirectory(@$"{pathToModel}\QueryModels\Models");
//                 CreateModelClass(domainInfo);
//                 CreateQueryModelClass(domainInfo);
//             }
//                 AddTableToDataConnection(domainInfo);
//             
//             //also create table in dataconnection
//         }
//
//         private static void AddTableToDataConnection(DomainInfo domainInfo)
//         {
//             //find location of last item. add after
//             var dataConnectionPath = @"D:\git_repos\BlackLotus\src\Inventory.Infrastructure\InventoryDataConnection2.cs";
//             var dataConnectionFile =
//                 File.ReadAllText(dataConnectionPath);
//             // Microsoft.CodeAnalysis.D  
//             var root = GetRoot(dataConnectionFile);
//             var propertyNodes = root.DescendantNodes()
//                 .OfType<PropertyDeclarationSyntax>();
//             var property =
//                 SyntaxFactory.PropertyDeclaration(SyntaxFactory.ParseTypeName("int"), "TEST")
//                     .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
//                     .AddAccessorListAccessors(
//                         SyntaxFactory.AccessorDeclaration(SyntaxKind.GetAccessorDeclaration).WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken)),
//                         SyntaxFactory.AccessorDeclaration(SyntaxKind.SetAccessorDeclaration).WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken))
//                     );
//             var newPropertyNodes = propertyNodes.Append(property);
//             // root.ReplaceNode(propertyNodes, newPropertyNodes);
//             var dataConnectionPath2 =
//                 @"D:\git_repos\BlackLotus\src\Inventory.Infrastructure\InventoryDataConnection3.cs";
//             try
//             {
//                 File.WriteAllText(dataConnectionPath2, root.ToFullString()); //doesnt write property to file yet
//                 File.WriteAllText(@$"{domainInfo.PathToModel}\QueryModels\Models\test.cs", "blaaaa");
//
//
//             }
//             catch (Exception ex)
//             {
//                 
//             }
//             // dataConnectionFile.WithSyntaxRoot(root.ReplaceNode(classeDecl, newClass));
//
//         }
//
//         // private PropertyDeclarationSyntax CreateProperty(string name)
//         // {
//         //     
//         // };
//
//         private static void CreateModelClass(DomainInfo domainInfo)
//         {
//             var className = @$"{_modelName}.cs";
//             var namespaceName = $"{domainInfo.DomainName}.Infrastructure.{domainInfo.ModelName}.QueryModels.Models";
//             var modelFile = @$"using {namespaceName};
// using LinqToDB.Mapping;
// using Rna.LinqToDB;
//
// namespace {namespaceName}
//
// [Table("""")] 
// {{ 
//     public abstract class {domainInfo.ModelName}
//     {{
//         
//     }}
// }}";
//             File.WriteAllText(@$"{domainInfo.PathToModel}\QueryModels\Models\{className}", modelFile);
//             //D:\git_repos\BlackLotus\src\Inventory.Infrastructure\Equipments\QueryModels\Models\Equipment.cs
//             //D:\git_repos\BlackLotus\src\Inventory.Infrastructure\Experiments\QueryModels\Models
//         }
//         
//         private static void CreateQueryModelClass(DomainInfo domainInfo)
//         {
//              var className = @$"{_modelName}QueryModel.cs";
//              File.WriteAllText(@$"{domainInfo.PathToModel}\QueryModels\{className}", "bla");
// //             var model = @"  using System; 
// //
// //                 namespace OldNamespace 
// //                 { 
// //                     public class Person
// //                     {
// //                         public string Name { get; set; }
// //                         public int Age {get; set; }
// //                     }
// //                 }";
// //             File.WriteAllText(@$"{pathToModel}\QueryModels\Models\{className}", "");
//         }
//
//         private static object ReadModelFile(SyntaxNode root)
//         {
//             //create an object that contains the classname, the properties and the attributes on the properties
//             var models = root.DescendantNodes().OfType<ClassDeclarationSyntax>();
//             var firstModel = models.FirstOrDefault();
//             var firstModelName = firstModel.Identifier.ToString();
//             var properties = firstModel!.DescendantNodes().OfType<PropertyDeclarationSyntax>();
//             var firstProperty = properties.FirstOrDefault();
//
//             var objectTypeDescriptors = new StringBuilder();
//             foreach (var property in properties)
//             {
//                 var attributes = property.AttributeLists;
//                 foreach (var item in attributes)
//                 {
//                     Console.WriteLine(item.Attributes.First().Name.NormalizeWhitespace().ToFullString());
//                 }
//                 //TODO: if abstract class, interfacetype
//                 if (attributes.Any(attribute => attribute.Attributes.First().Name.NormalizeWhitespace().ToFullString() == "Column")){
//                     objectTypeDescriptors.Append(@$"objectTypeDescriptor.Field({firstModelName.ToLower()} => {firstModelName.ToLower()}.{property.GetPropertyName()});");
//                 }
//             }
//  
//             var txt = @$"public class protected {firstModelName} : InterfaceTypeDescriptor<{firstModelName}> descriptor) 
//                 {{
//                     override void Configure(IInterfaceTypeDescriptor<{firstModelName}>descriptor 
//                     {{
//                     var objectTypeDescriptor = descriptor.Name(nameof({firstModelName})).BindFieldsExplicitly();
//                     }}
//                     {objectTypeDescriptors}
//                 }}";
//
//             File.WriteAllText(@"D:\Code\C#\roslyn\BlackLotus.CodeGenerator\testfiles\test.cs", txt);
//             //foreach (var model in models)
//             //{
//             //    var members = model.Members;
//             //    foreach (var member in members)
//             //    {
//             //        if (member is PropertyDeclarationSyntax property)
//             //        {
//             //            var t = member.iden
//             //        }
//             //    }
//             //}
//             return new ModelClass("bla");
//         }
//
//         private static string GetPropertyName(PropertyDeclarationSyntax property)
//         {
//             return property.Identifier.ValueText;
//         }
//
//         private static SyntaxNode GetRoot(string sourceCode)
//         {
//             var tree = CSharpSyntaxTree.ParseText(sourceCode);
//         
//             return tree.GetRoot();
//         }
//
//         static void CreateFile(string code)
//         {
//            
//         }
//
//         public record DomainInfo(string DomainName, string ModelName, string PathToModel, string QueryName);
//         public record ModelClass(string className); //old class
//         public record ObjectTypeClass(string className, List<Property> properties); //new class
//         public record Property(string name, Attribute attribute); 
//         public record Attribute(string name);
//         
//         partial class SourceGeneratorStarter
//         {
//             public static void Start()
//             {
//                 HelloFrom("Generated Code");
//             }
//
//             static partial void HelloFrom(string name);
//         }
//     }
// }
