using System;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using BlackLotus.CodeGenerator;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Roslyn.CodeGeneration.Program;

namespace Roslyn.CodeGeneration
{
    public class Program
    {
        private static string _command;

        public static void Main(string[] args)
        {
            _command = "scratch";
            if(_command == "scratch")
            {
                var modelName = args[0];
                CreateModelFiles(modelName);
            }
            var sourceCode = File.ReadAllText(@"D:\Code\C#\roslyn\BlackLotus.CodeGenerator\BlackLotus.CodeGenerator\Task.cs");
            var root = GetRoot(sourceCode);
            //identify model?
            var models = ReadModelFile(root);
            //return;
            var className = root.DescendantNodes().OfType<ClassDeclarationSyntax>().First();
            var methods = root.DescendantNodes().OfType<MethodDeclarationSyntax>();

            Console.WriteLine(className.Identifier.ToString());
            foreach (var method in methods)
            {
                Console.WriteLine(method.Identifier.ToString());
            }

            //SyntaxTree tree = CSharpSyntaxTree.ParseText(sourceCode);
            //CompilationUnitSyntax root = tree.GetCompilationUnitRoot();
            //var classes = root.DescendantNodes().OfType<ClassDeclarationSyntax>();
            //Console.WriteLine($"The tree is a {root.Kind()} node.");
            //Console.WriteLine($"The tree has {root.Members.Count} elements in it.");
            //Console.WriteLine($"The tree has {root.Usings.Count} using statements. They are:");
            //foreach (UsingDirectiveSyntax element in root.Usings)
            //    Console.WriteLine($"\t{element.Name}");
            return;


            //domain + queryName + models
            var domainName = args[0];
            var subdomainName = args[1];
            var queryName = args[2];
            var commannd = args[3];

            //1. read existing code per layer

            //NB: we can automate the creation of the objecttypes from the models
            //read model (get current location + file with models)
            //make list of all models
            //write objecttypes from models


            //2. change existing code per layer

            // We will change the namespace of this sample code.
            var code =
            @"  using System; 

                namespace OldNamespace 
                { 
                    public class Person
                    {
                        public string Name { get; set; }
                        public int Age {get; set; }
                    }
                }";


            var dateAddition = DateTime.Now;
            File.WriteAllText($@"D:\Code\C#\roslyn\BlackLotus.CodeGenerator\testfiles\{dateAddition}_test.cs", code);
        }

        private static void CreateModelFiles(string modelName)
        {
            
        }

        private static object ReadModelFile(SyntaxNode root)
        {
            //create an object that contains the classname, the properties and the attributes on the properties
            var models = root.DescendantNodes().OfType<ClassDeclarationSyntax>();
            var firstModel = models.FirstOrDefault();
            var firstModelName = firstModel.Identifier.ToString();
            var properties = firstModel!.DescendantNodes().OfType<PropertyDeclarationSyntax>();
            var firstProperty = properties.FirstOrDefault();

            var objectTypeDescriptors = new StringBuilder();
            foreach (var property in properties)
            {
                var attributes = property.AttributeLists;
                foreach (var item in attributes)
                {
                    Console.WriteLine(item.Attributes.First().Name.NormalizeWhitespace().ToFullString());
                }
                //TODO: if abstract class, interfacetype
                if (attributes.Any(attribute => attribute.Attributes.First().Name.NormalizeWhitespace().ToFullString() == "Column")){
                    objectTypeDescriptors.Append(@$"objectTypeDescriptor.Field({firstModelName.ToLower()} => {firstModelName.ToLower()}.{property.GetPropertyName()});");
                }
            }
 
            var txt = @$"public class protected {firstModelName} : InterfaceTypeDescriptor<{firstModelName}> descriptor) 
                {{
                    override void Configure(IInterfaceTypeDescriptor<{firstModelName}>descriptor 
                    {{
                    var objectTypeDescriptor = descriptor.Name(nameof({firstModelName})).BindFieldsExplicitly();
                    }}
                    {objectTypeDescriptors}
                }}";

            File.WriteAllText(@"D:\Code\C#\roslyn\BlackLotus.CodeGenerator\testfiles\test.cs", txt);
            //foreach (var model in models)
            //{
            //    var members = model.Members;
            //    foreach (var member in members)
            //    {
            //        if (member is PropertyDeclarationSyntax property)
            //        {
            //            var t = member.iden
            //        }
            //    }
            //}
            return new ModelClass("bla");
        }

        private static string GetPropertyName(PropertyDeclarationSyntax property)
        {
            return property.Identifier.ValueText;
        }

        private static SyntaxNode GetRoot(string sourceCode)
        {
            var tree = CSharpSyntaxTree.ParseText(sourceCode);
        
            return tree.GetRoot();
        }

        static void CreateFile(string code)
        {
           
        }

        public record ModelClass(string className); //old class
        public record ObjectTypeClass(string className, List<Property> properties); //new class
        public record Property(string name, Attribute attribute); 
        public record Attribute(string name); 
    }
}
