# ControlBuilderInterceptor
A sample control builder interceptor for WebForms that reads an AutoBind attribute and calls DataBind() on the control in the PreRender
event of the lifecycle
  
You can build your own Control Builder Interceptor to control how a page's markup gets converted into code. Look at the file AutoBindControlBuilderInterceptor.cs. I tried to use the build in expression api to produce code that is usable in all .NET languages, but it turns out there is no way (that I could find) to create and subscribe to an event, so I just had it output the c# text `"(s, e) => @__ctrl.DataBind();"`.  Oddly, the Microsoft rep in the demo that I watched did the same thing. I left my attempts in the commented code.  
  
In order to use a custom control builder interceptor, add this to your web.config file:  
<compilation debug="true" targetFramework="4.5" controlBuilderInterceptorType="ControlBuilderInterceptor.AutoBindControlBuilderInterceptor" />
   
