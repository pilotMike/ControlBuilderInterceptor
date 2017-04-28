using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace ControlBuilderInterceptor
{
    public class AutoBindControlBuilderInterceptor : System.Web.Compilation.ControlBuilderInterceptor
    {
        private static bool BindMethodCreated = false;

        public override void PreControlBuilderInit(ControlBuilder controlBuilder, TemplateParser parser, ControlBuilder parentBuilder, Type type, string tagName, string id, IDictionary attributes, IDictionary additionalState)
        {
            if (!type?.IsSubclassOf(typeof(Control)) ?? true) return;

            var autoBindKey = attributes?.Keys?.Cast<string>().FirstOrDefault(key => String.Equals(key, "AutoBind", StringComparison.CurrentCultureIgnoreCase));
            if (autoBindKey == null) return;
            var autobind = attributes?[autoBindKey];
            if (autobind == null) return;

            attributes.Remove(autobind);

            bool autoBindValue;
            bool.TryParse(autobind.ToString(), out autoBindValue);

            additionalState.Add("AutoBind", autoBindValue);
            
        }

        public override void OnProcessGeneratedCode(ControlBuilder controlBuilder, CodeCompileUnit codeCompileUnit, 
            CodeTypeDeclaration baseType, CodeTypeDeclaration derivedType, 
            CodeMemberMethod buildMethod, CodeMemberMethod dataBindingMethod, IDictionary additionalState)
        {
            //Debugger.Break();
            if (buildMethod == null) { return; }

            var autoBind = additionalState["AutoBind"];
            if (autoBind is bool && (bool)autoBind)
            {
                //buildMethod.Statements.Insert(buildMethod.Statements.Count - 1,
                //    new CodeAttachEventStatement(new CodeEventReferenceExpression(new CodeThisReferenceExpression(), "PreRenderComplete"), 
                //        new CodeDelegateCreateExpression(
                //            new CodeTypeReference(typeof(EventHandler)),
                //            new CodeThisReferenceExpression(), "DataBind")));
                //var statement = codeCompileUnit.;

                
                buildMethod.Statements.Insert(buildMethod.Statements.Count - 1,
                    new CodeAttachEventStatement(new CodeEventReferenceExpression(new CodeThisReferenceExpression(), "PreRenderComplete"),
                         new CodeSnippetExpression("(s, e) => @__ctrl.DataBind();")));

                //if (!BindMethodCreated)
                //{
                //    //CreateBindMethod
                //    //dataBindingMethod.
                //}

                //buildMethod.Statements.Insert(buildMethod.Statements.Count - 1,
                //    new CodeAttachEventStatement(new CodeEventReferenceExpression(new CodeThisReferenceExpression(), "PreRenderComplete"),
                        
                //        new CodeSnippetExpression("(s, e) =>")
                //            new CodeMethodInvokeExpression(
                //                new CodeMethodReferenceExpression(new CodeVariableReferenceExpression(controlVariable.Name),
                //                                                  "DataBind", null))));
            }

            //base.OnProcessGeneratedCode(controlBuilder, codeCompileUnit, baseType, derivedType, buildMethod, dataBindingMethod, additionalState);
        }

        private CodeMemberMethod InvokeDataBindExpression()
        {
            // target output code:
            // void Bind(object sender, EventArgs e)
            // {
            //    var control = sender as Control;
            //    if (control == null) return;
            //    control.DataBind();
            // }

            var method = new CodeMemberMethod
            {
                Name = "InvokeDataBind",
                Parameters =
                {
                    new CodeParameterDeclarationExpression("System.Object", "sender"),
                    new CodeParameterDeclarationExpression("System.EventArgs", "e")
                    //new CodeParameterDeclarationExpression("System.Web.UI.WebControls.Control", "control")
                }
            };
            method.Statements.Add(new CodeVariableDeclarationStatement(new CodeTypeReference("System.Web.UI.Control"), "control"));
            method.Statements.Add(
                new CodeTryCatchFinallyStatement(new CodeStatement[] 
                { 
                    new CodeAssignStatement(new CodeVariableReferenceExpression("control"),
                        new CodeCastExpression(typeof(System.Web.UI.Control), new CodeVariableReferenceExpression("sender")))
                },
                new CodeCatchClause[] { new CodeCatchClause() {Statements = {  new CodeMethodReturnStatement() }}})
            );

            method.Statements.Add(
                new CodeMethodInvokeExpression(new CodeArgumentReferenceExpression("control"),
                    "DataBind"));
            return method;
        }
    }
}