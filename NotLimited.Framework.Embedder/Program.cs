using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NotLimited.Framework.Common.Helpers;
using NotLimited.Framework.Common.Helpers.Xml;

namespace NotLimited.Framework.Embedder
{
    class Program
    {
        private const string Warning =
            @"//////////////////////////////////////////////////////////////////////////
// This file is a part of {0} NuGet package.
// You are strongly discouraged from fiddling with it.
// If you do, all hell will break loose and living will envy the dead.
//////////////////////////////////////////////////////////////////////////";


        private static readonly HashSet<string> _excludeDirs = new HashSet<string>(new [] {"obj", "bin", "Properties"}, StringComparer.OrdinalIgnoreCase);
        private static readonly Regex _classRegex = new Regex(@"(\s*)public\s+class");
        private static readonly Regex _ifaceRegex = new Regex(@"(\s*)public\s+interface");
        private static readonly Regex _structRegex = new Regex(@"(\s*)public\s+struct");
        private static readonly Regex _enumRegex = new Regex(@"(\s*)public\s+enum");

        private static readonly Dictionary<SyntaxKind, Func<BaseTypeDeclarationSyntax, SyntaxTokenList>> _modifierAccessors = 
            new Dictionary<SyntaxKind, Func<BaseTypeDeclarationSyntax, SyntaxTokenList>>
            {
                {SyntaxKind.ClassDeclaration, syntax => ((ClassDeclarationSyntax)syntax).Modifiers},
                {SyntaxKind.InterfaceDeclaration, syntax => ((InterfaceDeclarationSyntax)syntax).Modifiers},
                {SyntaxKind.StructDeclaration, syntax => ((StructDeclarationSyntax)syntax).Modifiers},
                {SyntaxKind.EnumDeclaration, syntax => ((EnumDeclarationSyntax)syntax).Modifiers}
            };
        
        static void Main(string[] args)
        {
            try
            {
                string solutionDir = args[0];

                EmbedProject(solutionDir, "NotLimited.Framework.Common");

                Console.WriteLine("Done!");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private static void EmbedProject(string solutionDir, string projectName)
        {
            var nsRegex = new Regex(@"(\s*namespace\s+)" + projectName);
            var usingRegex = new Regex(@"(\s*using\s+)" + projectName);

            string nugetDir = Path.Combine(solutionDir, "NuGet", projectName);
            if (!Directory.Exists(nugetDir))
                Directory.CreateDirectory(nugetDir);
            
            string sourcesDir = Path.Combine(nugetDir, "sources");
            if (Directory.Exists(sourcesDir))
                PathHelpers.DeleteDirectory(sourcesDir);
            Directory.CreateDirectory(sourcesDir);

            var dir = new DirectoryInfo(Path.Combine(solutionDir, projectName));
            var sources =
                dir.GetFiles("*.cs")
                   .Concat(dir.GetDirectories()
                              .Where(x => !_excludeDirs.Contains(x.Name))
                              .SelectMany(x => x.GetFiles("*.cs", SearchOption.AllDirectories)))
                   .ToList();

            var dstFiles = new List<string>();
            foreach (var file in sources)
            {
                string dstPath = PathHelpers.MakeAbsolute(PathHelpers.MakeRelative(file.FullName, dir.FullName), sourcesDir);
                string dstDir = Path.GetDirectoryName(dstPath);
                if (!Directory.Exists(dstDir))
                    Directory.CreateDirectory(dstDir);

                file.CopyTo(dstPath);
                dstFiles.Add(dstPath);
            }

            foreach (var file in dstFiles)
            {
                string content = ChangeVisibility(File.ReadAllText(file));
                content = nsRegex.Replace(content, match => match.Groups[1].Value + "$rootnamespace$");
                content = usingRegex.Replace(content, match => match.Groups[1].Value + "$rootnamespace$");
                content = _classRegex.Replace(content, match => match.Groups[1].Value + "internal class");
                content = _ifaceRegex.Replace(content, match => match.Groups[1].Value + "internal interface");
                content = _structRegex.Replace(content, match => match.Groups[1].Value + "internal struct");
                content = _enumRegex.Replace(content, match => match.Groups[1].Value + "internal enum");

                content = string.Format(Warning, projectName) + Environment.NewLine + content;

                File.Delete(file);
                File.WriteAllText(file + ".pp", content);
            }

            string nuspecPath = Path.Combine(nugetDir, projectName + ".nuspec");

            var ver = Version.Parse(GetCurrentVersion(nuspecPath));
            string version = string.Format("{0}.{1}.{2}", ver.Major, ver.Minor, ver.Build + 1);
            var doc = XDocument.Parse(Properties.Resources.Nuspec);
            var metadata = doc.Root.ChildElement("metadata");
            metadata.ChildElement("id").Value = projectName;
            metadata.ChildElement("version").Value = version;

            var files = doc.Root.ChildElement("files");
            foreach (var file in dstFiles)
            {
                files.AddElement(new XElement("file",
                    new XAttribute("src", PathHelpers.MakeRelative(file + ".pp", nugetDir)),
                    new XAttribute("target", string.Format(@"content\App_Packages\{0}.{1}\{2}", projectName, version, PathHelpers.MakeRelative(file + ".pp", sourcesDir)))));
            }

            File.WriteAllText(nuspecPath, doc.ToString());
        }

        private static string GetCurrentVersion(string path)
        {
            if (!File.Exists(path))
                return "1.0.0";

            var doc = XDocument.Load(path);
            return doc.Root.ChildElement("metadata").ChildElement("version").Value;
        }

        private static string ChangeVisibility(string source)
        {
            var tree = SyntaxFactory.ParseSyntaxTree(source);
            var root = (CompilationUnitSyntax)tree.GetRoot();

            var decls = root.DescendantNodes()
                            .Where(x => x.IsKind(SyntaxKind.ClassDeclaration)
                                        || x.IsKind(SyntaxKind.InterfaceDeclaration)
                                        || x.IsKind(SyntaxKind.StructDeclaration)
                                        || x.IsKind(SyntaxKind.EnumDeclaration))
                            .Cast<BaseTypeDeclarationSyntax>()
                            .ToList();

            root = root.ReplaceNodes(decls, (syntax, rewrittenSyntax) =>
            {
                var modifiers = _modifierAccessors[rewrittenSyntax.CSharpKind()](rewrittenSyntax);

                if (!modifiers.Any(x => x.CSharpKind() == SyntaxKind.PublicKeyword))
                    return rewrittenSyntax;

                var token = modifiers.First(x => x.CSharpKind() == SyntaxKind.PublicKeyword);

                return rewrittenSyntax.ReplaceToken(token, SyntaxFactory.Token(token.LeadingTrivia, SyntaxKind.InternalKeyword, token.TrailingTrivia));
            });

            return root.ToString();
        }
    }
}
